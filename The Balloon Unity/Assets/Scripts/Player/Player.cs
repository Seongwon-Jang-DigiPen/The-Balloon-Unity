using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [HideInInspector]
    public BalloonState balloonState;
    public static string playerTag = "Player";

    [Header("StateData")]

    [SerializeField]
    private BalloonState NORMALSTATE;
    [SerializeField]
    private BalloonState FLATSTATE;
    [SerializeField]
    private BalloonState WATERSTATE;
    [SerializeField]
    private BalloonState ELECTRICSTATE;

    /*public*/
    [Header("Movement")]
    public float MaxSpeed = 10.0f;
    public float MaxDownSpeed = 4f;
    public float MaxDownFastSpeed = 7f;
    public float jumpForce = 0.0f;
    public float acceleration = 3.0f;
    public float decceleration = 3.0f;


    private Rigidbody2D playerRb = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;
    private BoxCollider2D boxCollider = null;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>(); 
    }

    private void Start()
    {
        ChangeState(BALLOONSTATE.NORMAL);
        
    }
    private void Update()
    {
        ChangeColliderSize();
    }
    public void ChangeState(BALLOONSTATE state)
    {
        switch (state)
        {
            case BALLOONSTATE.Flat:
                balloonState = FLATSTATE;
                break;
            case BALLOONSTATE.NORMAL:
                balloonState = NORMALSTATE;
                break;
            case BALLOONSTATE.WATER:
                balloonState = WATERSTATE;
                break;
            case BALLOONSTATE.ELECTRIC:
                balloonState = ELECTRICSTATE;
                break;
        }
        StateEnter();
    }

    void StateEnter()
    {
        playerRb.mass = balloonState.rigid.mass;
        playerRb.drag = balloonState.rigid.drag;
        playerRb.angularDrag = balloonState.rigid.angularDrag;
        playerRb.gravityScale = balloonState.rigid.gravityScale;
        MaxSpeed = balloonState.MaxSpeed;
        acceleration = balloonState.acceleration;
        decceleration = balloonState.decceleration;
        jumpForce = balloonState.jumpForce;
        animator.runtimeAnimatorController = balloonState.animator.runtimeAnimatorController;
        MaxDownFastSpeed = balloonState.MaxDownFastSpeed;
        MaxDownSpeed = balloonState.MaxDownSpeed;
    }

    void ChangeColliderSize()
    {
        boxCollider.size = spriteRenderer.sprite.bounds.size - new Vector3(0.05f,0.05f,0);
        //boxCollider.bounds = renderer.sprite.bounds;
       // boxCollider.bounds = renderer.sprite.bounds;
    }
}
