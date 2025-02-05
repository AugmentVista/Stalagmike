using UnityEngine;
using System;

/// <summary>
/// Manages core game functionality
/// </summary>
public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {PauseGame();}
        
    }

    public void GameStart()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// If the game isn't paused, pause it, if the game is paused, unpause it.
    /// </summary>
    public void PauseGame()
    {
        if (InputManager.pauseInput) 
        {
            UserInterfaceManager. RequestUIUpdate("Paused");
            Time.timeScale = 0;
        }
        else if (!InputManager.pauseInput) 
        {
            UserInterfaceManager. RequestUIUpdate("Game");
            Time.timeScale = 1;
        }
    }
}
