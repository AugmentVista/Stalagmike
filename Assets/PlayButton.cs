using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        // Disables the Main Menu panel and child objects that exist infront of gameplay.
        // GameStart does not do anything as of Feburary 5th 2025.
        if (MainMenuUI.activeSelf)
        {
            MainMenuUI.SetActive(false);
            gameManager.GameStart();
        }
    }
}
