



namespace UnoTerminal {
    using Microsoft.Extensions.Logging;
    using System.Net.NetworkInformation;
    internal class Program
    {

        static void Main(string[] args)
        {
            
            

            /*
            Card Card1 = new Card(CardType.Number, CardColor.Red);
            // Card.PrintAllCardTypes(); 
            Random r = new();
            List<int> list1 = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            for(int i = 0; i <= 10; i++)
            {
                int randomintindex = r.Next(9);
                int randomint = list1[randomintindex];
                Console.WriteLine($"Random Number {i}: ");
                Console.WriteLine(randomint); 
                Console.WriteLine(); 
            }
            */


            /*
            deck3.AddNumberCardsToDeck();
            deck3.AddSkipCardsToDeck();
            deck3.AddDrawTwoToDeck();
            deck3.AddReverseToDeck();
            deck3.AddWildCardsToDeck();
            */



            /*
            var deck = Deck.CreateDeck();
            foreach(Card card in deck)
            {
                Console.WriteLine("---------------");
                Console.WriteLine(card);
                
            }
            */
            /*
            Game game = new Game();
            var deck11 = game.ListDeck;

            foreach(Card card in deck11)
            {
                Console.WriteLine("---------------");
                Console.WriteLine(card);
            }
            */



            GamePlay game = new ();

            var deck11 = GamePlay.GameDeck;


            /*
            int i = 1;
            foreach(Card card in deck11)
            {
                
                Console.WriteLine(i);
                Console.WriteLine(card);
                i++;
            }
            */
            
            game.CreateHands();
            game.PlayGame();
            /*
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            game.DisplayCurrentCard();
            Console.WriteLine();
            game.DisplayInfo();
            game.PlayTurn();
            */

        }
    }
}
