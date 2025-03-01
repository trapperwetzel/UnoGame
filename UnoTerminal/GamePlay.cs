using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal {
    public class GamePlay {

        
        private static Stack<Card> gamedeck = Deck.CreateDeck();
        private static List<Card> discardpile = new();
        private Card currentcard = new();
        private Player currentplayer = new();
        private Player player1 = new();
        private Player player2 = new();
        Random random = new();

        // gets and sets
        public Card CurrentCard {
            get { return this.currentcard; }
            set { this.currentcard = value; }
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

        public GamePlay() : this(GameDeck.Pop(), new Player())
        {
            Player1 = new Player();
            Player2 = new Player();
            CurrentPlayer = Player1;
            
        }
        public GamePlay(Card aCurrentCard, Player aCurrentPlayer)
        {
            CurrentCard = aCurrentCard;
            CurrentPlayer = aCurrentPlayer;

        }


        // Methods


        public void PlayTurn()
        {
            // create a temp new card that will become the card that the user selects.
            Card tempcard = new();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Enter in the place in hand of the card to play it.\nOr type 0 to Draw Card.");
            Console.WriteLine("--------------------------------------------------");

            

            int userInput = int.Parse(Console.ReadLine());
            
            foreach (Card card in CurrentPlayer.Hand.ToList())
            {
                
                if (card.PlaceInHand == userInput)
                {
                    tempcard = card;
                    Console.WriteLine();
                    //Console.WriteLine("Testing!\n\n");
                    //Console.WriteLine("temp card below\n");
                    //Console.WriteLine(tempcard);
                    Console.WriteLine();
                    // Checks if the card number or the color of the card is the same; 
                    // If either is true, we know we can play the card. 
                    if (tempcard.CardNumber == CurrentCard.CardNumber || tempcard.ColorOfCard == CurrentCard.ColorOfCard)
                    {
                        Console.WriteLine("Testing: This Works!");
                        CurrentPlayer.PlayCard(tempcard);
                        DiscardPile.Add(tempcard);
                        DiscardPile.Add(CurrentCard);
                        CurrentCard = GameDeck.Pop();
                        Console.WriteLine(CurrentCard.ToString());
                        
                        
                        
                        //CurrentCard = FinishedCard;
                        Console.WriteLine();
                        Console.WriteLine("Your new hand!\n\n");
                        
                        Console.WriteLine("Your new current card!\n\n");
                        
                        Console.WriteLine(CurrentCard.ToString());
                        
                    } 
                    // The else if will check for the wild card situations. Checking if the type of the card is the same and that card number is null.
                    else if (tempcard.TypeOfCard == card.TypeOfCard)
                    {
                        if(tempcard.CardNumber == null & tempcard.CardNumber == null)
                        {
                            Console.WriteLine("Wild Card is the same");


                        }
                    }
                }
            }


        }



        public void DisplayCurrentCard()
        {
            Console.WriteLine("Current Card\n");
            Console.WriteLine(CurrentCard);
        }

        public void DisplayInfo()
        {
            if (CurrentPlayer == Player1)
            {
                Player1.ViewHand();
            }
            else
            {
                Player2.ViewHand();
            }
        }
        public void CreateHands()
        {
            for(int i = 0; i < 10; i++)
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


