using BeeCreak.Extensions.CellReference;
using BeeCreak.Extensions.Entity.Model;

namespace BeeCreak.Extensions.Entity.Reference;

public sealed class EntityReferenceContent
{
    public string Id { get; set; }

    public EntityModelContent Model { get; set; }

    public CellReferenceContent Cell { get; set; }
}
