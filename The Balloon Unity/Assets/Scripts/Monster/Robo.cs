using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo : MoveBlock
{
    Animator animator;
    SpriteRenderer render;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        render.flipX = (rigid.velocity.x > 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    IEnumerator Hitted()
    {
        animator.SetTrigger("Hitted");
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
