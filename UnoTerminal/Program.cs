namespace UnoTerminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Card Card1 = new Card(CardType.Number,CardColor.Red);
            Card.PrintAllCardTypes(); 

        }
    }
}
