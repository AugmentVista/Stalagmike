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
        
        // Get the distance to the mouse
        Vector2 direction = (mousePosition - playerPosition).normalized;

        // Get the angle from the player to the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // position and rotation of the player's weapon relative to player
        weapon.transform.position = playerPosition + direction * weaponDistance;
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}