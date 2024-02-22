using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isCorrupted : MonoBehaviour
{
    public bool HasKey;
    private Animator animator;
    private bool hasBeenPurified = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasKey && !hasBeenPurified)
        {
            StartCoroutine(Purified());
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
    }
}
