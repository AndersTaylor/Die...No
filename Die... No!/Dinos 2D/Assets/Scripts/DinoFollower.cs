using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DinoFollower : MonoBehaviour
{
    public float speed = 1;
    
    private bool isSitting;
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
    

    void Update () 
    {
        if (Vector2.Distance(target.position, transform.position) < 2)
            rb.velocity = Vector2.zero;
        else if(target.position.x - 2 > transform.position.x)
            rb.velocity = new Vector2(speed, 0);
        else if(target.position.x - 2 < transform.position.x)
            rb.velocity = new Vector2(-speed, 0);
    }
}
