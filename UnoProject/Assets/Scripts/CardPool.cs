using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour {
    public static CardPool Instance { get; private set; }

    // Assign your card prefab in the Inspector
    [SerializeField] private GameObject cardPrefab;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        // Simple Singleton approach
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject GetCard()
    {
        // Reuse a card if available
        if (pool.Count > 0)
        {
            GameObject cardObj = pool.Dequeue();
            cardObj.SetActive(true);
            return cardObj;
        }
        // Otherwise instantiate a new one
        return Instantiate(cardPrefab);
    }

    public void ReturnCard(GameObject cardObj)
    {
        // Disable & enqueue for reuse
        cardObj.SetActive(false);
        pool.Enqueue(cardObj);
    }
}