using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo : MoveBlock
{
    Animator animator;
    SpriteRenderer render;
    bool isHitted = false;
    public float hittedTime = 5.0f;
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
  
    protected override void FixedUpdate()
    {
        if(isHitted == false)
        {
            BlockMove();
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
        animator.SetTrigger("Hitted");

        yield return new WaitForSeconds(hittedTime);

        animator.SetTrigger("Idle");
        isHitted = false;
    }
}
