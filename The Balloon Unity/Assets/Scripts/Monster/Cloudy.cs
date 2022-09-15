using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudy : MoveBlock
{
    public Collider2D traceLimit;
    public Collider2D findLimit;
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
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
    }
    private void Update()
    {
        render.flipX = (rigid.velocity.x > 0);
    }
    protected override void FixedUpdate()
    {
        if (isHitted == false)
        {
            checkPlayer();
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

    void checkPlayer()
    {
        if (findLimit.IsTouching(player.GetComponent<Collider2D>()) == true)
        {
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
        else
        {
            isTrace = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHitted == false && collision.CompareTag("WaterBomb"))
        {
            StartCoroutine(Hitted());
            Destroy(collision.gameObject);
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
        boxCollider.isTrigger = true;
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
