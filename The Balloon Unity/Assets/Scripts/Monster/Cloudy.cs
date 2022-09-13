using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudy : MoveBlock
{
    public Collider2D traceLimit;
    private GameObject player;
    Animator animator;
    SpriteRenderer render;
    BoxCollider2D boxCollider; 
    bool isHitted = false;
    bool isTrace = false;
    public BangMark bang;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        render.flipX = (rigid.velocity.x > 0);
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
        direction = (playerPos - transform.position).normalized;
        rigid.velocity = direction * moveSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            player = collision.gameObject;

            if (traceLimit?.IsTouching(player.GetComponent<Collider2D>()) == true)
            {
                if (isTrace == false)
                {
                    SoundManager.instance.PlaySound("FindPlayer");
                    bang.StartBangMark();
                    isTrace = true;
                }
            }
            else
            {
                isTrace = false;
            }
        }
        else if (isHitted == false && collision.CompareTag("WaterBomb"))
        {
            StartCoroutine(Hitted());
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            isTrace = false;
        }
    }
    
    protected override void OnCollisionStay2D(Collision2D collision)
    {

        if(isHitted == false && collision.gameObject.CompareTag(Player.playerTag) == true)
        {
            if(collision.gameObject.GetComponent<PlayerControl>().isDash == true)
            {
                StartCoroutine(Hitted());
            }
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
