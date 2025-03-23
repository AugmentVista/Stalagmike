using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWeapon : MonoBehaviour
{
    public GameObject weapon; // Reference to the weapon sprite
    public float weaponDistance = 0.8f; // Fixed distance from the player
    public float maxAttackRange = 1.0f; // 1 tile distance for attack

    void Update()
    {
        Vector2 playerPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Calculate direction to the mouse
        Vector2 direction = (mousePosition - playerPosition).normalized;

        // Calculate angle to rotate the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Rotate the player to face the mouse
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Position the weapon sprite at a fixed distance
        weapon.transform.position = playerPosition + direction * weaponDistance;

        // Rotate the weapon sprite to face the mouse
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Check if we can attack
        float distanceToMouse = Vector2.Distance(playerPosition, mousePosition);
        if (distanceToMouse <= maxAttackRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(playerPosition, direction, maxAttackRange);

            // Connect to TileBreakableSystem
        }
    }
}