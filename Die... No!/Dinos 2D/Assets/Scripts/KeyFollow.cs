using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KeyFollow : MonoBehaviour
{
    public float speed = 1;

    private Animator animator; 
    private PickUp pickUpScript;
    private SpriteRenderer sr; 

    void Start()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            pickUpScript = parentTransform.GetComponent<PickUp>();
        }
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    void Update()
    {
        if (!pickUpScript.guyDiscovered && pickUpScript.player.transform.position.y >= 5.057f && pickUpScript.transform.position.x >= 43f)
        {
            sr.enabled = true;
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!pickUpScript.isSelected)
            {
                animator.SetBool("Selected", true);
            }
        }
        if (other.gameObject.CompareTag("Corrupted"))
        {
            animator.SetTrigger("Unlock");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
    }
}