using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isCorrupted : MonoBehaviour
{
    public GameObject BlueDino;
    public bool hasKey;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasKey == true)
        {
            StartCoroutine(purified());
        }
    }
    
    IEnumerator purified()
    {
        animator.SetTrigger("purify");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Instantiate(BlueDino, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
