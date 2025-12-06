using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = TextSetConfig.ProcessorDisplayName)]
public sealed class TextSetProcessor : ContentProcessor<TextSetDto, TextSetContent>
{
    public override TextSetContent Process(TextSetDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new TextSetContent
        {
Id = input.Id,
Font = input.Font,
};


if (input.Text is not null)
        {
            foreach (var entry in input.Text)
            {
content.Text[entry.Key] = entry.Value;
}
        }
return content;
    }

    private static void AssertValid(TextSetDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("TextSet payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("TextSet requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.Font))
        {
            throw new InvalidContentException("TextSet requires ''.");
        }

        if (input.Text is null)
        {
throw new InvalidContentException("TextSet requires ''.");
}
}
}
