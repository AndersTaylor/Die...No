using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private PlayerDeath deathScript;
    [HideInInspector] public Vector3 respawnPos;

    void Start()
    {
        deathScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            respawnPos = transform.position;
            deathScript.UpdateLastCheckpoint(respawnPos);
        }
    }
}
