using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    public Animator Animator;

    public float Speed = 1f;
    float HorizontalMove = 0f;
    float VerticalMove = 0f;
    public float JumpVelocity = 3f;
    public bool CanJump = true;

    private bool FacingRight = true;

    private bool isGrounded;
    public Transform GroundCheck;
    public float CheckRadius;
    public LayerMask WhatsGround;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatsGround);

        HorizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(HorizontalMove * Speed, rb.velocity.y);
        

        if (HorizontalMove > 0  || HorizontalMove < 0)
        {
            Animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));
        } else
        {
            Animator.SetFloat("Speed", 0);
        }

        if (!FacingRight && HorizontalMove > 0)
        {
            FlipCharacter();
        } else if (FacingRight && HorizontalMove < 0)
        {
            FlipCharacter();
        }

    }

    void Update ()
    {
        if (isGrounded)
        {
            CanJump = true;
            Animator.SetBool("isJumping", false);
        }

        if (VerticalMove > 0 && CanJump)
        {
            CanJump = false;
            Animator.SetBool("isJumping", true);
            rb.velocity = Vector2.up * JumpVelocity;
        }

    }


    void FlipCharacter()
    {
        FacingRight = !FacingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

}
