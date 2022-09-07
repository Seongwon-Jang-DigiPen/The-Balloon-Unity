using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudy : MoveBlock
{
    Animator animator;
    SpriteRenderer render;
    BoxCollider2D boxCollider; 
    bool isHitted = false;
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

    private void OnCollisionStay2D(Collision2D collision)
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
        Debug.Log("Cloudy Hitted");
        isHitted = true;
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
