using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal {
    public class Game {

        // private static Deck d = new();
        private Stack<Card> gamedeck = Deck.CreateDeck();

        private Player player1 = new();
        private Player player2 = new();
        Random random = new();

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
        public Stack<Card> GameDeck
        {
            get { return gamedeck; }
            set { gamedeck = value; }
        }

    }
}


