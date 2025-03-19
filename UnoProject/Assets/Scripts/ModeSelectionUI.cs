using UnityEngine;
using UnityEngine.UI;

public class ModeSelectionUI : MonoBehaviour {
    public UnoGameManager gameManager;
    public GameObject modeSelectionPanel;

    // Reference your buttons (if you want to wire them up via code)
    public Button stackingModeButton;
    public Button classicModeButton;

    void Start()
    {
        // Attach listeners if not done in Inspector
        stackingModeButton.onClick.AddListener(OnStackingModeChosen);
        classicModeButton.onClick.AddListener(OnClassicModeChosen);

        // Ensure the panel is visible at start
        modeSelectionPanel.SetActive(true);
    }

    public void OnStackingModeChosen()
    {
        gameManager.SwitchToStackingMode();
        modeSelectionPanel.SetActive(false);
        //spawn the cards
        gameManager.InitializeGame();
    }

    public void OnClassicModeChosen()
    {
        gameManager.SwitchToClassicMode();
        modeSelectionPanel.SetActive(false);
        // Spawn the cards
        gameManager.InitializeGame();
    }
}
