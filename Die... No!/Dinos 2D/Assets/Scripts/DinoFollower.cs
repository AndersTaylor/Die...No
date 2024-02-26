using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DinoFollower : MonoBehaviour
{
    public float speed = 1;

    private bool isSitting = false;
    private GameObject player;
    private Transform target;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    //AT To Do:
    // - Smoothly follow player on x axis, always trying to get to its spot in line behind player
    //    - What if player turns around?
    // - Sit on player keypress to stop moving


    void Update()
    {
        if (!isSitting)
        {
            if (Vector2.Distance(target.position, transform.position) < 2)
                rb.velocity = Vector2.zero;
            else if (target.position.x - 2 > transform.position.x)
                rb.velocity = new Vector2(speed, 0);
            else if (target.position.x - 2 < transform.position.x)
                rb.velocity = new Vector2(-speed, 0);

            //teleport
            if (Vector2.Distance(target.position, transform.position) > 10)
            {
                transform.position = target.position;
            }

        }

        //sit when clicked on
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider.gameObject == gameObject)
            {
                StopStart();
            }
        }
    }

    void StopStart()
    {
        isSitting = !isSitting;

        if (isSitting)
        {
            rb.velocity = Vector2.zero;
        }
    }
}

