using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Trapper W 3/4/2025
namespace UnoTerminal {
    public class GamePlay {

        
        private static Stack<Card> gamedeck = Deck.CreateDeck();
        private static List<Card> discardpile = new();
        private Card currentcard = GameDeck.Peek();
        private CardColor currentcardcolor;
        private Player currentplayer = new();
        private Player player1 = new Player("Player 1");
        private Player player2 = new Player("Player 2");
        Random random = new();

        // gets and sets
        public Card CurrentCard {
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
        public Player Player1 {
            get { return player1; }
            set { player1 = value; }
        }
        public Player Player2 {
            get { return player2; }
            set { player2 = value; }
        }
        public static Stack<Card> GameDeck {
            get { return gamedeck; }
            set { gamedeck = value; }
        }
        public static List<Card> DiscardPile
        {
            get { return discardpile; }
            set { discardpile = value; }
        }
        // constructors 

        public GamePlay() : this(GameDeck.Pop(),new CardColor(), new Player())
        {
            
            CurrentPlayer = Player1;
            CurrentCardColor = (CardColor)CurrentCard.ColorOfCard;
            
        }
        public GamePlay(Card aCurrentCard,CardColor aCardColor, Player aCurrentPlayer)
        {
            CurrentCard = aCurrentCard;
            CurrentCardColor = aCardColor;
            CurrentPlayer = aCurrentPlayer;

        }


        // Methods

        public void PlayGame()
        {
            bool game = true;
            while (game != false)
            {
                PlayTurn();
                if (Player1.Hand.Count == 0 || Player2.Hand.Count == 0)
                {
                    game = false;
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    Console.WriteLine();
                }
            }
            
        }
        public void PlayTurn()
        {
            // create a temp new card that will become the card that the user selects.
            
            
            DisplayCurrentCard();
            Console.WriteLine("------------------------------------------------------\n\n");
            DisplayInfo();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Enter in the place in hand of the card to play it.\nOr type 0 to Draw Card.");
            Console.WriteLine("--------------------------------------------------");

            

            int userInput = int.Parse(Console.ReadLine());

            if (userInput == 0)
            {

                DrawCard();
                SwitchPlayer();
            }

            

            else {
                foreach (Card card in CurrentPlayer.Hand.ToList())
                {

                    if (card.PlaceInHand == userInput)
                    {

                        Card tempcard = card;
                        if(tempcard.ColorOfCard == CurrentCardColor)
                        {
                            Console.Clear();
                            PlayCard(CurrentCard, tempcard);
                            SwitchPlayer();
                            break;
                        }
                        Console.WriteLine();
                        
                        switch(tempcard.TypeOfCard)
                        {
                            case CardType.Wild:
                                Console.Clear();
                                ChooseColor();
                                SwitchPlayer();
                            break;

                            case CardType.DrawTwo:
                                Console.Clear();
                                PlayCard(CurrentCard,tempcard);
                                DrawTwo();
                                
                                SwitchPlayer();
                            break;

                            case CardType.DrawFour:
                                Console.Clear();
                                PlayCard(CurrentCard, tempcard);
                                DrawFour();
                                ChooseColor();
                                SwitchPlayer();
                            break;

                            case CardType.Skip:
                                Console.Clear();
                                PlayCard(CurrentCard, tempcard);
                                SwitchPlayer();
                            break;
                            case CardType.Reverse:
                                Console.Clear();
                                PlayCard(CurrentCard, tempcard);
                                SwitchPlayer();
                            break;

                        }   
                        

                        // Checks if the card number or the color of the card is the same; // If either is true, we know we can play the card.
                        if (tempcard.TypeOfCard == CardType.Number) 
                        { 
                            if (tempcard.GetNumber() == CurrentCard.GetNumber() || tempcard.ColorOfCard == CurrentCard.ColorOfCard)
                            {
                                Console.Clear();
                                Console.WriteLine("Testing: This Works!");
                                PlayCard(CurrentCard, tempcard);
                                SwitchPlayer();
                                break;
                            }
                        }
                        
                        
                    }
                }
            }

            


        }





        public void ChooseColor()
        {
            Console.WriteLine("Enter Color (Red, Blue, Green, Yellow):");
            string choosenColor = Console.ReadLine();
            
            foreach(CardColor color in Enum.GetValues(typeof(CardColor)))
            {
               string tempColor = color.ToString();
                if(choosenColor == tempColor)
                {
                    CurrentCardColor = color;
                }
            }

            
            
            
        }

        public void PlayCard(Card current, Card temp)
        {
            // Remove the played card from hand and add the previous current card, and the new current card to the discard pile. Then make the played card the new current card.
            CurrentPlayer.RemoveFromHand(temp);
            DiscardPile.Add(temp);
            DiscardPile.Add(current);
            CurrentCard = temp;
            CurrentCardColor = CurrentCard.ColorOfCard;

        }
        public void DrawCard()
        {
            Console.Clear();
            Console.WriteLine("Drawing Card\n\n\n\n");
            Card tempcard = GameDeck.Pop();
            tempcard.PlaceInHand = GetPlaceInHand(CurrentPlayer);
            CurrentPlayer.AddToHand(tempcard);
            
            
            
        }

        public void DrawTwo() 
        {
            if (CurrentPlayer == Player1)
            {
                Console.WriteLine("Drawing Two Cards!");
                for (int i = 0; i < 2; i++)
                {
                    Card tempcard = GameDeck.Pop();
                    tempcard.PlaceInHand = GetPlaceInHand(Player2);
                    Player2.AddToHand(tempcard);

                }
            }


            else
            {
                Console.WriteLine("Drawing Two Cards!");
                for (int i = 0; i < 2; i++)
                {
                    Card tempcard = GameDeck.Pop();
                    tempcard.PlaceInHand = GetPlaceInHand(Player1);
                    Player1.AddToHand(tempcard);

                }
            }
        }
        public void DrawFour()
        {


            if (CurrentPlayer == Player1)
            {
                Console.WriteLine("Drawing Four Cards!");
                for (int i = 0; i < 4; i++)
                {
                    Card tempcard = GameDeck.Pop();
                    tempcard.PlaceInHand = GetPlaceInHand(Player2);
                    Player2.AddToHand(tempcard);
                    
                }
            }


            else
            {
                Console.WriteLine("Drawing Four Cards!");
                for (int i = 0; i < 4; i++)
                {
                    Card tempcard = GameDeck.Pop();
                    tempcard.PlaceInHand = GetPlaceInHand(Player1);
                    Player1.AddToHand(tempcard);

                }
            }
                
        }
            
        

        public void SwitchPlayer()
        {
            if(CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
            }
            else
            {
                CurrentPlayer = Player1;
            }
        }




        public void DisplayCurrentCard()
        {
            Console.WriteLine("Current Card");
            Console.WriteLine(CurrentCard);
            Console.WriteLine("Current Color");
            Console.WriteLine(CurrentCardColor.ToString());
        }

        public void DisplayInfo()
        {
            if (CurrentPlayer == Player1)
            {
                Console.WriteLine(Player1.ToString());
                Player1.ViewHand();
            }
            else
            {
                Console.WriteLine(Player2.ToString());
                Player2.ViewHand();
            }
        }

        public int GetPlaceInHand(Player aPlayer)
        {
            int lastCardIndex = aPlayer.Hand.Count() - 1;
            Card lastCard = aPlayer.Hand[lastCardIndex];
            int nextPlaceInHand = lastCard.PlaceInHand + 1;
            return nextPlaceInHand;
        }
        public void CreateHands()
        {
            for(int i = 0; i < 7; i++)
            {
                Card temp1 = GameDeck.Pop();
                Card temp2 = GameDeck.Pop();
                temp1.PlaceInHand = i + 1;
                temp2.PlaceInHand = i + 1;
                Player1.Hand.Add(temp1);
                Player2.Hand.Add(temp2);
            }
        }
        
        



    }
}


