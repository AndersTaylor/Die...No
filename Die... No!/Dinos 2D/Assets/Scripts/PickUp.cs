using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float speed = 4;
    public GameObject player;
    private GameObject corruptDino;
    private GameController gc;
    private Transform target;
    private Rigidbody2D rb;
    public bool guyDiscovered = false;
    private bool shouldMove;
    public bool isSelected;
    public GameObject goldKey;
    private Animator goldKeyAnimator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        corruptDino = GameObject.FindGameObjectWithTag("Corrupted");
        target = player.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        shouldMove = false;
        isSelected = false;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (goldKey != null)
        {
            goldKeyAnimator = goldKey.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if(!rb)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

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
            if (rb == null) 
            {
                Debug.LogError("Rigidbody not found on the object.");
            } else {
            rb.velocity = new Vector2(0, 0);
            }
        }
        if (goldKeyAnimator != null)
        {
            if (!isSelected && shouldMove)
            {
                goldKeyAnimator.SetBool("Selected", true);
            }
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
            if (goldKeyAnimator != null)
            {
                goldKeyAnimator.SetTrigger("Unlock");
                gc.unlockCorrupted = false;
                gc.pickedUpKey = false;
            }
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