using Assets.Scripts.Entity.Foe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private DifficultyToggle easyToggle;
    [SerializeField] private DifficultyToggle mediumToggle;
    [SerializeField] private DifficultyToggle hardToggle;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject difficultyPanel;

    [SerializeField] private DifficultySettings easySettings;
    [SerializeField] private DifficultySettings mediumSettings;
    [SerializeField] private DifficultySettings hardSettings;

    private DifficultyToggle selectedToggle;

    private void Start()
    {
        confirmButton.SetActive(false);

        // Make sure toggles know about the manager
        easyToggle.manager = this;
        mediumToggle.manager = this;
        hardToggle.manager = this;
    }

    public void SelectDifficulty(DifficultyToggle toggle)
    {
        selectedToggle = toggle;

        // Enable the correct one, disable the others
        easyToggle.SetSelected(toggle == easyToggle);
        mediumToggle.SetSelected(toggle == mediumToggle);
        hardToggle.SetSelected(toggle == hardToggle);

        confirmButton.SetActive(true);
    }

    public void ConfirmDifficulty()
    {
        FoeSpawner[] allSpawners = FindObjectsOfType<FoeSpawner>();
        HpSpawn[] allHpSpawns = FindObjectsOfType<HpSpawn>();

        DifficultySettings chosenSettings = null;
        HpSpawn.DifficultySetting hpSetting = HpSpawn.DifficultySetting.none;

        if (selectedToggle == easyToggle)
        {
            chosenSettings = easySettings;
            hpSetting = HpSpawn.DifficultySetting.easy;
        }
        else if (selectedToggle == mediumToggle)
        {
            chosenSettings = mediumSettings;
            hpSetting = HpSpawn.DifficultySetting.medium;
        }
        else if (selectedToggle == hardToggle)
        {
            chosenSettings = hardSettings;
            hpSetting = HpSpawn.DifficultySetting.hard;
        }

        // Apply to all foe spawners
        foreach (FoeSpawner spawner in allSpawners)
        {
            spawner.ApplyDifficultySettings(chosenSettings);
        }

        // Apply to all health spawns
        foreach (HpSpawn hp in allHpSpawns)
        {
            hp.ApplyDifficulty(hpSetting);
        }

        Debug.Log($"Confirmed difficulty: {selectedToggle.gameObject.name}");

        difficultyPanel.SetActive(false);
        confirmButton.SetActive(false);
    }

}