using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinny : MonoBehaviour
{
    Vector3 moveVelocity;
    public float movePower = 1f;
    private bool isTracing = false;
    private GameObject target;
    private Rigidbody2D Rb;
    private Animator animator;
    private int movementFlag = 0; // 0 = idle, 1 = left, 2 = right

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(ChangeMovement());
    }
    private void Update()
    {
        animator.SetBool("IsRun", movementFlag != 0);
    }
    void FixedUpdate()
    {
        Move();
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);
        yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));

        StartCoroutine(ChangeMovement());
    }

    private void Move()
    {
        if(isTracing == true)
        {
            Vector3 playerPos = target.transform.position;

            if(playerPos.x < transform.position.x)
            {
                movementFlag = 1;
            }
            else if(playerPos.x > transform.position.x)
            {
                movementFlag = 2;
            }
        }
        if(movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            moveVelocity = Vector3.zero;
        }
        Rb.velocity = moveVelocity * movePower;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Player.playerTag))
        {
            target = collision.gameObject;
            isTracing = true;
            StopCoroutine(ChangeMovement());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Player.playerTag))
        {
            isTracing = false;
            StartCoroutine(ChangeMovement());
        }
    }

    
}
