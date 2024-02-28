using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float respawnYThreshold = -20f;

    //public GameObject respawnPrefab;

    private Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = new Vector3(-12.62f, -0.21f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < respawnYThreshold)
        {
            RespawnPlayer();
        }
    }
    void RespawnPlayer()
    {
        
        GameObject newPlayer = Instantiate(gameObject, respawnPosition, Quaternion.identity);
        Destroy(gameObject);
    }

    public void UpdateLastCheckpoint(Vector3 checkpointPosition)
    {
        if (checkpointPosition.x > respawnPosition.x)
        {
            respawnPosition = checkpointPosition;
        }
    }

}