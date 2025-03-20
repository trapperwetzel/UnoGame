using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnoTerminal;

public class CardPlayUIObserver : MonoBehaviour, IObserver {
    
    [SerializeField] private UnoGameManager manager;

    public void OnNotify(UnoEvent gameEvent)
    {
        if (gameEvent.EventType == UnoEventType.CardPlayed)
        {
            // Put code for UI here
            Debug.Log("A Card was played");
        }
    }
}

