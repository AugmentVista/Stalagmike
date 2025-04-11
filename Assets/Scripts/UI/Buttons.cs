using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameManager gameManager;

    public void PlayOnClick()
    {
        if (MainMenuUI.activeSelf || OptionsUI.activeSelf)
        {
            gameManager.MainMenuPlayGame();
            //MainMenuUI.SetActive(false);
            //OptionsUI.SetActive(false);
        }
    }

    public void ContinueOnClick()
    {
        if (PauseMenuUI.activeSelf || OptionsUI.activeSelf)
        {
            gameManager.PauseMenuPlayGame();
            //PauseMenuUI.SetActive(false);
            //OptionsUI.SetActive(false);
        }
    }

    public void OptionsOnClick()
    {
        if (PauseMenuUI.activeSelf || MainMenuUI.activeSelf)
        {
            gameManager.OptionsMenu();
            //PauseMenuUI.SetActive(false);
            //MainMenuUI.SetActive(false);
        }
    }

    public void OptionsPlayGame()
    {
        if (OptionsUI.activeSelf)
        { 
            gameManager.OptionsMenuPlayGame();
            //MainMenuUI.SetActive(false);
            //OptionsUI.SetActive(false);
        }
    }

}
