using UnityEngine;
using UnityEngine.UI;
using UnoTerminal;  // For accessing Uno-related enums and classes.

public class CallUnoUIObserver : MonoBehaviour, IObserver {
    
    [SerializeField] private Button callUnoButton;
    [SerializeField] private UnoGameManager gameManager;

    void Start()
    {
        // Find the game manager and register as an observer
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UnoGameManager>();
        gameManager.AddObserver(this);

        
        callUnoButton.gameObject.SetActive(false);

        // Ensure button click is assigned
        callUnoButton.onClick.AddListener(OnCallUnoClicked);
    }

    public void OnNotify(UnoEvent gameEvent)
    {
        
        if (gameEvent.EventType == UnoEventType.UnoCalled)
        {
            // If uno event is called in game manager show the uno button 
            Debug.Log("UNO was called by a player!");
            CheckForUnoButton();
            // If UNO was called, we might flash a visual alert or log it in UI
           // ShowUnoAlert();
        }
    }

    /*
    private void UpdateTurnUI()
    {
        //  Check if it's Player1’s turn
        bool isPlayerTurn = gameManager.gamePlay.CurrentPlayer == gameManager.gamePlay.Player1;

        // Show or hide the "Your Turn" UI
        currentTurnUI.SetActive(isPlayerTurn);

        //  Check if the UNO button should be displayed
        CheckForUnoButton();
    }
    */


    private void CheckForUnoButton()
    {
        bool isPlayerTurn = gameManager.gamePlay.CurrentPlayer == gameManager.gamePlay.Player1;
        bool shouldShowUnoButton = isPlayerTurn && gameManager.gamePlay.CurrentPlayer.Hand.Count == 1;

        callUnoButton.gameObject.SetActive(shouldShowUnoButton);
    }

    public void OnCallUnoClicked()
    {
        Debug.Log("UNO Called!");
        callUnoButton.gameObject.SetActive(false);

        
        
    }
}