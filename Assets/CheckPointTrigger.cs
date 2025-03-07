using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> respawnPoints = new List<GameObject>();

    [SerializeField] private GameObject targetRespawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!targetRespawnPoint.activeSelf) { targetRespawnPoint.SetActive(true); }

            foreach (GameObject respawnPoint in respawnPoints)
            {
                if (respawnPoint != targetRespawnPoint)
                {
                    respawnPoint.SetActive(false);
                }
            }
        }
    }
}
