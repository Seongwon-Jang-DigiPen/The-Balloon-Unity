using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinny : MoveBlock
{
    bool isTrace = false;
    bool isHitted = false;
    public Collider2D traceLimit;
    private GameObject player;
    Animator animator;
    BoxCollider2D boxCollider;
    public BangMark bang;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        animator.SetBool("IsRun", rigid.velocity != Vector2.zero);
    }
    protected override void FixedUpdate()
    {
        if (isHitted == false)
        {
            if (isTrace == false)
            {
                base.FixedUpdate();
            }
            else
            {
                TraceTarget();
            }
        }
    }

    void TraceTarget()
    {
        Vector3 playerPos = player.transform.position;

        if (playerPos.x < transform.position.x)
        {
            direction = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (playerPos.x > transform.position.x)
        {
            direction = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rigid.velocity = direction * moveSpeed + new Vector2(0, rigid.velocity.y);
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (isHitted == false && collision.gameObject.CompareTag(Player.playerTag) == true)
        {
            if (collision.gameObject.GetComponent<PlayerControl>().isDash == true)
            {
                StartCoroutine(Hitted());
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            player = collision.gameObject;

            if (traceLimit?.IsTouching(player.GetComponent<Collider2D>()) == true)
            {
                if(isTrace == false)
                {
                    bang.StartBangMark();
                    isTrace = true;
                }
            }
            else
            {
                isTrace = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            isTrace = false;
        }
    }

    IEnumerator Hitted()
    {
        isHitted = true;
        SoundManager.instance.PlaySound("DashHit");
        animator.SetTrigger("Hitted");
        boxCollider.enabled = false;
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                break;
            }
            yield return null;
        }
        Destroy(this.gameObject);
    }
}