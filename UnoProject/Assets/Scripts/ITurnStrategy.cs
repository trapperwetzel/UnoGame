using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoTerminal;

public interface ITurnStrategy
{
    void HandleTurn(UnoGameManager gameManager);   
}

public class ClassicTurnStrategy : ITurnStrategy {
    public void HandleTurn(UnoGameManager gameManager)
    {
        gameManager.NextPlayer();
    }
}

public class StackingTurnStrategy : ITurnStrategy {
    public void HandleTurn(UnoGameManager gameManager)
    {
        if (gameManager.gamePlay.CurrentCard.TypeOfCard == CardType.DrawTwo && gameManager.gamePlay.CurrentPlayer.HasDrawTwoCard())
        {
            Debug.Log($"{gameManager.gamePlay.CurrentPlayer.Name} stacked a DrawTwo!");
            gameManager.gamePlay.CurrentPlayer.PlayDrawTwoCard();
        }
        else
        {
            int stackedDraw = gameManager.GetStackedDrawCount();
            gameManager.gamePlay.CurrentPlayer.DrawCards(stackedDraw, gameManager.gamePlay);
            Debug.Log($"{gameManager.gamePlay.CurrentPlayer.Name} drew {stackedDraw} cards!");
        }

        gameManager.NextPlayer();
    }
}


