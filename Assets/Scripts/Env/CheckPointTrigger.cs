using UnityEngine;
using System.Collections.Generic;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> respawnPoints = new List<GameObject>();

    [SerializeField] private GameObject targetRespawnPoint;

    public Transform newRespawnPoint;

    [Header("Flag Sprites for Each Checkpoint")]
    [SerializeField] private List<Sprite> limpFlags = new List<Sprite>();   // 3 Limp flag sprites
    [SerializeField] private List<Sprite> flowingFlags = new List<Sprite>(); // 3 Flowing flag sprites

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;

            // Ensure the target respawn point is active
            if (!targetRespawnPoint.activeSelf)
            {
                targetRespawnPoint.SetActive(true);
            }

            // Set new respawn point
            newRespawnPoint = targetRespawnPoint.transform;

            // Update player's respawn point
            if (player.TryGetComponent(out PlayerController playerController))
            {
                playerController.RespawnPoint = newRespawnPoint;
            }

            // Swap sprite of targetRespawnPoint to the corresponding flowing flag
            UpdateFlagSprite(targetRespawnPoint, true);

            // Reset other flags to their limp versions
            ResetOtherFlags(targetRespawnPoint);
        }
    }

    private void UpdateFlagSprite(GameObject checkpoint, bool isFlowing)
    {
        SpriteRenderer flagRenderer = checkpoint.GetComponentInChildren<SpriteRenderer>();

        if (flagRenderer != null)
        {
            int index = respawnPoints.IndexOf(checkpoint);
            if (index >= 0 && index < respawnPoints.Count)
            {
                flagRenderer.sprite = isFlowing ? flowingFlags[index] : limpFlags[index];
            }
        }
    }

    private void ResetOtherFlags(GameObject activeCheckpoint)
    {
        for (int i = 0; i < respawnPoints.Count; i++)
        {
            GameObject respawnPoint = respawnPoints[i];

            if (respawnPoint != activeCheckpoint)
            {
                SpriteRenderer flagRenderer = respawnPoint.GetComponentInChildren<SpriteRenderer>();
                if (flagRenderer != null && i < limpFlags.Count)
                {
                    flagRenderer.sprite = limpFlags[i];
                }
            }
        }
    }
}

