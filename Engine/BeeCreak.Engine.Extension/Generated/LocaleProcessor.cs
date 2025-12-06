using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = LocaleConfig.ProcessorDisplayName)]
public sealed class LocaleProcessor : ContentProcessor<LocaleDto, LocaleContent>
{
    public override LocaleContent Process(LocaleDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new LocaleContent
        {
Id = input.Id,
};


if (input.Translations is not null)
        {
            foreach (var entry in input.Translations)
            {
content.Translations[entry.Key] = entry.Value;
}
        }
return content;
    }

    private static void AssertValid(LocaleDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("Locale payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Locale requires ''.");
        }

        if (input.Translations is null)
        {
throw new InvalidContentException("Locale requires ''.");
}
}
}
