using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameManager gameManager;

    public void PlayOnClick()
    {
        if (MainMenuUI.activeSelf || OptionsUI.activeSelf) { gameManager.MainMenuPlayGame(); }
    }

    public void ContinueOnClick()
    {
        if (PauseMenuUI.activeSelf || OptionsUI.activeSelf) { gameManager.PauseMenuPlayGame(); }
    }

    public void OptionsOnClick()
    {
        if (PauseMenuUI.activeSelf || MainMenuUI.activeSelf) { gameManager.OptionsMenu(); }
    }

    public void OptionsPlayGame()
    {
        if (OptionsUI.activeSelf) { gameManager.OptionsMenuPlayGame(); }
    }
}
