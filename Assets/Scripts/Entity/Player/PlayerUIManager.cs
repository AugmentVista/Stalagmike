using Assets.Scripts;
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

    [SerializeField] GameObject player;

    [SerializeField] GameObject playerCanvas;

    int spaceBetweenHearts = 100;
    int lastKnownMaxHP;
    int lastKnownHealth;

    private void Start()
    {
        playerCanvas = transform.parent.gameObject;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && player.TryGetComponent(out HealthSystem playerHealthSystem))
        {
            healthSystem = playerHealthSystem;
            lastKnownMaxHP = healthSystem.maxHp;
            lastKnownHealth = healthSystem.health;
            SetMaxUIHealth();
        }
        else
        {
            Debug.LogWarning("Player or HealthSystem not found by UI script.");
        }
    }

    public void SetMaxUIHealth()
    {
        int fullHeartCount = healthSystem.maxHp / 2;

        for (int i = 0; i < fullHeartCount; i++)
        {
            GameObject fullheart = Instantiate(fullHeartPrefab, transform.position, Quaternion.identity, transform);
            fullheart.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 + i * spaceBetweenHearts, 0);
            listOfFullHearts.Add(fullheart);
            Debug.Log($"Initial heart created");

            Transform left = fullheart.transform.Find("Left Heart");
            Transform right = fullheart.transform.Find("Right Heart");

            if (left == null || right == null)
            {
                Debug.LogWarning($"Heart prefab is missing 'Left Heart' or 'Right Heart' child objects.");
                continue;
            }

            HeartPair pair = new HeartPair
            {
                leftHeart = left.gameObject,
                rightHeart = right.gameObject
            };

            heartPairs.Add(pair);
        }
    }


    private void Update()
    {
        UpdateMaxHealth();
        UpdateHealthValue();
    }

    private void UpdateMaxHealth()
    {
        if (lastKnownMaxHP + 1 < healthSystem.maxHp)
        {
            int deltaMaxHealth = healthSystem.maxHp - lastKnownMaxHP;
            lastKnownMaxHP = healthSystem.maxHp;

            for (int i = 0; i < deltaMaxHealth / 2; i++)
            {
                GameObject fullheart = Instantiate(fullHeartPrefab, transform.position, Quaternion.identity, transform);
                fullheart.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 + i + listOfFullHearts.Count * 100, 0);

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

    private void UpdateHeartVisuals()
    {
        int healthToShow = healthSystem.health;
        int maxHearts = heartPairs.Count * 2;
        int currentIndex = 0;

        for (int i = 0; i < heartPairs.Count; i++)
        {
            HeartPair pair = heartPairs[i];
            Image left = pair.leftHeart.GetComponent<Image>();
            Image right = pair.rightHeart.GetComponent<Image>();

            // Left Heart
            left.enabled = (currentIndex < healthToShow);
            currentIndex++;

            // Right Heart
            right.enabled = (currentIndex < healthToShow);
            currentIndex++;
        }
    }

    private void UpdateHealthValue()
    {
        int deltaHealth = healthSystem.health - lastKnownHealth;
        if (deltaHealth != 0)
        {
            if (lastKnownHealth != healthSystem.health)
            {
                UpdateHeartVisuals();
                lastKnownHealth = healthSystem.health;
            }

            lastKnownHealth = healthSystem.health;
        }
    }

}