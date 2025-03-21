using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UnoTerminal {
    
    // Handles the main UNO gameplay logic
    
    
    public class GamePlay {
        // The main deck of cards (face-down draw pile)
        private static Stack<Card> gamedeck = Deck.CreateDeck();

        // Discard pile
        private static List<Card> discardpile = new();

        // Current card in play (on top of the discard pile)
        private Card currentcard;

        // The "active" color in effect (especially important for Wild and DrawFour cards)
        private CardColor currentcardcolor;

        // Whose turn it is right now
        private Player currentplayer = new Player();

        // We have two players for current setup.
        // (Can expand to more players if needed.)
        private Player player1 = new Player("Player 1");
        private Player player2 = new Player("Player 2");

        
        // gets and sets
        
        public Card CurrentCard
        {
            get { return this.currentcard; }
            set { this.currentcard = value; }
        }

        public CardColor CurrentCardColor
        {
            get { return currentcardcolor; }
            set { currentcardcolor = value; }
        }

        public Player CurrentPlayer
        {
            get { return this.currentplayer; }
            set { this.currentplayer = value; }
        }

        public Player Player1
        {
            get { return player1; }
            set { player1 = value; }
        }

        public Player Player2
        {
            get { return player2; }
            set { player2 = value; }
        }

        public static Stack<Card> GameDeck
        {
            get { return gamedeck; }
            set { gamedeck = value; }
        }

        public static List<Card> DiscardPile
        {
            get { return discardpile; }
            set { discardpile = value; }
        }

        
        // Constructors
        
        public GamePlay()
            : this(GameDeck.Pop(), CardColor.Red, new Player("Player 1"))
        {
            // Set the initial CurrentPlayer
            CurrentPlayer = Player1;
            // Use the color of the first card drawn
            CurrentCardColor = CurrentCard.ColorOfCard;
        }

        public GamePlay(Card aCurrentCard, CardColor aCardColor, Player aCurrentPlayer)
        {
            CurrentCard = aCurrentCard;
            CurrentCardColor = aCardColor;
            CurrentPlayer = aCurrentPlayer;
        }

        

        
        /// Deal 7 cards to each player from the deck.
        
        public void CreateHands()
        {
            for (int i = 0; i < 7; i++)
            {
                Card temp1 = GameDeck.Pop();
                Card temp2 = GameDeck.Pop();

                // Assign a place-in-hand index so your logic can keep track
                temp1.PlaceInHand = i + 1;
                temp2.PlaceInHand = i + 1;

                Player1.Hand.Add(temp1);
                Player2.Hand.Add(temp2);
            }
        }

        
        // Called when a player attempts to play a card from their hand.
        // Return true if it was successfully played, false if invalid.
        // Uses the PlayCardStrategy (Strategy Pattern)
        
        public bool TryPlayCard(Card playedCard)
        {
            // 1) Check if the card is valid to play
            if (!CheckIfValid(playedCard))
            {
                // In Unity, you'd show a warning or message in the UI.
                return false;
            }

            IPlayCardStrategy strategy = PlayCardStrategyFactory.GetStrategy(playedCard);
            strategy.Execute(playedCard, this);

            

            return true;
        }

        
        // Called by Unity code if a player clicks a "Draw Card" button.
        
        public void DrawCard(Player p)
        {
            // If deck is empty, you might want to reshuffle discard pile or handle it differently
            if (GameDeck.Count == 0)
            {
                return;
            }

            Card drawnCard = GameDeck.Pop();
            drawnCard.PlaceInHand = GetPlaceInHand(p);
            p.AddToHand(drawnCard);
            
        }




        
        // Called when a Wild card is played in Unity
        
        public void SetWildColor(CardColor newColor)
        {
            CurrentCardColor = newColor;
        }

        
        // Internal Helpers
        

        
        // Check if playing this card is valid based on the current card/color.
        
        private bool CheckIfValid(Card tempcard)
        {
            // 1) Same color
            if (tempcard.ColorOfCard == CurrentCardColor)
            {
                return true;
            }
            // 2) Same type (for Skip, Reverse, etc.), ignoring numbers
            else if (tempcard.TypeOfCard == CurrentCard.TypeOfCard && tempcard.TypeOfCard != CardType.Number)
            {
                return true;
            }
            // 3) Both are NumberCards with the same number
            else if (tempcard.TypeOfCard == CardType.Number && CurrentCard.TypeOfCard == CardType.Number)
            {
                if (tempcard.GetNumber() == CurrentCard.GetNumber())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // 4) Wild or DrawFour can be played anytime
            else if (tempcard.TypeOfCard == CardType.Wild || tempcard.TypeOfCard == CardType.DrawFour)
            {
                return true;
            }
            // Otherwise, invalid
            else
            {
                return false;
            }
        }

        
        

        
        // Move from CurrentCard -> Discard pile, and set the new card as CurrentCard.
        
        public void PlayCard(Card oldCard, Card newCard)
        {
            // Remove the chosen card from the player's hand
            CurrentPlayer.RemoveFromHand(newCard);

            // Put both old and new cards into the discard pile
            DiscardPile.Add(newCard);
            DiscardPile.Add(oldCard);

            // The new card is now the "current" card in play
            CurrentCard = newCard;
            CurrentCardColor = CurrentCard.ColorOfCard;
        }

        
        // Forces the next player to draw 2 cards.
        
        public void DrawTwo()
        {
            Player nextPlayer = (CurrentPlayer == Player1) ? Player2 : Player1;
            for (int i = 0; i < 2; i++)
            {
                DrawCard(nextPlayer);
            }
        }

        
        // Forces the next player to draw 4 cards.
        
        public void DrawFour()
        {
            Player nextPlayer = (CurrentPlayer == Player1) ? Player2 : Player1;
            for (int i = 0; i < 4; i++)
            {
                DrawCard(nextPlayer);
            }
        }

        
        // Switch whose turn it is. 
        // (In a 2-player game, we just swap between Player1 and Player2.)
        
        public void SwitchPlayer()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
            }
            else
            {
                CurrentPlayer = Player1;
            }
        }
        public Card GetPreviousCard()
        {
            if (DiscardPile.Count < 2) return null;
            return DiscardPile[DiscardPile.Count - 2]; // Get the second last card
        }

        // Assigns a new "PlaceInHand" index to the card we're about to draw,
        // so it doesn't overlap with existing cards in the player's hand.

        private int GetPlaceInHand(Player p)
        {
            if (p.Hand.Count == 0)
            {
                return 1;
            }
            Card lastCard = p.Hand[p.Hand.Count - 1];
            return lastCard.PlaceInHand + 1;
        }
    }
}


