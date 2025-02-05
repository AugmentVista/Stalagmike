using UnityEngine;

/// <summary>
/// Button to continue game from pause menu.
/// Disables pause menu when clicked.
/// </summary>
public class ContinueButton : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuUI;

    public void OnClick()
    {
        if (PauseMenuUI.activeSelf)
        {
            PauseMenuUI.SetActive(false);
            if (Time.timeScale != 1) {Time.timeScale = 1;}
            UserInterfaceManager. RequestUIUpdate("GamePlay");
        }
    }
}
