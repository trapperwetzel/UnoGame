using UnityEngine;
using UnityEngine.UI;
using UnoTerminal;  // To access Card, CardColor, UnoEvent, etc.

public class ColorSelectionObserver : MonoBehaviour, IObserver {
    [SerializeField] private GameObject colorSelectionPanel;
    [SerializeField] private Button redButton;
    [SerializeField] private Button blueButton;
    [SerializeField] private Button greenButton;
    [SerializeField] private Button yellowButton;

    // Reference to the game manager to notify about the selected color.
    private UnoGameManager gameManager;

    void Start()
    {
        // Make sure the panel is hidden initially.
        colorSelectionPanel.SetActive(false);

        // Find the game manager (assumes it has the tag "GameManager").
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UnoGameManager>();
        gameManager.AddObserver(this);
        // Hook up button events.
        redButton.onClick.AddListener(() => OnColorChosen(CardColor.Red));
        blueButton.onClick.AddListener(() => OnColorChosen(CardColor.Blue));
        greenButton.onClick.AddListener(() => OnColorChosen(CardColor.Green));
        yellowButton.onClick.AddListener(() => OnColorChosen(CardColor.Yellow));
    }

    // This method is called when an event is received.
    public void OnNotify(UnoEvent gameEvent)
    {
        Debug.Log("CardColorSelectorDEBUG!");
        // We check if the event indicates a card was played.
        // For our case, we want to handle when a Draw Four card is played.
        if (gameEvent.EventType == UnoEventType.CardPlayed)
        {
            Card playedCard = gameEvent.Data as Card;
            if (playedCard.TypeOfCard == CardType.Wild || playedCard.TypeOfCard == CardType.DrawFour)
            {
                // Show the Choose Color UI when a Draw Four is played.
                ShowColorSelectionUI();
            }
        }
    }

    // Display the color selection panel.
    private void ShowColorSelectionUI()
    {
        colorSelectionPanel.SetActive(true);
        Debug.Log("Color selection UI launched.");
    }

    // Called when a color button is clicked.
    private void OnColorChosen(CardColor chosenColor)
    {
        // Hide the UI.
        colorSelectionPanel.SetActive(false);
        Debug.Log("Color selected: " + chosenColor);

        // Notify the game manager about the selected color.
        gameManager.SetWildColor(chosenColor);

        
    }
}
