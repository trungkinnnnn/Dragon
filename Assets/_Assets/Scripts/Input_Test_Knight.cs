using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Test_Knight : MonoBehaviour
{
   
    public float speedWalk = 4f;
    public float speedCrouch = 2f;
    public float speedUp = 1.0f;
    public float speed;

    public bool isCrouch = false;
    public bool isStandup = false;  

    public float jumForce = 10f;
    public float addForceDash = 5f;

    public bool isGround;
    public bool isFalling;
    public bool isJump;

    [SerializeField] Transform pointCheck;
    public LayerMask LayerMaskGround;

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
        HandleStrikeInput();
        HandleInputHurt();
        HandleDashInput();
        HandlBlockInput();
        HandleCastInput();
        CheckGround();
    }

    public void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(pointCheck.position, 0.2f, LayerMaskGround);
        animator.SetBool("isGround", isGround);
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animator.SetTrigger("isJump");
            rb.velocity = new Vector2(rb.velocity.x, jumForce);
        }

        if (!isGround && Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("isJumpAttack");
              
        }
    }

    private void HandleInputHurt()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("isHurt");
            animator.SetBool("isUp", false);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("isUp", true);
        }
    }

    private void HandleStrikeInput()
    {

        if(Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("isStrike");
            Vector2 dircetion = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.AddForce(dircetion * addForceDash, ForceMode2D.Force);
        }
    }
    private void HandleAttackInput()
    {
        if (!isCrouch && isGround)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger("isAttack");
            }
        }
    }

    private void HandlBlockInput()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("isBlock");
        }
    }

    private void HandleCastInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("isCast");
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
    }

    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isDash");
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.AddForce(direction * addForceDash, ForceMode2D.Impulse);
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
            animator.SetBool("isWalk", true);
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalk", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale= scale;

            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            animator.SetBool("isWalk", true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalk", false);
        }



    }  

}
