using UnityEngine;
using UnityEngine.UI;
using UnoTerminal; // For accessing Uno-related enums and classes.

public class GameWinObserver : MonoBehaviour, IObserver {
    [SerializeField] private GameObject winPanel; // UI Panel to display winner message
    [SerializeField] private Text winText; // UI Text to show winner name
    [SerializeField] private UnoGameManager gameManager;

    void Start()
    {
        // Find and register with the Game Manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UnoGameManager>();
        gameManager.AddObserver(this);

        // Ensure the win panel is hidden initially
        winPanel.SetActive(false);
    }

    public void OnNotify(UnoEvent gameEvent)
    {
        if (gameEvent.EventType == UnoEventType.GameWon)
        {
            Player winner = gameEvent.Data as Player;
            if (winner != null)
            {
                DisplayWinScreen(winner);
            }
        }
    }

    private void DisplayWinScreen(Player winner)
    {
        winPanel.SetActive(true);
        winText.text = $"{winner.Name} Wins!";
        Debug.Log($"{winner.Name} has won the game!");
    }
}