using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KeyFollow : MonoBehaviour
{
    public float speed = 1;

    private Animator animator;
    private GameObject player;
    private GameObject corruptDino;
    private GameController gc;
    private Transform target;
    private Rigidbody2D rb;
    private bool guyDiscovered = false;
    private SpriteRenderer sr;
    private bool shouldMove;
    private bool isSelected;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        corruptDino = GameObject.FindGameObjectWithTag("Corrupted");
        animator = GetComponent<Animator>();
        target = player.transform;
        rb = GetComponent<Rigidbody2D>();
        shouldMove = false;
        isSelected = false;
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
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
            sr.enabled = true;
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

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isSelected) 
            {
                shouldMove = false;
                Debug.Log("Fox is in range");
            } 
            else 
            {
                shouldMove = true;
                animator.SetBool("Selected", true);
                isSelected = true;
            }
        }
        if (other.gameObject.CompareTag("Corrupted"))
        {
            target = corruptDino.transform;
            animator.SetTrigger("Unlock");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldMove = true;
            Debug.Log("Fox is out of range");
        }
    }
}