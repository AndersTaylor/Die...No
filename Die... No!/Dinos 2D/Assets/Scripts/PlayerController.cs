using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 1000f;
    public float speed = 1;

    public float fallMultiplierFloat;
    public float lowJumpMultiplierFloat;
	
    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
    private void Update() 
    {
		PlayerInputsAndMovement();
    }

    private void Jump(float _jumpForce)
    {
        rb.AddForce(Vector2.up * _jumpForce);
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
}
