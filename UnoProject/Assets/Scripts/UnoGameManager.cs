using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoTerminal; // So we can access the classes like GamePlay, Card, etc.

public class UnoGameManager : MonoBehaviour {
    [SerializeField] private CardSpriteManager cardSpriteManager;

    // The main logic object from console code
    public GamePlay gamePlay;
    private ITurnStrategy turnStrategy;

    public void SetTurnStrategy(ITurnStrategy strategy)
    {turnStrategy = strategy;}
    public void HandleTurn()
    {turnStrategy.HandleTurn(this);}


    void Start()
    {
        // Create the deck & the gameplay logic
        gamePlay = new GamePlay();
        // Deal 7 cards to each player
        gamePlay.CreateHands();

        SetTurnStrategy(new ClassicTurnStrategy());  // Default

        // Display the starting hands visually
        DisplayHands();
        // Also display the current card
        DisplayCurrentCard();

        Debug.Log(gamePlay.CurrentCard);
        Debug.Log(gamePlay.CurrentPlayer);
    }

    private void DisplayHands()
    {
        // Clear the board 
        // Then spawn new visuals for each player's hand

        float xOffset = -5f; // Just for spacing example
        float xSpacing = 1.5f;
        for (int i = 0; i < gamePlay.Player1.Hand.Count; i++)
        {
            Card cardData = gamePlay.Player1.Hand[i];
            // Convert the card info to a sprite name
            string spriteName = ConvertCardToSpriteName(cardData);
            float xPos = xOffset + i * xSpacing;
            float yPos = 0f;
            // Create the card at some position
            GameObject cardGO = cardSpriteManager.CreateCard(spriteName, new Vector3(xPos, yPos, 0f), Quaternion.identity);

            // Attach a small script so we know which "Card" logic object it represents
            UnoCardHolder holder = cardGO.AddComponent<UnoCardHolder>();
            holder.cardData = cardData;
            holder.manager = this; // So it can call UnoGameManager when clicked
            holder.ownerPlayer = gamePlay.Player1;
        }

        // Player2’s hand at a different Y
        for (int i = 0; i < gamePlay.Player2.Hand.Count; i++)
        {
            Card cardData = gamePlay.Player2.Hand[i];
            string spriteName = ConvertCardToSpriteName(cardData);

            float xPos = xOffset + i * xSpacing;
            float yPos = -3f;
            GameObject cardGO = cardSpriteManager.CreateCard(spriteName, new Vector3(xPos, yPos, 0f), Quaternion.identity);

            UnoCardHolder holder = cardGO.AddComponent<UnoCardHolder>();
            holder.cardData = cardData;
            holder.manager = this;
            holder.ownerPlayer = gamePlay.Player2;
        }
    }

    private void DisplayCurrentCard()
    {
        var currentCard = gamePlay.CurrentCard;
        if (currentCard == null) return;

        string spriteName = ConvertCardToSpriteName(currentCard);
        cardSpriteManager.CreateCard(spriteName, new Vector3(0f, 3f, 0f), Quaternion.identity);
    }



    // Called by our UnoCardHolder script when a card is clicked
    public void OnCardClicked(Card cardData, Player owner)
    {
        // Only let the current player play if it’s their turn
        if (gamePlay.CurrentPlayer != owner)
        {
            Debug.Log("It's not your turn!");
            return;
        }

        // Attempt to play the card
        bool success = gamePlay.TryPlayCard(cardData);
        if (!success)
        {
            Debug.Log("Invalid card!");
            return;
        }

        // If it's a Wild or DrawFour, we might need the user to pick a color
        // For now, default to the card's color or keep it as is

        // After playing, re-draw the hands visually so the card is removed from that player's hand
        // Also re-display the new CurrentCard on top of the discard pile
        RedrawAll();
    }
    public void SwitchToStackingMode()
    {
        SetTurnStrategy(new StackingTurnStrategy());
        Debug.Log("Stacking rule enabled!");
    }

    public void SwitchToClassicMode()
    {
        SetTurnStrategy(new ClassicTurnStrategy());
        Debug.Log("Classic mode enabled!");
    }
    public void OnDrawClickedCurrentPlayer()
    {
        OnDrawCardClicked(gamePlay.CurrentPlayer);
    }
    public void OnDrawCardClicked(Player owner)
    {
        // If we have a "Draw Card" button for the current player
        if (gamePlay.CurrentPlayer != owner)
        {
            Debug.Log("It's not your turn!");
            return;
        }

        gamePlay.DrawCard(owner);
        // Redraw
        RedrawAll();

        
        // can call gamePlay.SwitchPlayer() here if needed?
    }

    private void RedrawAll()
    {
        // remove all card objects from the scene, then re-spawn them
        
        foreach (var obj in FindObjectsOfType<UnoCardHolder>())
        {
            Destroy(obj.gameObject);
        }

        DisplayHands();
        DisplayCurrentCard();
    }

    public int GetStackedDrawCount()
    {
        int count = 0;
        Card currentCard = gamePlay.CurrentCard;

        // Count how many DrawTwo cards have been played consecutively
        while (currentCard != null && currentCard.TypeOfCard == CardType.DrawTwo)
        {
            count += 2; // Each DrawTwo card adds +2 to the stack
            currentCard = gamePlay.GetPreviousCard(); // Assume this method retrieves the last played card
        }

        return count;
    }


    public void NextPlayer()
    {
        gamePlay.SwitchPlayer();  // Assuming gamePlay has a method to change the current player
        Debug.Log($"Next turn: {gamePlay.CurrentPlayer.Name}");
    }

    private string ConvertCardToSpriteName(Card cardData)
    {
        // Match up sprite naming convention
        if (cardData is NumberCard num)
        {
            // e.g. Red_0 or Green_7
            return $"{num.ColorOfCard}_{num.Number}";
        }
        else
        {
            switch (cardData.TypeOfCard)
            {
                case CardType.Skip: return $"{cardData.ColorOfCard}_Skip";
                case CardType.DrawTwo: return $"{cardData.ColorOfCard}_DrawTwo";
                case CardType.DrawFour: return $"Wild_DrawFour";
                case CardType.Reverse: return $"{cardData.ColorOfCard}_Reverse";
                case CardType.Wild: return "Wild";
                default: return "Unknown";
            }
        }
    }
}
