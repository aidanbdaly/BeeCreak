using BeeCreak.Tools;

namespace BeeCreak.Game.Scene.Entity.Instances.Prompt;

public class PromptDTO : EntityDTO
{
    public string Text { get; set; }

    public override Prompt FromDTO(IToolCollection tools)
    {
        return new Prompt(tools, WorldPosition, Text);
    }
}
