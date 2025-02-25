using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameManager gameManager;

    public void PlayOnClick()
    {
        // Disables the Main Menu panel and child objects that exist infront of gameplay.
        // GameStart does not do anything as of Feburary 5th 2025.
        if (MainMenuUI.activeSelf)
        {
            gameManager.MainMenuPlayGame();
            MainMenuUI.SetActive(false);
        }
    }

    public void ContinueOnClick()
    {
        if (PauseMenuUI.activeSelf)
        {
            gameManager.PauseMenuPlayGame();
            PauseMenuUI.SetActive(false);
        }
    }
}
