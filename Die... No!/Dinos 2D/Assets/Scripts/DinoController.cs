using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public bool facingRight = true;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((h > 0 && !facingRight) || (h < 0 && facingRight)) { Flip(); }

        float speed = rb.velocity.magnitude;
        if (speed > 0.1f)
        {
            animator.SetBool("IsMoving", true);
            Debug.Log("runn");
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
