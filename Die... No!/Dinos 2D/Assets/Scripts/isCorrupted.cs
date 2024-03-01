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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //AT This is a crappy piece of code to be refactored for example in the GameController script 
        // to set it once instead of checking every frame.
        hasKey = gc.pickedUpKey;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hasKey && !hasBeenPurified)
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
        
        StartCoroutine(TempLoadEnd());
    }

    //AT delete l8r
    IEnumerator TempLoadEnd()
   {
       yield return new WaitForSeconds(5);


       Scene currentScene = SceneManager.GetActiveScene();
       string sceneName = currentScene.name;


       if (sceneName == "Level 1")
       {
           SceneManager.LoadScene("Level 2");
       } 
       else if (sceneName == "Level 2")
       {
           SceneManager.LoadScene("Level 3");
       } else {
           SceneManager.LoadScene("EndScene");
       }
   }

}
