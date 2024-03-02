using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class isCorrupted : MonoBehaviour
{
    public bool hasKey;
    private Animator animator;
    private bool hasBeenPurified = false;
    private GameController gc;

    void Start()
    {
        animator = GetComponent<Animator>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gc.pickedUpKey && !hasBeenPurified)
            {
                StartCoroutine(Purified());
            }
        }
    }

    IEnumerator Purified()
    {
        animator.SetBool("Has Key", true);

        // Wait for the purification animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Set a flag to indicate that purification has been done
        hasBeenPurified = true;

        // Reset the parameter for next use
        animator.SetBool("Has Key", false);
        
        StartCoroutine(gc.LoadNextScene());
    }
}
