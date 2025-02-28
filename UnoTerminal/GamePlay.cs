using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal {
    public class GamePlay {

        //private static GamePlay game = new();
        private static Stack<Card> gamedeck = Deck.CreateDeck();
        private Card currentcard = new();
        private string playerturn = "";
        private Player player1 = new();
        private Player player2 = new();
        Random random = new();

        // gets and sets
        public Card CurrentCard {
            get { return this.currentcard; }
            set { this.currentcard = value; }
        }
        public string PlayerTurn
        {
            get { return this.playerturn; }
            set { this.playerturn = value; }
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

        // constructors 
        
        public GamePlay():this(GameDeck.Pop(),"player1")
        {

        }
        public GamePlay(Card aCurrentCard, string aPlayerTurn)
        {
            CurrentCard = aCurrentCard;
            PlayerTurn = aPlayerTurn;
        }


        // Methods
        public void DisplayCurrentCard()
        {
            Console.WriteLine(CurrentCard);
        }

        public void DisplayInfo()
        {
            if (PlayerTurn.Trim().ToLower() == "player1")
            {

            } 
        }
        public void CreateHands()
        {
            for(int i = 0; i < 10; i++)
            {
                Card temp1 = GameDeck.Pop();
                Card temp2 = GameDeck.Pop();
                Player1.Hand.Add(temp1);
                Player2.Hand.Add(temp2);
            }
        }
        
        



    }
}


