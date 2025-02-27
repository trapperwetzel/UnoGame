using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal {
    public class Game {

        private Player player1 = new();
        private Player player2 = new();

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

        public void AddCard()
        {
            
        }
        

    }
}

