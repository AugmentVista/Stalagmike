using UnityEngine;
using System;

/// <summary>
/// Manages core game functionality
/// </summary>
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void GameStart()
    {
        throw new NotImplementedException();
    }

    public void PauseGame()
    {
        // If the game isn't paused, pause it, if the game is paused, unpause it.
        if (Input.GetKey(KeyCode.Escape))
        {
            if (InputManager.pauseInput) {Time.timeScale = 0;}
            else if (!InputManager.pauseInput) {Time.timeScale = 1;}
        }
    }
}
