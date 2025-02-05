using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.pauseInput)
        {
            PauseGame();
        }
    }

    public void GameStart()
    {
        throw new NotImplementedException();
    }

    public void PauseGame()
    {
        // If the game isn't paused, pause it, if the game is paused, unpause it.
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}
