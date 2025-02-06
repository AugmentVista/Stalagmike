using UnityEngine;
using System;
using Unity.VisualScripting;

/// <summary>
/// Handles all high level interaction with UserInterface elements.
/// Has a static instance for other classes to provide it arguements without needing a direct reference.
/// Ensures that only one UserInterface screen is active at a time.
/// </summary>
public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance; // Static instance as there should only be one UserInterfaceManager
    
    public GameObject emptyUI;
    public GameObject mainMenuUI;
    public GameObject pausedUI;
    public GameObject gamePlayUI;
    public GameObject optionsUI;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    
    public enum UserInterfaceState
    {
        EmptyUI,
        MainMenu,
        Paused,
        GamePlay,
        Options,
        GameWin,
        GameLose,
    }

    public UserInterfaceState uiState;

    private void Awake()
    {
        // Singleton logic for UserInterfaceManager instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        uiState = UserInterfaceState.MainMenu;
        UpdateUI(uiState);
    }

    public static void RequestUIUpdate(string desiredScreenName)
    {
        // Try to parse the string argument into an enum value, upper/lower case doesn't matter
        if (Enum.TryParse(desiredScreenName, true, out UserInterfaceState state))
        {
            // Update the UI if the new state matches
            instance.UpdateUI(state);
        }
        else
        {
            // If it doesn't match, display the attempted string as a debug message
            Debug.LogWarning($"The UI state: {desiredScreenName}, doesn't exist");
        }
    }

    public void UpdateUI(UserInterfaceState state)
    {
        switch (state)
        {
            case UserInterfaceState.MainMenu:
                MainMenuUI();
                break;
            case UserInterfaceState.GamePlay:
                GamePlayUI();
                break;
            case UserInterfaceState.Paused:
                PausedUI();
                break;
            case UserInterfaceState.GameWin:
                GameWinUI();
                break;
            case UserInterfaceState.GameLose:
                GameOverUI();
                break;
            default:
                NoUI();
                break;
        }
    }

    private void NoUI()
    {
        HideAllUI(emptyUI);
    }
    private void MainMenuUI()
    {
        HideAllUI(mainMenuUI);
    }
    public void GamePlayUI()
    {
        HideAllUI(gamePlayUI);
    }
    public void OptionsUI()
    {
        HideAllUI(optionsUI);
    }
    protected void GameWinUI()
    {
        HideAllUI(gameWinUI);
    }
    protected void GameOverUI()
    {
        HideAllUI(gameOverUI);
    }
    public void PausedUI()
    {
        HideAllUI(pausedUI);
    }

    public void HideAllUI(GameObject ActiveUI)
    {
        emptyUI.SetActive(false);
        mainMenuUI.SetActive(false);
        gamePlayUI.SetActive(false);
        optionsUI.SetActive(false);
        pausedUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        ActiveUI.SetActive(true);
    }
}
