namespace Shared.GameLogic;


public class PlayerStatus
{
    public bool IsConnected { get; set; } = false;
    public bool IsReady { get; set; } = false;
    public bool IsHost { get; set; } = false;
    public bool IsLocal { get; set; } = false;
    public bool IsAI { get; set; } = false;
    public bool IsOut { get; set; } = false;

    public PlayerStatus()
    {
    }
}