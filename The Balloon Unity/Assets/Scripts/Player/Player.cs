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
        if(LoadingSceneController.isDataLoad == true)
        {
            gameObject.transform.position = new Vector3(LoadingSceneController.saveData.xPos, LoadingSceneController.saveData.yPos, gameObject.transform.position.z);
        }

    }
    private void Update()
    {
    }
    public void ChangeState(BALLOONSTATE state)
    {
        switch (state)
        {
            case BALLOONSTATE.Flat:
                balloonState = FLATSTATE;
                EventManager.Instance.PostNotification(EVENT_TYPE.Player_Change_Flat, this);
                break;
            case BALLOONSTATE.NORMAL:
                balloonState = NORMALSTATE;
                EventManager.Instance.PostNotification(EVENT_TYPE.Player_Change_Normal, this);
                break;
            case BALLOONSTATE.WATER:
                balloonState = WATERSTATE;
                EventManager.Instance.PostNotification(EVENT_TYPE.Player_Change_Water,this);
                break;
            case BALLOONSTATE.ELECTRIC:
                balloonState = ELECTRICSTATE;
                EventManager.Instance.PostNotification(EVENT_TYPE.Player_Change_Electric, this);
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

        this.gameObject.layer = balloonState.layer;
        StartCoroutine(IChangeColliderSize());
        
    }

    IEnumerator IChangeColliderSize()
    {
        yield return null;
        ChangeColliderSize();
    }
    public void ChangeColliderSize()
    {
        boxCollider.offset = new Vector3(0, -0.1f, 0);
        boxCollider.size = spriteRenderer.sprite.bounds.size - new Vector3(0.2f,0.23f,0);
    }
}
