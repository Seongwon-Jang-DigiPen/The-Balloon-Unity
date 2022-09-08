using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangMark : MonoBehaviour
{
    Animator animator;
    SpriteRenderer render;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
    }

    public void StartBangMark()
    {
        StartCoroutine(StartBang());
    }

    IEnumerator StartBang()
    {
        render.enabled = true;
        animator.SetTrigger("Bang");
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Bang") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                break;
            }
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).ToString());
            yield return null;
        }
        animator.SetTrigger("EndBang");
        render.enabled = false;

    }
}
