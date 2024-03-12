 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickUp : MonoBehaviour
{
    public float damp = 1;
    
    private GameObject player;
    private Transform target;
    private bool shouldMove;
    private GameController gc;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        target = player.transform;
        shouldMove = false;
    }

    Vector3 vel = Vector3.zero;
    private void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
        }
        
        if (shouldMove)
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel, damp);
    }

    /*void FixedUpdate()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
        }
       
        float distance = Vector3.Distance(transform.position, target.position);
        float tempSpeed = speed * Mathf.Sqrt(distance);
        //shouldMove = distance > 1;
        
        
        if (shouldMove)
        {
            if (target.position.x - transform.position.x > 1)
            {
                rb.velocity = new Vector2(tempSpeed, rb.velocity.y);
            }
            else if (target.position.x - transform.position.x < 1)
            {
                rb.velocity = new Vector2(-tempSpeed, rb.velocity.y);
            }
            if (target.position.y - transform.position.y > 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, tempSpeed);
            }
            else if (target.position.y - transform.position.y < 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, -tempSpeed);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    } */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || gc.pickedUpKey) return;
        GetComponent<AudioSource>().Play();
        gc.pickedUpKey = true;
        shouldMove = true;
    }
}