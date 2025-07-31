using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Test_Dragon : MonoBehaviour
{
   
    public float speedWalk = 4f;
    public float speedCrouch = 2f;
    public float speedUp = 1.0f;
    public float speed;

    public bool isCrouch = false;
    public bool isStandup = false;  
    public bool isStrike = false;

    public float jumForce = 10f;

    public bool isGround;
    public bool isFalling;
    public bool isJump;

    public float addForceFlyKick = 3f;

    [SerializeField] Transform pointCheck;
    public LayerMask LayerMaskGround;

    // fireBall
    [SerializeField] Transform pointFire;
    [SerializeField] GameObject fireBall;

    private Rigidbody2D rb;
    private Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = speedWalk;
    }

    public void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
        HandleCrouchInput();
        HandleJumpInput();
        HandleFlyKickInput();
        HandleStrikeInput();
        HandleInputHurt();
        CheckGround();
        CheckFalling();
    }

    public void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(pointCheck.position, 0.2f, LayerMaskGround);
        animator.SetBool("isGround", isGround);
    }

    public void CheckFalling()
    {
        if (!isGround && rb.velocity.y < 0)
        {
            if (!isFalling)
            {
                animator.SetTrigger("isJump");
                isFalling = true;
            }
        }
        else
        {
            isFalling = false;
        }
    }

    private void HandleJumpInput()
    {
        if (!isStrike)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                animator.SetTrigger("isJump");
                rb.velocity = new Vector2(rb.velocity.x, jumForce);
            }

            if (!isGround && Input.GetKeyDown(KeyCode.J))
            {
                animator.SetBool("isAttack_bool", true);
                Fire();
            }
        }
    }

    private void HandleInputHurt()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("isHurt");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("isStandup", true);
        }
    }

    private void HandleStrikeInput()
    {

        if(Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, speedUp);
            isStrike = true;
            animator.SetBool("isStrike", true);
            animator.Play("strike");
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isStrike = false;
            animator.SetBool("isStrike", false);
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("isAttack_bool", false);
        animator.SetBool("isStandup", false);
    }

    private void HandleFlyKickInput()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isFlyKick");
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.AddForce(direction * addForceFlyKick, ForceMode2D.Impulse);
        }
    }


    private void HandleAttackInput()
    {
        if (!isCrouch && isGround)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger("isAttack_trigger");
                Fire();
            }
        }
    }

    private void HandleCrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouch = true;
            animator.SetBool("isCrouch", isCrouch);
            speed = speedCrouch;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouch = false;
            animator.SetBool("isCrouch", isCrouch);
            speed = speedWalk;
        }

        if (isCrouch && Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("isAttack_bool", true);
            Fire();
        }
    }
    private void HandleMovementInput()
    {
        float moveInput = 0f;
        Vector3 scale = transform.localScale;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;

            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale= scale;

            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        
        animator.SetFloat("speed", Mathf.Abs(moveInput));
    }


    private void Fire()
    {
        GameObject Ball = Instantiate(fireBall, pointFire.position, Quaternion.identity);
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Ball.GetComponent<FireBall>().SetDirection(direction);
    }    

}
