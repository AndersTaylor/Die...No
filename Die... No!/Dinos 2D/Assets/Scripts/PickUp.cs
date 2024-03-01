using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public float speed = 1;
    public GameObject player;
    private GameObject corruptDino;
    private GameController gc;
    private Transform target;
    private Rigidbody2D rb;
    public bool guyDiscovered = false;
    private bool shouldMove;
    public bool isSelected;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        corruptDino = GameObject.FindGameObjectWithTag("Corrupted");
        target = player.transform;
        rb = GetComponent<Rigidbody2D>();
        shouldMove = false;
        isSelected = false;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
        }

        if (!guyDiscovered && player.transform.position.y >= 5.057f && player.transform.position.x >= 43f)
        {
            guyDiscovered = true;
        }

        if (shouldMove)
        {
            Debug.Log("Move!");
            if (target.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (target.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            if (target.position.y > transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else if (target.position.y < transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isSelected) 
            {
                shouldMove = false;
            } 
            else 
            {
                shouldMove = true;
                isSelected = true;
            }
        }
        if (other.gameObject.CompareTag("Corrupted"))
        {
            gc.unlockCorrupted = true;
            target = corruptDino.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldMove = true;
        }
    }
}
