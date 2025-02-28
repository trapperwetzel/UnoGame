using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal
{

    public enum CardType
    {
        Number, // Just a regular number card 
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
        Yellow
    }

    
    public class Card {

        public CardType TypeOfCard { get; private set; }
        public CardColor? ColorOfCard { get; private set; }
        public int? CardNumber { get; private set; }

        
        public Card()
        {

        }
        public Card(CardType typeOfCard)
        {
            TypeOfCard = typeOfCard;
        }
        public Card(CardType typeOfCard, CardColor colorOfCard)
        {
            TypeOfCard = typeOfCard;
            ColorOfCard = colorOfCard;
        }
        public Card(CardType typeOfCard, CardColor colorOfCard, int cardNumber)
        {
            TypeOfCard = typeOfCard;
            ColorOfCard = colorOfCard;
            CardNumber = cardNumber;

        }

        public static void PrintAllCardTypes()
        {
            string[] CardTypeArray = Enum.GetNames(typeof(CardType));
            foreach(string CardType in CardTypeArray) { Console.WriteLine(CardType);}
        }




        public override string ToString()
        {
            
            if(TypeOfCard == CardType.Number)
            {
                string message = $"Card Type: {TypeOfCard}\nColor: {ColorOfCard}\nNumber: {CardNumber}";
                return message;
            }
            else if(TypeOfCard == CardType.Wild || TypeOfCard == CardType.DrawFour)
            {
                string message = $"Card Type: {TypeOfCard}";
                return message;
            }
            else
            {
                string message = $"Card Type: {TypeOfCard}\nColor: {ColorOfCard}";
                return message;
            }
            
            
        }


    }
}
