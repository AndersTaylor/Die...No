using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 9f;
    public float deadZone = 0.25f;
    public float topEdge;
    public float bottEdge;
    public float rightEdge;
    public float leftEdge;

    private GameObject player;
    private PlayerController pController;
    private Vector3 groundedTarget;
    private Vector3 airTarget;
    private float _followSpeed;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        Debug.Log(Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)));
        //AT Set the _followSpeed (the actual value the logic checks) depending on how far the camera is from player
        if (Input.GetAxisRaw("Horizontal") == 0 && pController.isGrounded)
        {
            _followSpeed = 20;
        }
        else if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y),
                new Vector2(transform.position.x, transform.position.y)) > 3)
        {
            _followSpeed = pController.speed;
        }
        else
        {
            _followSpeed = followSpeed;
        }
        
        groundedTarget = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        airTarget = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if (pController.isGrounded && Vector3.Distance(transform.position, groundedTarget) > deadZone)
        {
            // Move our position a step closer to the target.
            float step = _followSpeed * Time.deltaTime; // calculate distance to move, change by frame rate
            Vector3 newPosition = transform.position;
            
            // Move the camera within edges
            if (groundedTarget.x > leftEdge && groundedTarget.x < rightEdge)
            {
                newPosition.x = Mathf.MoveTowards(transform.position.x, groundedTarget.x, step);
            }
            if (groundedTarget.y < topEdge && groundedTarget.y > bottEdge)
            {
                newPosition.y = Mathf.MoveTowards(transform.position.y, groundedTarget.y, step);
            }
            transform.position = newPosition;
        }
        else if (!pController.isGrounded && Vector3.Distance(transform.position, airTarget) > deadZone) 
        {
            // Move our position a step closer to the target.
            float step = _followSpeed * Time.deltaTime;
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
