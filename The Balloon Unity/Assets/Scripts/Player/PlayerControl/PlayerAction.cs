using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl
{
    [Header("Action")]
    [Header("Dash")]
    public float dashTime = 1.0f;
    public float dashForce = 10.0f;
    [Header("GetAir")]
    public float getAirTime = 3.0f;
    [Header("Sprinkle")]
    public GameObject waterBomb;
    public float sprinkleTime = 2.0f;

    private bool isDoAction = false;

    public bool isDash { get { return isDoAction == true && player.balloonState.state == BALLOONSTATE.NORMAL; } }
    void DoAction() 
    {
        if (isHitted == false && isInteract == false && isDoAction == false && isCatched == false)
        {
            switch (player.balloonState.state)
            {
                case BALLOONSTATE.Flat:
                    GetAir();
                    break;
                case BALLOONSTATE.NORMAL:
                    Dash();
                    break;
                case BALLOONSTATE.WATER:
                    Sprinkle();
                    break;
            }
        }
    }

    void GetAir()
    {
        if (isTouchingGround == true)
        {
            StartCoroutine(IGetAir());
        }
    }
    IEnumerator IGetAir()
    {
        animator.SetTrigger("GetAir");
        SoundManager.instance.PlaySound("FlatToNormal");
        isDoAction = true;
        playerRb.velocity = new Vector3(0, 0);
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("GetAir") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                break;
            }
            if (isHitted == true)
            {
                break;
            }
            yield return null;
        }
        if (isDoAction == true)
        {
            player.ChangeState(BALLOONSTATE.NORMAL);
            StartCoroutine(IInvincible());
            isDoAction = false;
        }
    }

    void Dash()
    {
        StartCoroutine(IDash());
    }

    IEnumerator IDash()
    {
        SoundManager.instance.PlaySound("Dash");
        isDoAction = true;
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce((Vector2.right * transform.localScale.x + new Vector2(0,0.2f)) * dashForce, ForceMode2D.Impulse);
        animator.SetTrigger("Dash");
        float gravity = playerRb.gravityScale;
        playerRb.gravityScale = 0;

        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                break;
            }
            if (isHitted == true)
            {
                break;
            }
            yield return null;
        }
        if (isHitted == false)
        {
            playerRb.gravityScale = gravity;
            playerRb.velocity = new Vector3(0, 0);
            animator.SetTrigger("ChangeState");
            player.ChangeState(BALLOONSTATE.Flat);
            isDoAction = false;
            StartCoroutine(IInvincible());
        }
    }
    void Sprinkle() { StartCoroutine(ISprinkle()); }
    IEnumerator ISprinkle()
    {
        isDoAction = true;
        yield return new WaitForSeconds(sprinkleTime);
        isDoAction = false;
    }
}
