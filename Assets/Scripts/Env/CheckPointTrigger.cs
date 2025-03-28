using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> respawnPoints = new List<GameObject>();

    [SerializeField] private GameObject targetRespawnPoint;

    public Transform newRespawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            // Get a reference to the player gameObject

            if (!targetRespawnPoint.activeSelf) { targetRespawnPoint.SetActive(true); }
            // Ensure the respawn point we want is active

            newRespawnPoint = targetRespawnPoint.transform;
            // Assign a value to newRespawnPoint to our target respawnPoint

            if (player.TryGetComponent(out PlayerController playerController))
            {
                // Tell the playerController to use the new value for its respawn location
                playerController.RespawnPoint = newRespawnPoint.transform;
            }
        }
    }
}
