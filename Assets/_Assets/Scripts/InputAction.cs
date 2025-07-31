using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction : MonoBehaviour
{
    public float speedWalk = 4f;
    public float speedCrouch = 2f;
    public float speed;
    public bool isCrouch = false;

    public float jumForce = 10f;
    public float addForceDash = 5f;

    public bool isGround;
    public bool isJump;

    [SerializeField] Transform pointCheck;
    public LayerMask LayerMaskGround;
    public float checkGroundRaidus = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;

    private const string NAME_ANI_WALK = "isWalk";
    private const string NAME_ANI_CROUCH = "isCrouch";
    private const string NAME_ANI_JUMP = "isJump";
    private const string NAME_CHECKGROUND = "isGround";

    public KeyCode inputMoveLeft = KeyCode.A;
    public KeyCode inputMoveRight = KeyCode.D;
    public KeyCode inputCround = KeyCode.S;
    public KeyCode inputJump = KeyCode.Space;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = speedWalk;
    }

    private void Update()
    {
        HandleMovement();
        HandleCrounch();
        HandleJump();
        CheckGround();
       
    }

    private void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(pointCheck.position, checkGroundRaidus, LayerMaskGround);
        animator.SetBool(NAME_CHECKGROUND, isGround);
    }

    private void HandleMovement()
    {
        float moveFloat = 0;
        Vector2 scale = transform.localScale;

        if(Input.GetKey(inputMoveLeft))
        {
            moveFloat = -1;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;

            rb.velocity = new Vector2(moveFloat * speed, rb.velocity.y);
            animator.SetBool(NAME_ANI_WALK, true);
        }

        if(Input.GetKeyUp(inputMoveLeft))
        {
            animator.SetBool(NAME_ANI_WALK, false);
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKey(inputMoveRight))
        {
            moveFloat = 1;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;

            rb.velocity = new Vector2(moveFloat * speed, rb.velocity.y);
            animator.SetBool(NAME_ANI_WALK, true);
        }

        if (Input.GetKeyUp(inputMoveRight))
        {
            animator.SetBool(NAME_ANI_WALK, false);
            rb.velocity = Vector2.zero;
        }

        if (isCrouch) animator.SetBool(NAME_ANI_WALK, false);

    }

    private void HandleCrounch()
    {
        if(Input.GetKey(inputCround))
        {
            animator.SetBool(NAME_ANI_CROUCH, true);
            speed = speedCrouch;
            isCrouch = true;
        }

        if (Input.GetKeyUp(inputCround))
        {
            animator.SetBool(NAME_ANI_CROUCH, false);
            speed = speedWalk;
            isCrouch = false;
        }
    }    

    private void HandleJump()
    {
        if(Input.GetKeyDown(inputJump) && isGround)
        {
            animator.SetTrigger(NAME_ANI_JUMP);
            rb.velocity = new Vector2(rb.velocity.x, jumForce);
        }
    }


}
