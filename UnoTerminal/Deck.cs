using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal
{
    public class Deck
    {
        public static List<Card> deck =  new List<Card>(); // Place holder before we make the initalize deck method 
        
        Random random = new(); 

        
        public static List<Card> AddNumberCardsToDeck()
        {
            foreach(CardColor cardcolor in Enum.GetValues(typeof(CardColor)))
            {
                for(int i = 0; i < 10; i++)
                {

                    if(i > 0)
                    {
                        Card tempCard = new Card(CardType.Number, cardcolor, i);
                        Card tempCard2 = new Card(CardType.Number, cardcolor, i);
                        deck.Add(tempCard);
                        deck.Add(tempCard2);
                    }
                    else
                    {
                        Card zeroCard = new Card(CardType.Number, cardcolor, i);
                        deck.Add(zeroCard);
                    }
                    
                }
            }


            return deck;


        }


        public void printCards()
        {
            foreach(Card card in deck)
            {
                Console.WriteLine(card.ToString());
                Console.WriteLine();
            }
        }





    }
}
