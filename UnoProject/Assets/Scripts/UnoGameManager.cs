using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoTerminal; // So we can access the classes like GamePlay, Card, etc.

public class UnoGameManager : Subject {
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
        float cardWidth = 2.5f; // Adjust for better spacing
        float xOffset = -(gamePlay.CurrentPlayer.Hand.Count / 2f) * cardWidth;
        float xSpacing = cardWidth;

        // figure out which player's hand to display
        Player currentP = gamePlay.CurrentPlayer;



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
            //  Debug.Log("cardGO: " + (cardGO != null ? cardGO.name : "Null"));


            // 2) Position it

            cardGO.transform.position = new Vector3(xPos, -3f, 0f);
            cardGO.transform.localScale = Vector3.one * 2.5f;

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
        CheckForUno();
    }

    private void DisplayCurrentCard()
    {
        var currentCard = gamePlay.CurrentCard;
        if (currentCard == null) return;

        string spriteName = ConvertCardToSpriteName(currentCard);

        GameObject cardGO = CardPool.Instance.GetCard();
        cardGO.transform.position = new Vector3(0f, 7f, 0f);
        cardGO.transform.localScale = Vector3.one * 2.5f;

        SpriteRenderer sr = cardGO.GetComponent<SpriteRenderer>();
        if (sr == null) sr = cardGO.AddComponent<SpriteRenderer>();
        sr.sprite = cardSpriteManager.GetSpriteByName(spriteName);


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
        NotifyObservers(new UnoEvent(UnoEventType.CardPlayed, cardData));
        CheckForUno();
        CheckForWinner();
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
        NotifyObservers(new UnoEvent(UnoEventType.CardDrawn, owner));
        // Redraw and Switch Players
        gamePlay.SwitchPlayer();
        CheckForWinner();
        RedrawAll();

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


    public void CheckForWinner()
    {
        if (gamePlay.Player1.Hand.Count == 0 || gamePlay.Player2.Hand.Count == 0) // Player has no cards left
        {
            Debug.Log($"{gamePlay.CurrentPlayer.Name} has won the game!");
            NotifyObservers(new UnoEvent(UnoEventType.GameWon, gamePlay.CurrentPlayer));
        }
    }
    public void CheckForUno()
    {
        Player currentPlayer = gamePlay.CurrentPlayer;

        // Check if the current player has exactly 1 card
        if (currentPlayer.Hand.Count == 1)
        {
            Debug.Log($"{currentPlayer.Name} has 1 card left! UNO should be called!");
            NotifyObservers(new UnoEvent(UnoEventType.UnoCalled));
        }
    }
    public void SetWildColor(CardColor chosencolor)
    {
        Debug.Log($"{gamePlay.CurrentCardColor}");
        gamePlay.SetWildColor(chosencolor);
        Debug.Log($"Color Chosen: {gamePlay.CurrentCardColor}");

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
