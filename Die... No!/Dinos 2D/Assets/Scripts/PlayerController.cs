using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 1000f;
    public float speed = 1;

    public float fallMultiplierFloat;
    public float lowJumpMultiplierFloat;
    private bool isGrounded = true;
	
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
        //AT Hardcoded distance down to check for ground. Will be different depending on character sprite
        RaycastHit2D hit = Physics2D.Raycast((transform.position + Vector3.down * 1.5f), Vector2.down, 0.1f);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
            return isGrounded = true;
        
        return isGrounded = false;
    }
}
