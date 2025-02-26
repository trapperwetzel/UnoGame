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

        
        public void AddNumberCardsToDeck()
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

        }

        public void AddSkipCardsToDeck()
        {
            int i = 0;
            while (i < 2)
            {
                
                foreach (CardColor cardcolor in Enum.GetValues(typeof(CardColor)))
                {
                    Card tempcard4 = new Card(CardType.Skip, cardcolor);
                    deck.Add(tempcard4);
                   
                }
                i++;
            }
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
