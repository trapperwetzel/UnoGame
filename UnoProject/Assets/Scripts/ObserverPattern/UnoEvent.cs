using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnoEventType {
    CardPlayed,
    CardDrawn,
    UnoCalled,
    GameWon
}

// UnoEvent class for the Observer Pattern
public class UnoEvent
{
    public UnoEventType EventType { get; private set; }
    public object Data { get; private set; }

    public UnoEvent(UnoEventType eventType, object data = null)
    {
        EventType = eventType;
        Data = data;
    }
}
