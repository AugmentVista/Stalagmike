using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameManager gameManager;


    [SerializeField] Button playButton;

    void Update()
    {
        playButton.interactable = DifficultyManager.IsDifficultySelected;
    }
    public void PlayOnClick()
    {
        if (!DifficultyManager.IsDifficultySelected)
        {
            return;
        }
        if (MainMenuUI.activeSelf || OptionsUI.activeSelf) { gameManager.MainMenuPlayGame(); }
    }

    public void ContinueOnClick()
    {
        if (!DifficultyManager.IsDifficultySelected)
        {
            return;
        }
        if (PauseMenuUI.activeSelf || OptionsUI.activeSelf) { gameManager.PauseMenuPlayGame(); }
    }

    public void OptionsOnClick()
    {
        gameManager.OptionsMenu();
    }

    public void OptionsPlayGame()
    {
        if (!DifficultyManager.IsDifficultySelected)
        {
            return;
        }
        if (OptionsUI.activeSelf) { gameManager.OptionsMenuPlayGame(); }
    }

    public void MainMenuOnClick()
    {
        if (OptionsUI.activeSelf || PauseMenuUI.activeSelf) { gameManager.MainMenu(); }
    }
}
