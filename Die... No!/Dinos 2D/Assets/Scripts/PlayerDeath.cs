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
        GameObject respawnObject = GameObject.FindGameObjectWithTag("RespawnLamp");
        if (respawnObject != null)
        {
            respawnPosition = respawnObject.transform.position;
        }
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

}