using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float respawnYThreshold = -20f;

    private GameObject startLamp;
    private Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        startLamp = GameObject.FindGameObjectWithTag("StartLamp");
        
        //AT In the future, we should not have multiple respawn managers running around the scene and instead
        // attach all respawning logic to the game manager, the checkpoints updating the one manager where to
        // respawn the player.
        if(startLamp) respawnPosition = startLamp.transform.position;
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