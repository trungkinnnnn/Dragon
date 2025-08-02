using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Purchasing;
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

    [Header("CHECK GROUND")]
    [SerializeField] Transform pointCheck;
    public LayerMask LayerMaskGround;
    public float checkGroundRaidus = 0.2f;

    [Header("FIRE BALL")]
    [SerializeField] Transform pointFire;
    [SerializeField] GameObject fireBall;

    private Rigidbody2D rb;
    private Animator animator;

    private const string NAME_ANI_WALK = "isWalk";
    private const string NAME_ANI_CROUCH = "isCrouch";
    private const string NAME_ANI_JUMP = "isJump";
    private const string NAME_CHECKGROUND = "isGround";
    private const string NAME_ANI_ATTACKTJUMP = "isJumpAttack";
    private const string NAME_ANI_ATTACKMELEE = "isAttackMelee";
    private const string NAME_ANI_ATTACKRANGE = "isAttackRange";
    private const string NAME_ANI_DASH = "isDash";
    private const string NAME_ANI_STRIKE = "isStrike";
    private const string NAME_ANI_HURT = "isHurt";
    private const string NAME_CHECKUP = "isUp";


    [Header("INPUT KEYCODE")]
    public KeyCode inputMoveLeft = KeyCode.A;
    public KeyCode inputMoveRight = KeyCode.D;
    public KeyCode inputCround = KeyCode.S;
    public KeyCode inputJump = KeyCode.Space;
    public KeyCode inputAttackMelee = KeyCode.J;
    public KeyCode inputAttackRange = KeyCode.K;
    public KeyCode inputDash = KeyCode.N;
    public KeyCode inputStrike = KeyCode.H;
    public KeyCode inputHurt = KeyCode.I;
    public KeyCode inputUp = KeyCode.U;

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
        HandleAttackMelee();
        HandleAttackRange();
        HandleDash();
        HandleStrike();
        HandleHurt();

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

        if (Input.GetKeyDown(inputAttackMelee) && !isGround)
        {
            animator.SetTrigger(NAME_ANI_ATTACKTJUMP);
        }
    }

    private void HandleAttackMelee()
    {
        if(Input.GetKeyDown(inputAttackMelee) && isGround)
        {
            animator.SetTrigger(NAME_ANI_ATTACKMELEE);
        }
       
    }

    private void HandleAttackRange()
    {
        if (Input.GetKeyDown(inputAttackRange))
        {
            animator.SetTrigger(NAME_ANI_ATTACKRANGE);
            Fire();
        }    
        
    }

    private void HandleDash()
    {
        if(Input.GetKeyDown(inputDash))
        {
            animator.SetTrigger(NAME_ANI_DASH);
            Vector3 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.AddForce(direction * addForceDash, ForceMode2D.Impulse);
        }
    }

    private void HandleStrike()
    {
        if (Input.GetKeyDown(inputStrike))
        {
            animator.SetTrigger(NAME_ANI_STRIKE);
            Vector3 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.AddForce(direction * addForceDash, ForceMode2D.Impulse);
        }
    }

    private void HandleHurt()
    {
        if (Input.GetKeyDown(inputHurt))
        {
            animator.SetTrigger(NAME_ANI_HURT);
            animator.SetBool(NAME_CHECKUP, false);
        }

        if(Input.GetKeyDown(inputUp))
        {
            animator.SetBool(NAME_CHECKUP, true);
        }
    }

    private void Fire()
    {
        GameObject Ball = Instantiate(fireBall, pointFire.position, Quaternion.identity);
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Ball.GetComponent<FireBall>().SetDirection(direction);
    }

}
