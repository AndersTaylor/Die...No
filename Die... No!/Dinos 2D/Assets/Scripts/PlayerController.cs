using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 1000f;
    public float speed = 1;
    public bool isGrounded = true;
    public bool facingRight = true;

    public float fallMultiplierFloat;
    public float lowJumpMultiplierFloat;
	
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
    private void Update() 
    {
		PlayerInputsAndMovement();
        CheckIsGrounded();
    }

    private void Jump(float _jumpForce)
    {
        rb.AddForce(Vector2.up * _jumpForce);
        isGrounded = false;
    }

    private void PlayerInputsAndMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((h > 0 && !facingRight) || (h < 0 && facingRight)) { Flip(); }

        //AT Move the character using axis
        transform.position += Vector3.right * (h * speed * Time.deltaTime);
        
        animator.SetBool("NotMoving", Mathf.Approximately(h, 0));

        //AT Hardcode jump to space key. Could change.
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            animator.SetTrigger("Jump");
            Jump(jumpForce);
        }
        //AT faster falling
        if (rb.velocity.y < 0) {
            animator.SetTrigger("JumpFall");
            rb.velocity += Vector2.up * (Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime);
        }
        //AT control jump height by length of time jump button held
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) 
            rb.velocity += Vector2.up * (Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime);
    }

    private bool CheckIsGrounded()
    {
        RaycastHit2D[] hits = new RaycastHit2D[2];
        
        //AT Hardcoded distance down to check for ground. Will be different depending on character sprite
        //AT Send out two rays at each end of the character. The rays are intentionally offset downwards and to the sides for forgiving jumps
        RaycastHit2D hitR = Physics2D.Raycast((transform.position + Vector3.down * 1.5f + Vector3.right * 0.8f), Vector2.down, 0.1f);
        RaycastHit2D hitL = Physics2D.Raycast((transform.position + Vector3.down * 1.5f + Vector3.left * 0.4f), Vector2.down, 0.1f);

        hits[0] = hitR;
        hits[1] = hitL;
        
        for (int i = 0; i < hits.Length; i++)
        {
            //AT Check to see if the rays hit a collider, the collider is not a trigger, and the collider is tagged ground
            if (hits[i].collider != null && !hits[i].collider.isTrigger && hits[i].collider.gameObject.CompareTag("Ground")) {
                animator.SetBool("IsGrounded", true);
                return isGrounded = true;
            }
        }
        animator.SetBool("IsGrounded", false);
        return isGrounded = false;
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}