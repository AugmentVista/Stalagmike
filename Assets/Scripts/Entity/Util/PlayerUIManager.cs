using Assets.Scripts;
using Assets.Scripts.Entity.Util;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private List<GameObject> hearts = new List<GameObject>();


    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        Debug.Log($"Is HealthSystem Valid? {healthSystem.isActiveAndEnabled}");
        Debug.Log(hearts.Count.ToString());
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    healthSystem.TakeDamage(new HitInfo(1));

            if (healthSystem.isActiveAndEnabled && healthSystem.health <= healthSystem.maxHp)
            {
                foreach (GameObject heart in hearts)
                {
                    heart.SetActive(false);
                }
                for (int i = 0; i < healthSystem.health; i++)
                {
                    hearts[i].SetActive(true);
                }
            }
        }
    //}
}