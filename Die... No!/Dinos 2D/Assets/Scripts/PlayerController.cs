using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 1000f;
    public float speed = 1;
    public bool isGrounded = true;

    public float fallMultiplierFloat;
    public float lowJumpMultiplierFloat;
	
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        //AT Move the character using axis
        transform.position += Vector3.right * (Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
        
        //AT Hardcode jump to space key. Could change.
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump(jumpForce);
        
        //AT faster falling
        if (rb.velocity.y < 0) 
            rb.velocity += Vector2.up * (Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime);
        
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
            if (hits[i].collider != null && !hits[i].collider.isTrigger && hits[i].collider.gameObject.CompareTag("Ground"))
                return isGrounded = true;
        }
        
        return isGrounded = false;
    }
}