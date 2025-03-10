using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal {
    public class CardFactory {
        
        public static Card CreateCard(CardType type, CardColor color = CardColor.Red, int number = 0, int placeInHand = 0)
        {
            Card card = null;

            switch (type)
            {
                case CardType.Number:
                card = new NumberCard(color, number);
                break;
                case CardType.Skip:
                card = new SkipCard(color);
                break;
                case CardType.DrawTwo:
                card = new DrawTwoCard(color);
                break;
                case CardType.Reverse:
                card = new ReverseCard(color);
                break;
                case CardType.Wild:
                card = new WildCard();
                break;
                case CardType.DrawFour:
                card = new DrawFourCard();
                break;
                default:
                throw new ArgumentException($"Unsupported card type: {type}");
            }

            card.PlaceInHand = placeInHand;
            return card;
        }

        
        /// Creates a NumberCard with the specified color and number.
        
        public static NumberCard CreateNumberCard(CardColor color, int number, int placeInHand = 0)
        {
            var card = new NumberCard(color, number);
            card.PlaceInHand = placeInHand;
            return card;
        }

        
        /// Creates a SkipCard with the specified color.
        
        public static SkipCard CreateSkipCard(CardColor color, int placeInHand = 0)
        {
            var card = new SkipCard(color);
            card.PlaceInHand = placeInHand;
            return card;
        }

        
        /// Creates a DrawTwoCard with the specified color.
        
        public static DrawTwoCard CreateDrawTwoCard(CardColor color, int placeInHand = 0)
        {
            var card = new DrawTwoCard(color);
            card.PlaceInHand = placeInHand;
            return card;
        }

        
        /// Creates a ReverseCard with the specified color.
        
        public static ReverseCard CreateReverseCard(CardColor color, int placeInHand = 0)
        {
            var card = new ReverseCard(color);
            card.PlaceInHand = placeInHand;
            return card;
        }

        
        /// Creates a WildCard instance.
        
        public static WildCard CreateWildCard(int placeInHand = 0)
        {
            var card = new WildCard();
            card.PlaceInHand = placeInHand;
            return card;
        }

        
        /// Creates a DrawFourCard instance.
        
        public static DrawFourCard CreateDrawFourCard(int placeInHand = 0)
        {
            var card = new DrawFourCard();
            card.PlaceInHand = placeInHand;
            return card;
        }
    }
}
