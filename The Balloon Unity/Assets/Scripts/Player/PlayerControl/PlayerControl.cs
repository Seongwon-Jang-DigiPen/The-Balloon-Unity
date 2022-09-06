using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public partial class PlayerControl : MonoBehaviour
{


    [Header("Collision")]
    public Transform groundCheck;
    public float groundCheckLength = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump")]
    public float jumpCutMultiplier = 1.0f;
    [HideInInspector]
    public bool isTouchingGround = false;

    [Header("DownFast")]
    public float downFastSpeed = 7;

    [Header("Hitted")]
    float invincibleTime = 1f;
    /*private*/
    //input data
    private Vector2 inputValue = new Vector2(0,0);
    private bool isJumpKeyPressed = false;
    private bool isJump = false;
    private bool isFall = false;

    private Rigidbody2D playerRb = null;
    private Animator animator = null;
    private Player player = null;
    private BoxCollider2D boxCollider = null;

    private bool isHitted = false;
    private bool isInvincible = false;
    private void Awake()
    {
        player = GetComponent<Player>();
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Cheat();
        Flip();
        AnimInfo();
    }
    void FixedUpdate()
    {
        CheckGround();
        if (isDoAction == false && isInteract == false)
        {
            Movement();
            Jump();
            DownFast();
        }
    }

    void CheckGround()
    {
        isTouchingGround = Physics2D.OverlapBox(transform.position - new Vector3(0, boxCollider.size.y / 2), new Vector2(boxCollider.size.x * 0.9f, boxCollider.size.y * 0.2f),0, groundLayer);
        //isTouchingGround =Physics2D.OverlapCircle(transform.position - new Vector3(0, boxCollider.size.y / 2), boxCollider.size.x * 4 / 10, groundLayer);
        //isTouchingGround = Physics2D.Linecast(transform.position, transform.position + Vector3.down * groundCheckLength, groundLayer);
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckLength);
    }
    void Movement()
    {
        if (inputValue.x != 0)
        {
            playerRb.AddForce(new Vector2(inputValue.x, 0) * player.acceleration);
        }
        else if (Mathf.Abs(playerRb.velocity.x) < 0.1f)
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        }
        if(playerRb.velocity.x != 0)
        {
            if(playerRb.velocity.x > 0f && inputValue.x <= 0)
            {
                playerRb.AddForce(Vector2.left * player.decceleration);
            }
            else if (playerRb.velocity.x < -0f && inputValue.x >= 0)
            {
                playerRb.AddForce(Vector2.right * player.decceleration);
            }
        }
        
        if(playerRb.velocity.x >player.MaxSpeed)
        {
            playerRb.velocity = new Vector2(player.MaxSpeed, playerRb.velocity.y);
        }
        else if (playerRb.velocity.x < -player.MaxSpeed)
        {
            playerRb.velocity = new Vector2(-player.MaxSpeed, playerRb.velocity.y);
        }
    }
    void Jump()
    {
        if (isJumpKeyPressed == true && isTouchingGround == true && isJump == false)
        {
            SoundManager.instance?.PlaySound("Jump");
            isJump = true;
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
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

    void DownFast()
    {
        if(playerRb.velocity.y < -player.MaxDownSpeed)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x,  -player.MaxDownSpeed);
        }
        if(isTouchingGround == false && isJumpKeyPressed == false && inputValue.y < 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -player.MaxDownFastSpeed);
        }

    }

    void JumpCut()
    {
        if (playerRb.velocity.y > 0 && isJump == true && isJumpKeyPressed == false)
        {
            playerRb.AddForce(Vector2.down * playerRb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        if ((playerRb.velocity.x > 0.1f && 0 > gameObject.transform.localScale.x)
            || (playerRb.velocity.x < -0.1f && 0 < gameObject.transform.localScale.x))
        {
                int r = (playerRb.velocity.x > 0) ? 1 : -1;
            gameObject.transform.localScale = new Vector3(r, 1, 1);
        }
    }
    
    //Key Event Functions
    public void OnMovement(InputAction.CallbackContext context)
    {
        inputValue = context.ReadValue<Vector2>();
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

    public void OnAction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            DoAction();
        }
    }

    public void Boost(Vector2 angle, float power)
    {
        isJump = false;
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(angle * power, ForceMode2D.Impulse);
    }

    public void Hitted()
    {
        if (isHitted == false)
        {
            StartCoroutine(IHitted());
        }
    }

    IEnumerator IHitted()
    {
        isHitted = true;
        animator.SetTrigger("IsHitted");
        yield return new WaitForSeconds(0.01f);
        float curAnimTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(curAnimTime);
        if (player.balloonState.state == BALLOONSTATE.Flat)
        {

        }
        else
        {
            animator.SetTrigger("ChangeState");
            player.ChangeState(BALLOONSTATE.Flat);

            isHitted = false;
            isInvincible = true;
            yield return new WaitForSeconds(invincibleTime);

            isInvincible = false;
        }
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.ChangeState(BALLOONSTATE.Flat);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.ChangeState(BALLOONSTATE.NORMAL);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.ChangeState(BALLOONSTATE.ELECTRIC);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.ChangeState(BALLOONSTATE.WATER);
        }
    }

    private void AnimInfo()
    {
        animator.SetBool("IsRun", inputValue.x != 0);
        animator.SetBool("IsJump", isJump);
        animator.SetBool("IsFall", !isTouchingGround);
    }


    private void OnDrawGizmos()
    {
        //Gizmos.color =new Vector4(1,1,1,0.5f);
        //Gizmos.DrawSphere(transform.position - new Vector3(0, boxCollider.size.y/2), boxCollider.size.x * 4 / 10);
        //Gizmos.DrawCube(transform.position - new Vector3(0, boxCollider.size.y / 2), new Vector2(boxCollider.size.x * 0.9f, boxCollider.size.y * 0.2f));
        //Physics2D.OverlapBox(transform.position - new Vector3(0, boxCollider.size.y / 2), boxCollider.size, groundLayer);
    }
}



