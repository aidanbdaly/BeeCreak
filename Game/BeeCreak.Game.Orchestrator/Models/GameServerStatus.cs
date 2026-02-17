namespace BeeCreak.Game.Orchestrator.Models;

public enum GameServerStatus
{
    Unknown = 0,
    Provisioning = 1,
    Running = 2,
    Failed = 3,
    Succeeded = 4,
    Deleting = 5
}
