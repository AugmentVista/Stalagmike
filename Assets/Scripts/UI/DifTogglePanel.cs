using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifTogglePanel : MonoBehaviour
{
    public GameObject DifficultyPanel;

    public void OnClick()
    {
        DifficultyPanel.SetActive(!DifficultyPanel.activeSelf);
    }
}

