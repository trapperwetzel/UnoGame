using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoTerminal;

// Start of the playcard strategy pattern.
// Used to encapsulate the behavior of each card. 
public interface IPlayCardStrategy 
{
    public void Execute(Card card, GamePlay gamePlay);
}


public class PlayNumberCardStrategy : IPlayCardStrategy {
    public void Execute(Card card, GamePlay gamePlay)
    {
        gamePlay.PlayCard(gamePlay.CurrentCard, card);
        gamePlay.SwitchPlayer();
        Debug.Log("Played Number Card");

    }
}

public class PlayWildCardStrategy : IPlayCardStrategy {
    public void Execute(Card card, GamePlay gamePlay)
    {
        gamePlay.PlayCard(gamePlay.CurrentCard, card);
        gamePlay.SwitchPlayer();
        Debug.Log("Played Wild Card");
    }
}

public class PlayDrawTwoCardStrategy : IPlayCardStrategy {
    
    public void Execute(Card card, GamePlay gamePlay)
    {
        gamePlay.PlayCard(gamePlay.CurrentCard, card);
        gamePlay.DrawTwo();
    }
}

public class PlayDrawFourCardStrategy : IPlayCardStrategy {

    public void Execute(Card card, GamePlay gamePlay)
    {
        gamePlay.PlayCard(gamePlay.CurrentCard, card);
        gamePlay.DrawFour();
    }
}

public class PlaySkipCardStrategy : IPlayCardStrategy {

    public void Execute(Card card, GamePlay gamePlay)
    {
        gamePlay.PlayCard(gamePlay.CurrentCard, card);
        
    }
}

public class PlayReverseCardStrategy : IPlayCardStrategy {

    public void Execute(Card card, GamePlay gamePlay)
    {
        gamePlay.PlayCard(gamePlay.CurrentCard, card);
        
    }
}