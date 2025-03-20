using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpriteManager : MonoBehaviour {
    // Dictionary to map card names/IDs to sprites
    public Dictionary<string, Sprite> cardSprites = new Dictionary<string, Sprite>();

    // Store references in the inspector or load them programmatically
    // can drag all the sprites from the Project window onto a serialized array in the Inspector.
    [SerializeField] private Sprite[] allSprites;
    [SerializeField] private GameObject cardPrefab;
    void Awake()
    {
        // Populate the dictionary with references to each sprite.
        // This assumes each sprite’s name matches something like “Red_0”, “Red_Draw2”, “Wild_DrawFour”, etc.
        foreach (var sprite in allSprites)
        {
            cardSprites[sprite.name] = sprite;
        }
    }
   
    // Method to create a card by name
    public GameObject CreateCard(string cardName, Vector3 position, Quaternion rotation)
    {
        
        GameObject newCard = Instantiate(cardPrefab, position, rotation);

        // Get the SpriteRenderer and assign the correct sprite
        SpriteRenderer sr = newCard.GetComponent<SpriteRenderer>();
        if (cardSprites.ContainsKey(cardName))
        {
            sr.sprite = cardSprites[cardName];
        }
        else
        {
            Debug.LogWarning($"Card sprite not found for: {cardName}");
        }

        return newCard;
    }

    public Sprite GetSpriteByName(string spriteName)
    {
        if (cardSprites.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite '{spriteName}' not found in CardSpriteManager!");
            return null;
        }
    }

}
