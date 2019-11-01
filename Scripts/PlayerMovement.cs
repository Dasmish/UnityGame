using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator Animator;

    public float Speed = 1f;
    float HorizontalMove = 0f;
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

        Animator.Play("PlayerRun");

        HorizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(HorizontalMove * Speed, rb.velocity.y);

        if (FacingRight == false && HorizontalMove > 0)
        {
            FlipCharacter();
        } else if (FacingRight == true && HorizontalMove < 0)
        {
            FlipCharacter();
        }

    }

    void Update ()
    {
        if (isGrounded)
        {
            CanJump = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && CanJump)
        {
            CanJump = false;
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
