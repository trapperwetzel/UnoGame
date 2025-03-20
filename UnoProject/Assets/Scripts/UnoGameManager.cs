using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoTerminal; // So we can access the classes like GamePlay, Card, etc.

public class UnoGameManager : MonoBehaviour {
    [SerializeField] private CardSpriteManager cardSpriteManager;

    // The main logic object from console code
    public GamePlay gamePlay;
    


    void Start()
    {
        InitializeGame();
    }
    public void InitializeGame()
    {
        Debug.Log("Game started!");
        // Create the deck & the gameplay logic
        gamePlay = new GamePlay();
        // Deal 7 cards to each player
        gamePlay.CreateHands();

        DisplayHands();

        DisplayCurrentCard();
        Debug.Log(gamePlay.CurrentCard);
        Debug.Log(gamePlay.CurrentPlayer);
    }
    private void DisplayHands()
    {
        float xOffset = -5f; // For spacing
        float xSpacing = 1.5f;

        // figure out which player's hand to display
        Player currentP = gamePlay.CurrentPlayer;

        // We'll create a reference for position offset
        float yPos = (currentP == gamePlay.Player1) ? 0f : -3f;

        // We'll also define a local method to get the player's hand & spawned list
        List<Card> cardsToShow = currentP.Hand;
        List<GameObject> spawnedObjs = currentP.SpawnedCardObjects;
        Debug.Log("CardPool.Instance: " + (CardPool.Instance != null ? "Exists" : "Null"));
        Debug.Log("cardSpriteManager: " + (cardSpriteManager != null ? "Assigned" : "Null"));
        // For spacing
        for (int i = 0; i < cardsToShow.Count; i++)
        {
            Card cardData = cardsToShow[i];
            string spriteName = ConvertCardToSpriteName(cardData);

            float xPos = xOffset + i * xSpacing;

            // 1) Get a card GameObject from the pool
            
            GameObject cardGO = CardPool.Instance.GetCard();
            Debug.Log("cardGO: " + (cardGO != null ? cardGO.name : "Null"));
            // 2) Position it
            cardGO.transform.position = new Vector3(xPos, yPos, 0f);
            cardGO.transform.rotation = Quaternion.identity;

            // 3) Assign the correct sprite
            SpriteRenderer sr = cardGO.GetComponent<SpriteRenderer>();
            if (sr == null) sr = cardGO.AddComponent<SpriteRenderer>();
            Sprite theSprite = cardSpriteManager.GetSpriteByName(spriteName);
            sr.sprite = theSprite;

            // 4) Attach or refresh UnoCardHolder
            UnoCardHolder holder = cardGO.GetComponent<UnoCardHolder>();
            if (holder == null) holder = cardGO.AddComponent<UnoCardHolder>();
            holder.cardData = cardData;
            holder.manager = this;
            holder.ownerPlayer = currentP;

            // 5) Track this card GameObject in the player's list
            spawnedObjs.Add(cardGO);
        }
    }

    private void DisplayCurrentCard()
    {
        var currentCard = gamePlay.CurrentCard;
        if (currentCard == null) return;

        string spriteName = ConvertCardToSpriteName(currentCard);

        GameObject cardGO = CardPool.Instance.GetCard();
        cardGO.transform.position = new Vector3(0f, 3f, 0f);
        cardGO.transform.rotation = Quaternion.identity;

        SpriteRenderer sr = cardGO.GetComponent<SpriteRenderer>();
        if (sr == null) sr = cardGO.AddComponent<SpriteRenderer>();
        sr.sprite = cardSpriteManager.GetSpriteByName(spriteName);

        // for future*** track it in a "CurrentCardObj" variable, or discard list, etc.
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
        // Redraw and Switch Players
        gamePlay.SwitchPlayer();
        RedrawAll();
        

        
        // can call gamePlay.SwitchPlayer() here if needed?
    }

    private void RedrawAll()
    {
        // Return Player1's old cards
        foreach (var cardObj in gamePlay.Player1.SpawnedCardObjects)
        {
            CardPool.Instance.ReturnCard(cardObj);
        }
        gamePlay.Player1.SpawnedCardObjects.Clear();

        // Return Player2's old cards
        foreach (var cardObj in gamePlay.Player2.SpawnedCardObjects)
        {
            CardPool.Instance.ReturnCard(cardObj);
        }
        gamePlay.Player2.SpawnedCardObjects.Clear();

        // Now re-display with fresh visuals
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
        gamePlay.SwitchPlayer();  
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
