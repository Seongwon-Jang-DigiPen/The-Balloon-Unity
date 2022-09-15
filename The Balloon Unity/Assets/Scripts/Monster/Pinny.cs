using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinny : MoveBlock
{
    bool isTrace = false;
    bool isHitted = false;
    public Collider2D traceLimit;
    public Collider2D findLimit;
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
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHitted == false && collision.CompareTag("WaterBomb"))
        {
            StartCoroutine(Hitted());
            Destroy(collision.gameObject);
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