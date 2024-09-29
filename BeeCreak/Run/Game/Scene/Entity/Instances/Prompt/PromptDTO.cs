using BeeCreak.Run.Tools;

namespace BeeCreak.Run.Game.Scene.Entity.Instances.Prompt;

public class PromptDTO : EntityDTO
{
    public string Text { get; set; }

    public override Prompt FromDTO(IToolCollection tools)
    {
        return new Prompt(tools, WorldPosition, Text);
    }
}
