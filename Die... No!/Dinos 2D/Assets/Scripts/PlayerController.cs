using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float jumpForce = 2000f;
    public float speed = 10;
    public bool isGrounded = true;
    public bool facingRight = true;
    public float lowJumpMultiplierFloat;
	
    private Rigidbody2D rb;
    private Animator animator;
    private float gravityScale;
    
    public bool goingUp;

    public ParticleSystem runDust;
    public ParticleSystem jumpDust;
    public AudioClip jumpSound;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Debug.Log(animator);

        gravityScale = rb.gravityScale;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = jumpSound;
        
        runDust.Play();
    }
	
    private void Update() 
    {
        PlayerInputsAndMovement();
        CheckIsGrounded();
    }

    private void Jump(float _jumpForce)
    {
        PlayJumpSound();
        rb.AddForce(Vector2.up * _jumpForce);
        isGrounded = false;
        
        //AT jump particles
        CreateJumpDust();
    }

    private void PlayerInputsAndMovement()
    {
        //AT Player Movement
        float h = Input.GetAxisRaw("Horizontal");
        if((h > 0 && !facingRight) || (h < 0 && facingRight)) { Flip();}

        //AT Give more movement control in the air
        rb.velocity = isGrounded ? new Vector2(h * speed, rb.velocity.y) : new Vector2(h * speed * 1.2f, rb.velocity.y);
        
        animator.SetBool("NotMoving", Mathf.Approximately(h, 0));

        //AT Hardcode jump to space key. Could change.
        if(Input.GetKeyDown(KeyCode.Space) && (isGrounded || Input.GetKey(KeyCode.LeftShift)))
        {
            animator.SetTrigger("Jump");
            Jump(jumpForce);
            goingUp = true;
        }
        
        //AT control jump height by length of time jump button held
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            //rb.gravityScale = lowJumpMultiplierFloat;
        }
        else if (rb.velocity.y < 0 && goingUp) 
        {
            animator.SetTrigger("JumpFall");
            StartCoroutine(FloatyJump());
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
        
        //AT Manage run particles
        CreateRunDust();
    }

    private bool CheckIsGrounded()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        
        //AT Hardcoded distance down to check for ground. Will be different depending on character sprite
        //AT Send out two rays at each end of the character and one in the middle. The rays are intentionally offset downwards and to the sides for forgiving jumps
        RaycastHit2D hitR = Physics2D.Raycast((transform.position + Vector3.down * 1.5f + Vector3.right * 0.6f), Vector2.down, 0.1f);
        RaycastHit2D hitL = Physics2D.Raycast((transform.position + Vector3.down * 1.5f + Vector3.left * 0.4f), Vector2.down, 0.1f);
        RaycastHit2D hitM = Physics2D.Raycast((transform.position + Vector3.down * 1.5f), Vector2.down, 0.1f);

        hits[0] = hitR;
        hits[1] = hitL;
        hits[2] = hitM;
        
        for (int i = 0; i < hits.Length; i++)
        {
            //AT Check to see if the rays hit a collider, the collider is not a trigger, and the collider is tagged ground
            if (hits[i].collider != null && !hits[i].collider.isTrigger && (hits[i].collider.gameObject.CompareTag("Ground") || hits[i].collider.gameObject.CompareTag("Dino"))) 
            {
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

    IEnumerator FloatyJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        yield return new WaitForSeconds(0.1f);
        goingUp = false;
    }

    void CreateRunDust()
    {
        if (rb.velocity != Vector2.zero)
        {
            var dust = runDust.emission;
            dust.enabled = true;
        }
        else
        {
            var dust = runDust.emission;
            dust.enabled = false;
        }
    }
    
    void CreateJumpDust()
    {
        jumpDust.Play();
    }
    
    void PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void Still() {
        animator.SetBool("NotMoving", true);
    }
}