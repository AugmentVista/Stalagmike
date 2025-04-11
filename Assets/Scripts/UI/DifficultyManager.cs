using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private DifficultyToggle[] difficultyToggles;

    private DifficultyToggle currentSelected;

    private void Awake()
    {
        // Tell each button who the manager is
        foreach (var toggle in difficultyToggles)
        {
            toggle.manager = this;
        }
    }

    public void DifficultySettingsOnClick()
    {
        foreach (var toggle in difficultyToggles)
        {
            toggle.gameObject.SetActive(true);
        }
    }

    public void SelectDifficulty(DifficultyToggle selectedToggle)
    {
        // Deselect previous
        if (currentSelected != null)
        {
            currentSelected.SetSelected(false);
        }

        // Select new
        currentSelected = selectedToggle;
        currentSelected.SetSelected(true);
    }
}
