namespace UnoTerminal
{
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

            Deck deck3 = new Deck();
            deck3.AddNumberCardsToDeck();
            deck3.AddSkipCardsToDeck();
            deck3.printCards();
        }
    }
}
