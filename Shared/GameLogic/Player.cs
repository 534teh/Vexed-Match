using System.Security.Cryptography.X509Certificates;

namespace Shared.GameLogic;


public class Player(string name, int playerId, int cardsLeft, string iconName, PlayerStatus status)
{
    public string Name { get; private set; } = name;
    public int PlayerId { get; private set; } = playerId;
    public int CardsLeft { get; private set; } = cardsLeft;
    public string IconAssetName { get; private set; } = iconName;
    public PlayerStatus Status { get; private set; } = status;


    public void UpdateCardCount(int newCount)
    {
        if (newCount < 0)
            throw new ArgumentOutOfRangeException(nameof(newCount), "Card count cannot be negative.");

        CardsLeft = newCount;
    }

    public void UpdateStatus(PlayerStatus newStatus)
    {
        Status = newStatus;
    }
}