using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private int score = 0;
    private SpriteRenderer sr;

    Vector2 start = new Vector2(-5, 1);

    private Vector2 movementVector;

    [SerializeField] Animator animator;

    [SerializeField] int speed = 0;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioClip JumpClip;
   
 //[SerizlizeField] 

    private Rigidbody2D rb;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        speed = 5;

        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            transform.position = start;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        // Debug.Log(movementVector);
        animator.SetBool("Walk_Right", !Mathf.Approximately(movementVector.x, 0));
        if (!Mathf.Approximately(movementVector.x, 0))
        {
            sr.flipX = movementVector.x < 0;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    void OnJump(InputValue value)
    {
        // Debug.Log("Here");
        if (isGrounded == true)
        {
            rb.AddForce(new Vector2(0, 500));
            SFXSource.PlayOneShot(JumpClip);
        }
      //  Debug.Log(isGrounded);
    }

    void OnDash(InputValue value)
    {
        rb.AddForce(new Vector2(200, 0));
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("Score is: " + score);
        }
    }


   
}