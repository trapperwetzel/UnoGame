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

    
    public abstract class Card {

        public abstract CardType TypeOfCard { get; }
        public  CardColor ColorOfCard { get; set; }
        public int PlaceInHand { get; set; }

        
        public Card()
        {

        }
        
        public Card(CardColor colorOfCard)
        {
            
            ColorOfCard = colorOfCard;
        }

        public Card(CardColor colorOfCard, int aPlaceInHand)
        {
            
            ColorOfCard = colorOfCard;
            PlaceInHand = aPlaceInHand;
        }

        public virtual int? GetNumber() => null;

        public  override string ToString()
        {
            string message = $"Card: {ColorOfCard} {TypeOfCard}\n# In Hand: {PlaceInHand}";
            return message;   
        }

    }


    public class NumberCard : Card
    {
        public override CardType TypeOfCard => CardType.Number;
        public int Number { get; set; }

        public NumberCard(CardColor colorOfCard, int aNumber)
        {
            
            ColorOfCard = colorOfCard;
            Number = aNumber;
        }

        public override int? GetNumber() => Number;

        public override string ToString()
        {
            string message = $"Card: {ColorOfCard} ({Number})\n# In Hand: {PlaceInHand}";
            return message;
        }


    }

    public class SkipCard : Card
    {
        public override CardType TypeOfCard => CardType.Skip;

        public SkipCard(CardColor colorOfCard)
        {
            ColorOfCard = colorOfCard;
        }

    }

    public class ReverseCard : Card
    {
        public override CardType TypeOfCard => CardType.Reverse;

        public ReverseCard(CardColor colorOfCard)
        {
            ColorOfCard = colorOfCard;
        }
    }

    public class WildCard : Card
    {
        public override CardType TypeOfCard => CardType.Wild;

        public override string ToString()
        {
            return $"Card: {TypeOfCard}\n# In Hand: {PlaceInHand}";
        }
    }


    public class DrawTwoCard : Card
    {
        public override CardType TypeOfCard => CardType.DrawTwo;

        public DrawTwoCard(CardColor colorOfCard)
        {
            ColorOfCard = colorOfCard;
        }
    }

    public class DrawFourCard : WildCard
    {
        public override CardType TypeOfCard => CardType.DrawFour;

        
    }


}
