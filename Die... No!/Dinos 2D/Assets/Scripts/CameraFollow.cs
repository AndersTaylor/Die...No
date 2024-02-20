using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 1.0f;
    public float deadZone = 0.25f;
    
    private GameObject player;
    private Vector3 groundedTarget;
    private Vector3 airTarget;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    { 
        groundedTarget = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        airTarget = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        
        if (player.GetComponent<PlayerController>().isGrounded && Vector3.Distance(transform.position, groundedTarget) < deadZone)
        {
            // Move our position a step closer to the target.
            float step =  followSpeed * Time.deltaTime; // calculate distance to move, change by frame rate
            transform.position = Vector3.MoveTowards(transform.position, groundedTarget, step);
        }
        else if (!player.GetComponent<PlayerController>().isGrounded && Vector3.Distance(transform.position, airTarget) < deadZone)
        {
            // Move our position a step closer to the target.
            float step =  followSpeed * Time.deltaTime; // calculate distance to move, change by frame rate
            transform.position = Vector3.MoveTowards(transform.position, airTarget, step);
        }
    }
}
