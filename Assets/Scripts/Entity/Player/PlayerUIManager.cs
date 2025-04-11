using Assets.Scripts;
using Assets.Scripts.Entity.Util;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PlayerUIManager : MonoBehaviour
{
    private class HeartPair
    {
        public GameObject leftHeart;
        public GameObject rightHeart;
    }

    private List<HeartPair> heartPairs = new List<HeartPair>();

    [SerializeField] private HealthSystem healthSystem;

    private List<GameObject> listOfFullHearts = new List<GameObject>();

    [SerializeField] private GameObject fullHeartPrefab;

    GameObject player;

    int lastKnownMaxHP;
    int lastKnownHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && player.TryGetComponent(out HealthSystem playerHealthSystem))
        {
            healthSystem = playerHealthSystem;
            lastKnownMaxHP = healthSystem.maxHp;
            lastKnownHealth = healthSystem.health;
        }
        else
        {
            Debug.LogWarning("Player or HealthSystem not found by UI script.");
        }
    }

    public void SetMaxUIHealth()
    {
        int fullHeartCount = healthSystem.maxHp / 2;
        Vector3 heartPositions = new Vector3(0, 0, 0);

        for (int i = 0; i < fullHeartCount; i++)
        {
            heartPositions.x *= i * 100;
            GameObject fullheart = Instantiate(fullHeartPrefab, heartPositions, Quaternion.identity);
            listOfFullHearts.Add(fullheart);
        }
    }

    private void UpdateMaxHealth()
    {
        if (lastKnownMaxHP + 1 < healthSystem.maxHp)
        {
            int deltaMaxHealth = healthSystem.maxHp - lastKnownMaxHP;
            lastKnownMaxHP = healthSystem.maxHp;
            Vector3 heartPositions = new Vector3(0, 0, 0);

            for (int i = 0; i < deltaMaxHealth; i++)
            {
                // Use the amount of hearts to determine the position of the next heart.
                heartPositions.x = listOfFullHearts.Count * 100 + i * 100;
                GameObject fullheart = Instantiate(fullHeartPrefab, heartPositions, Quaternion.identity);
                listOfFullHearts.Add(fullheart);

                // Get reference to the halves of the heart that was just created
                Transform left = fullheart.transform.Find("Left Heart");
                Transform right = fullheart.transform.Find("Right Heart");

                if (left == null || right == null)
                {
                    Debug.LogWarning($"Heart prefab is missing expected child objects: 'Left Heart' or 'Right Heart' ");
                    continue;
                }

                // create an instance of HeartPair class named pair
                HeartPair pair = new HeartPair
                {
                    leftHeart = left.gameObject,
                    rightHeart = right.gameObject
                };

                // add pair to a list of heartPairs
                heartPairs.Add(pair);
            }
        }
    }

    private void Update()
    {
        UpdateHealthValue();
    }

    private void UpdateHealthValue()
    {
        // if delta health is postive player gained health, if negative player lost health
        int deltaHealth = healthSystem.health - lastKnownHealth;

        if (deltaHealth != 0)
        {
            if (deltaHealth > 0)
                ApplyHealing(deltaHealth); 

            if (deltaHealth < 0)
                ApplyDamage(deltaHealth);

            lastKnownHealth = healthSystem.health;
        }
    }

    public void ApplyDamage(int damageToApply)
    {
        int damageTaken = 0;

        // Start from the end of the list and go backwards by decrementing i
        for (int i = heartPairs.Count - 1; i >= 0 && damageTaken < damageToApply; i--)
        {
            HeartPair pair = heartPairs[i];
            Image rightHeartImage = pair.rightHeart.GetComponent<Image>();
            Image leftHeartImage = pair.leftHeart.GetComponent<Image>();

            if (damageTaken < damageToApply && rightHeartImage.enabled)
            {
                rightHeartImage.enabled = false;
                damageTaken++;
            }

            if (damageTaken < damageToApply && leftHeartImage.enabled)
            {
                leftHeartImage.enabled = false;
                damageTaken++;
            }
        }
    }

    public void ApplyHealing(int healingToApply)
    {
        int healingTaken = 0;

        // Start from the end of the list and go backwards by decrementing i
        for (int i = heartPairs.Count - 1; i >= 0 && healingTaken < healingToApply; i--)
        {
            HeartPair pair = heartPairs[i];
            Image rightHeartImage = pair.rightHeart.GetComponent<Image>();
            Image leftHeartImage = pair.leftHeart.GetComponent<Image>();

            if (healingTaken < healingToApply && !leftHeartImage.enabled)
            {
                leftHeartImage.enabled = true;
                healingTaken++;
            }

            if (healingTaken < healingToApply && !rightHeartImage.enabled)
            {
                rightHeartImage.enabled = true;
                healingTaken++;
            }
        }
    }
}