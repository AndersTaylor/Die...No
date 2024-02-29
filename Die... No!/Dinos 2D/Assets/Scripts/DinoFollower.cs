using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DinoFollower : MonoBehaviour
{
    public float speed = 1;

    private Animator animator;
    private GameObject player;
    private Transform target;
    private Rigidbody2D rb;
    private bool isSitting = false;
    private  bool shouldMove;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        target = player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
        }

        if (!isSitting && shouldMove)
        {
            if (target.position.x > transform.position.x)
            {
                animator.SetBool("IsMoving", true);
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (target.position.x < transform.position.x)
            {
                animator.SetBool("IsMoving", true);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            //teleport
            if (Vector2.Distance(target.position, transform.position) > 6)
            {
                transform.position = target.position;
            }
        }
        else
        {   
            animator.SetBool("IsMoving", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //sit when clicked on
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldMove = false;
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

