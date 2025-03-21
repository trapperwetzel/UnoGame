using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnoTerminal;

public class CardPlayUIObserver : MonoBehaviour, IObserver {
    
    [SerializeField] private UnoGameManager manager;
    void Start()
    {
        // Find the UnoGameManager (assuming it's tagged "GameManager")
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UnoGameManager>();

        // Register this observer with the manager
        manager.AddObserver(this);
    }
    public void OnNotify(UnoEvent gameEvent)
    {
        if (gameEvent.EventType == UnoEventType.CardPlayed)
        {
            // Put code for UI here
            Debug.Log("A Card was played");
        }
    }
}

