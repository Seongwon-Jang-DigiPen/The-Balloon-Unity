using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl : MonoBehaviour
{
    /*public*/
    [Header("Movement")]
    public float speed = 64.0f;
    public float jumpForce = 10.0f;

    [Header("StateData")]
    public PlayerState state;

    [Header("Collision")]
    public Transform groundCheck;
    public float groundCheckLength = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump")]
    public float jumpCutMultiplier = 1.0f;
    /*private*/
    //input data
    private float verticalInput = 0;
    private bool isJumpKeyPressed = false;
    private bool isTouchingGround = false;
    private bool isJump = false;
    private bool isFall = false;

    private Rigidbody2D playerRb = null;
    private Animator animator = null;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        SetState();
    }
    void SetState()
    {
        playerRb.mass = state.rigid.mass;
        playerRb.drag = state.rigid.drag;
        playerRb.angularDrag = state.rigid.angularDrag;
        playerRb.gravityScale = state.rigid.gravityScale;
        speed = state.speed;
        jumpForce = state.jumpForce;
        animator.runtimeAnimatorController = state.animator.runtimeAnimatorController;
    }

    void FixedUpdate()
    {
        CheckGround();
        Movement();
        Jump();
    }

    void CheckGround()
    {
        isTouchingGround = Physics2D.Linecast(transform.position, transform.position + Vector3.down * groundCheckLength, groundLayer);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckLength);
    }
    void Movement()
    {
        transform.Translate(verticalInput * speed * Time.fixedDeltaTime, 0, 0);
        animator.SetBool("IsRun", verticalInput != 0);
    }

    void Jump()
    {
        if (isJumpKeyPressed == true && isTouchingGround == true && isJump == false)
        {
            isJump = true;
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if(playerRb.velocity.y == 0)
        {
            isJump = false;
            isFall = false;
        }
        if(playerRb.velocity.y < 0)
        {
            isJump = false;
            isFall = true;
        }
        JumpCut();
    }

    void JumpCut()
    {
        if (playerRb.velocity.y > 0 && isJump == true && isJumpKeyPressed == false)
        {
            playerRb.AddForce(Vector2.down * playerRb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            Debug.Log("jumpCut");
        }
    }

    void Flip(bool right)
    {
        if ((right == true && 0 > gameObject.transform.localScale.x)
            || (right == false && 0 < gameObject.transform.localScale.x))
        {
            int r = (right) ? 1 : -1;
            gameObject.transform.localScale = new Vector3(r, 1, 1);
        }
    }
    
    //Key Event Functions
    public void OnMovement(InputAction.CallbackContext context)
    {
        verticalInput = context.ReadValue<Vector2>().x;
        if(context.started)
        Flip(0 < verticalInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started == true)
        {
            isJumpKeyPressed = true;
        }
        if(context.canceled == true)
        {
            isJumpKeyPressed = false;
        }
    }

}
