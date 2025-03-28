using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameManager gameManager;

    public void MenuOnClick()
    {
        if (PauseMenuUI.activeSelf)
        {
            gameManager.PauseMenuPlayGame();
            
            PauseMenuUI.SetActive(false);
        }
    }

    public void ContinueOnClick()
    {
        if (MainMenuUI.activeSelf)
        {
            gameManager.MainMenuPlayGame();
            MainMenuUI.SetActive(false);
        }
    }
}

