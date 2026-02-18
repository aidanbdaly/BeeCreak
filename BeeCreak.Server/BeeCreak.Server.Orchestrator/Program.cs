using BeeCreak.Orchestrator.Models;
using BeeCreak.Orchestrator.Options;
using BeeCreak.Orchestrator.Services;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
var apiKeyHeader = builder.Configuration.GetValue<string>("Orchestrator:ApiKeyHeader") ?? "X-Api-Key";
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API key needed to access the endpoints.",
        In = ParameterLocation.Header,
        Name = apiKeyHeader,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.Configure<OrchestratorOptions>(builder.Configuration.GetSection("Orchestrator"));
builder.Services.AddSingleton<KubectlInvoker>();
builder.Services.AddSingleton<IGameServerOrchestrator, KubectlGameServerOrchestrator>();

var app = builder.Build();
var enableHttpsRedirection = builder.Configuration.GetValue("Orchestrator:EnableHttpsRedirection", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (enableHttpsRedirection)
{
    app.UseHttpsRedirection();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/health"))
    {
        await next();
        return;
    }

    var options = context.RequestServices.GetRequiredService<IOptions<OrchestratorOptions>>().Value;
    if (string.IsNullOrWhiteSpace(options.ApiKey))
    {
        await next();
        return;
    }

    if (!context.Request.Headers.TryGetValue(options.ApiKeyHeader, out var apiKey) ||
        !string.Equals(apiKey.ToString(), options.ApiKey, StringComparison.Ordinal))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Unauthorized");
        return;
    }

    await next();
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }))
    .WithName("Health");

app.MapGet("/servers", async (IGameServerOrchestrator orchestrator, CancellationToken ct) =>
    Results.Ok(await orchestrator.ListAsync(ct)))
    .WithName("ListServers")
    .WithOpenApi();

app.MapGet("/servers/{id}", async (string id, IGameServerOrchestrator orchestrator, CancellationToken ct) =>
{
    var server = await orchestrator.GetAsync(id, ct);
    return server is null ? Results.NotFound() : Results.Ok(server);
})
    .WithName("GetServer")
    .WithOpenApi();

app.MapPost("/servers", async (CreateServerRequest? request, IGameServerOrchestrator orchestrator, CancellationToken ct) =>
{
    var created = await orchestrator.CreateAsync(request ?? new CreateServerRequest(), ct);
    return Results.Created($"/servers/{created.Id}", created);
})
    .WithName("CreateServer")
    .WithOpenApi();

app.MapDelete("/servers/{id}", async (string id, IGameServerOrchestrator orchestrator, CancellationToken ct) =>
{
    var deleted = await orchestrator.DeleteAsync(id, ct);
    return deleted ? Results.NoContent() : Results.NotFound();
})
    .WithName("DeleteServer")
    .WithOpenApi();

app.Run();
