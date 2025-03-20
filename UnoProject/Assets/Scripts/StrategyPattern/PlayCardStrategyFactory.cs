using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoTerminal;

public class PlayCardStrategyFactory : MonoBehaviour {
    public static IPlayCardStrategy GetStrategy(Card card)
    {
        switch (card.TypeOfCard)
        {
            case CardType.Number:
            return new PlayNumberCardStrategy();
          
            case CardType.Wild:
            return new PlayWildCardStrategy();
           
            case CardType.DrawTwo:
            return new PlayDrawTwoCardStrategy();
            
            case CardType.DrawFour:
            return new PlayDrawFourCardStrategy();
            
            case CardType.Skip:
            return new PlaySkipCardStrategy();
            
            case CardType.Reverse:
            return new PlayReverseCardStrategy();

            default:
            throw new System.Exception("Can't find card type: " + card.TypeOfCard);
        }



    }
}


    

