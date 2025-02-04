using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public GameObject emptyUI;
    public GameObject mainMenuUI;
    public GameObject pausedUI;
    public GameObject gamePlayUI;
    public GameObject optionsUI;
    public GameObject gameOverUI;
    public GameObject gameWinUI;

    public enum UserInterfaceState
    {
    MainMenu,
    GamePlay,
    Paused,
    GameWin,
    GameLose,
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