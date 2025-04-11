using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HpSpawn : MonoBehaviour
{
    [SerializeField] GameObject HealthPickupPrefab;

    // Should have 4 possible locations for healh to spawn at per zone.
    // Entire list should contain 4 * number of zones that exist.
    [SerializeField] List<Transform> HealthPickupSpawnLocationsZone1;
    [SerializeField] List<Transform> HealthPickupSpawnLocationsZone2;


    public enum DifficultySetting
    {
        none,
        easy,
        medium,
        hard,
    }
    
    DifficultySetting difficulty;
    int reduction;
    int amountToSpawnZone1;
    int amountToSpawnZone2;

    public void ApplyDifficulty(DifficultySetting selectedDifficulty)
    {
        difficulty = selectedDifficulty;
        DistributeHealthAcrossMap();
    }

    void DistributeHealthAcrossMap()
    {
        Debug.Log($"difficulty is set to: {difficulty}");

        switch (difficulty)
        {
            case DifficultySetting.easy:
                {
                    reduction = 1;
                }
                break;
            case DifficultySetting.medium:
                {
                    reduction = 2;
                }
                break;
            case DifficultySetting.hard:
                {
                    reduction = 3;
                }
                break;
            // If a difficulty is not selected, all hearts spawn.    
            default:
                {
                    amountToSpawnZone1 = HealthPickupSpawnLocationsZone1.Count;
                    amountToSpawnZone2 = HealthPickupSpawnLocationsZone2.Count;
                    Debug.Log("Difficulty not selected, spawning hearts at all valid locations");
                }
                break;
        }
        amountToSpawnZone1 = HealthPickupSpawnLocationsZone1.Count - reduction;
        amountToSpawnZone2 = HealthPickupSpawnLocationsZone2.Count - reduction; 
        CreateHealthPickup(amountToSpawnZone1, amountToSpawnZone2);
    } 

    void CreateHealthPickup(int amountToSpawnZone1, int amountToSpawnZone2)
    {
        if (amountToSpawnZone1 > 0)
        {
            for (int i = 0; i < amountToSpawnZone1; i++)
            {
                Instantiate(HealthPickupPrefab, HealthPickupSpawnLocationsZone1[i].position, quaternion.identity);
            }
        }
        if (amountToSpawnZone2 > 0)
        {
            for (int j = 0; j < amountToSpawnZone2; j++)
            {
                Instantiate(HealthPickupPrefab, HealthPickupSpawnLocationsZone2[j].position, quaternion.identity);
            }
        }
    }
}