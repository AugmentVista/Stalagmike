using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform RespawnPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject;

            Player.transform.position = RespawnPoint.position;
        }
    }



}
