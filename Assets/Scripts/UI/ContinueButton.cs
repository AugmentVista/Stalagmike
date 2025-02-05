using UnityEngine;

/// <summary>
/// Button to continue game from pause menu.
/// Disables pause menu when clicked.
/// </summary>
public class ContinueButton : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
         if (PauseMenuUI.activeSelf)
        {
            PauseMenuUI.SetActive(false);
        }
    }
}
