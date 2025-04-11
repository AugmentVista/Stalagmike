using UnityEngine;

/// <summary>
/// Manages core game functionality
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] UserInterfaceManager UiManager;

    void Start()
    {
        UiManager.RequestUIUpdate("MainMenu");
        FreezeTime();
    }
    void Update()
    {
        PauseGame();
    }

    /// <summary>
    /// Play button in main menu calls this method to change state to gameplay and unfreezes time
    /// </summary>
    public void MainMenuPlayGame()
    {
        if (UiManager.uiState == UserInterfaceManager.UserInterfaceState.MainMenu)
        {
            UiManager.RequestUIUpdate("GamePlay");
            UnfreezeTime();
        }
    }
    public void MainMenu()
    {
        if (UiManager.uiState != UserInterfaceManager.UserInterfaceState.MainMenu)
        {
            UiManager.RequestUIUpdate("MainMenu");
            FreezeTime();
        }
    }
    /// <summary>
    /// Continue button in pause menu calls this method to change state to gameplay and unfreeze time
    /// </summary>
    public void PauseMenuPlayGame()
    {
        if (UiManager.uiState == UserInterfaceManager.UserInterfaceState.MainMenu)
        {
            UiManager.RequestUIUpdate("GamePlay");
            UnfreezeTime();
        }
    }
    /// <summary>
    /// Options button in pause or main menu calls this method to change state to Options and freeze time
    /// </summary>
    public void OptionsMenu()
    {
        if (UiManager.uiState != UserInterfaceManager.UserInterfaceState.Options)
        {
            UiManager.RequestUIUpdate("Options");
            FreezeTime();
        }
    }
    /// <summary>
    /// Play button in options calls this method to change state to gameplay and unfreezes time
    /// </summary>
    public void OptionsMenuPlayGame()
    {
        if (UiManager.uiState == UserInterfaceManager.UserInterfaceState.Options)
        {
            UiManager.RequestUIUpdate("GamePlay");
            UnfreezeTime();
        }
    }
    /// <summary>
    /// If the game isn't paused, pause it; if the game is paused, unpause it.
    /// Pausing is only allowed when the game state is 'GamePlay'.
    /// </summary> 
    public void PauseGame()
    {
        // Ensure that pausing only happens if the current UI state is "GamePlay"
        if (UiManager.uiState == UserInterfaceManager.UserInterfaceState.MainMenu || UiManager.uiState == UserInterfaceManager.UserInterfaceState.Options)
        {
            return;
        }

        // Proceed with normal pause/unpause if the UI state is "GamePlay"
        if (InputManager.pauseInput && !isPaused)
        {
            UiManager.RequestUIUpdate("Paused");
            FreezeTime();
        }
        else if (InputManager.pauseInput && isPaused)
        {
            UiManager.RequestUIUpdate("GamePlay");
            UnfreezeTime();
        }
    }
    /// <summary>
    /// Sets timescale to 1 if it isn't 1
    /// </summary>
    public void UnfreezeTime()
    {
         if (Time.timeScale != 1) {Time.timeScale = 1; isPaused = false;}
    }

    /// <summary>
    /// Sets timescale to 0 if it isn't 0
    /// </summary>
    public void FreezeTime()
    {
        if (Time.timeScale != 0) {Time.timeScale = 0; isPaused = true;}
    }
}