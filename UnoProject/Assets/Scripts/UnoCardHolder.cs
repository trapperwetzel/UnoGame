using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnoTerminal; // So we can reference Card, Player

[RequireComponent(typeof(Collider2D))] //  using 2D collisions
public class UnoCardHolder : MonoBehaviour {
    public Card cardData;
    public UnoGameManager manager;
    public Player ownerPlayer;

    
    private void OnMouseDown()
    {
        // When the user clicks the card
        if (manager != null && cardData != null && ownerPlayer != null)
        {
            manager.OnCardClicked(cardData, ownerPlayer);
        }
    }
}

