using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public BalloonState balloonState;

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
    public float jumpForce = 0.0f;
    public float acceleration = 3.0f;
    public float decceleration = 3.0f;


    private Rigidbody2D playerRb = null;
    private Animator animator = null;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeState(BALLOONSTATE.NORMAL);
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
    }
}
