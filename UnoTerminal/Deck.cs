using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal
{
    public class Deck {

        public static List<Card> GameDeck = new List<Card>(); // Place holder before we make the initalize deck method 
        
        Random random = new(); 


        /*
        public List<Card> GetDeck()
        {
            return deck;          
        }
        */
        // Combines all the methods into a CreateDeck() for ease of use + encapsulation
        public List<Card> CreateDeck()
        {
            AddNumberCardsToDeck();
            AddSkipCardsToDeck();
            AddDrawTwoToDeck();
            AddReverseToDeck();
            AddWildCardsToDeck();
            return GameDeck;
        }








        // Adds all the numbered cards to the deck. 
        // There is only **1** Zero for each color.
        // The rest of the numbers (1-9) will be in each color twice.
        // Example: Red Color has (0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9) 
        private void AddNumberCardsToDeck()
        {
            foreach(CardColor cardcolor in Enum.GetValues(typeof(CardColor)))
            {
                for(int i = 0; i < 10; i++)
                {

                    if(i > 0)
                    {
                        Card tempCard = new Card(CardType.Number, cardcolor, i);
                        Card tempCard2 = new Card(CardType.Number, cardcolor, i);
                        GameDeck.Add(tempCard);
                        GameDeck.Add(tempCard2);
                    }
                    else
                    {
                        Card zeroCard = new Card(CardType.Number, cardcolor, i);
                        GameDeck.Add(zeroCard);
                    }
                    
                }
            }

        }

        // Adds the skip cards to the deck
        // Each color gets two skips.
        private void AddSkipCardsToDeck()
        {
            int i = 0;
            while (i < 2)
            {
                
                foreach (CardColor cardcolor in Enum.GetValues(typeof(CardColor)))
                {
                    Card tempcard4 = new Card(CardType.Skip, cardcolor);
                    GameDeck.Add(tempcard4);
                   
                }
                i++;
            }
        }

        // Adds the "Draw 2" cards to the deck
        // Each color gets two Draw 2's. 
        private void AddDrawTwoToDeck()
        {
            int i = 0;
            while( i < 2)
            {
                foreach(CardColor cardcolor in Enum.GetValues(typeof(CardColor)))
                {
                    Card tempcard5 = new Card(CardType.DrawTwo, cardcolor);
                    GameDeck.Add(tempcard5);
                }
                i++;
            }
        }

        // Adds the reverse cards to the deck. 
        // Each color gets two reverse cards.
        private void AddReverseToDeck()
        {
            int i = 0;
            while( i < 2)
            {
                foreach(CardColor cardcolor in Enum.GetValues(typeof(CardColor)))
                {
                    Card tempcard6 = new Card(CardType.Reverse, cardcolor);
                    GameDeck.Add(tempcard6);
                }
                i++;
            }
        }

        // Adds the "Wild Card" and the "DrawFour Wild Card" to the deck. 
        private void AddWildCardsToDeck()
        {
            int i = 0;
            while(i < 4)
            {
                Card tempcard7 = new Card(CardType.Wild);
                Card tempcard8 = new Card(CardType.DrawFour);
                GameDeck.Add(tempcard7);
                GameDeck.Add(tempcard8);
                i++;
            }
            
        }














        
        public void printCards()
        {
            foreach(Card card in GameDeck)
            {
                Console.WriteLine(card.ToString());
                Console.WriteLine();
            }
        }





    }
}
