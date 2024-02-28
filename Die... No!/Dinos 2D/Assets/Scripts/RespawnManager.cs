using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerDeath deathScript;

    void Start()
    {
        deathScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            deathScript.UpdateLastCheckpoint(transform.position);
        }
    }
}
