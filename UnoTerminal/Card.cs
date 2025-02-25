using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal
{

    public enum CardType
    {
        Number, // Just a regualr number card 
        Skip, // Skips the next player 
        DrawTwo, // Makes the next player draw two cards 
        DrawFour, // Makes the next player draw four cards
        Reverse, // Reverses the order of the game 
        Wild  // Changes the color of the game 
    }

    public enum CardColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Wild 
    }
    internal class Card
    {
        public CardType TypeOfCard { get; private set; }
        public CardColor ColorOfCard { get; private set; }

        public Card(CardType typeOfCard, CardColor colorOfCard)
        {
            TypeOfCard = typeOfCard;
            ColorOfCard = colorOfCard;
        }

        public static void PrintAllCardTypes()
        {
            string[] CardTypeArray = Enum.GetNames(typeof(CardType));
            foreach(string CardType in CardTypeArray) 
            { Console.WriteLine(CardType);}
        }
        public override string ToString()
        {
            string message = $"Card Type: {TypeOfCard}\nColor: {ColorOfCard}";
            return message; 
        }


    }
}
