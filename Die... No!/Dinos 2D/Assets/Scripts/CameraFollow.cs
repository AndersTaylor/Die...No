using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 1.0f;
    public float deadZone = 0.25f;
    public float topEdge;
    public float bottEdge;
    public float rightEdge;
    public float leftEdge;

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

        //if (transform.position.x > leftEdge && transform.position.x < rightEdge && transform.position.y < topEdge && transform.position.y > bottEdge) {
        if (player.GetComponent<PlayerController>().isGrounded && Vector3.Distance(transform.position, groundedTarget) > deadZone)
        {
                // Move our position a step closer to the target.
            float step = followSpeed * Time.deltaTime; // calculate distance to move, change by frame rate
            Vector3 newPosition = transform.position;
            if (groundedTarget.x > leftEdge && groundedTarget.x < rightEdge)
            {
                newPosition.x = Mathf.MoveTowards(transform.position.x, groundedTarget.x, step);
            }
            if (groundedTarget.y < topEdge && groundedTarget.y > bottEdge)
            {
                newPosition.y = Mathf.MoveTowards(transform.position.y, groundedTarget.y, step);
            }
            transform.position = newPosition;

            //transform.position = Vector3.MoveTowards(transform.position, groundedTarget, step);
        }
        else if (!player.GetComponent<PlayerController>().isGrounded && Vector3.Distance(transform.position, airTarget) > deadZone) 
        {
            // Move our position a step closer to the target.
            float step = followSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position;
            if (airTarget.x > leftEdge && airTarget.x < rightEdge)
            {
                newPosition.x = Mathf.MoveTowards(transform.position.x, airTarget.x, step);
            }
            if (airTarget.y < topEdge && airTarget.y > bottEdge)
            {
                newPosition.y = Mathf.MoveTowards(transform.position.y, airTarget.y, step);
            }
            transform.position = newPosition;
        }
    
    }
}
