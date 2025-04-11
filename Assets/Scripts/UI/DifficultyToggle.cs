using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyToggle : MonoBehaviour
{
    public GameObject checkmark;
    public GameObject xMark;

    [HideInInspector]
    public DifficultyManager manager;

    public void SetSelected(bool selected)
    {
        checkmark.SetActive(selected);
        xMark.SetActive(!selected);
    }

    public void OnClick()
    {
        manager.SelectDifficulty(this);
    }
}
