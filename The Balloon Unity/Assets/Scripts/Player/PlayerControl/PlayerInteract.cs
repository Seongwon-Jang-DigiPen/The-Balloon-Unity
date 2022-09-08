using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public partial class PlayerControl
{
    bool isInteract = false;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isDoAction == false && isHitted == false && isInteract == false && player.balloonState.state == BALLOONSTATE.NORMAL)
            {
                if (isInsideWater == true)
                {
                    GetWater();
                }
                else if (isNearFurryBlock == true)
                {
                    GetElectric();
                }
            }
        }
    }

    void GetWater() 
    {
        if (isHitted == false)
        {
            StartCoroutine(IGetWater());
        }
    }
    IEnumerator IGetWater()
    {
        Debug.Log("IGetWater");
        isInteract = true;
        animator.SetTrigger("GetWater");
        while (true)
        {
            playerRb.velocity = new Vector2(0, 0);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("GetWater") &&
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
            animator.SetTrigger("ChangeState");
            player.ChangeState(BALLOONSTATE.WATER);
            StartCoroutine(IInvincible());
        }
        isInteract = false;
    }

    void GetElectric()
    {
        if (isHitted == false && isTouchingGround == true)
        {
            StartCoroutine(IGetElectric());
        }
    }
    IEnumerator IGetElectric()
    {
        isInteract = true;
        if(isOnFurryBlock == true)
        {
            animator.SetTrigger("GetElectricDown");
        }
        else
        {
            animator.SetTrigger("GetElectricSide");
        }

       while(true)
        {
            playerRb.velocity = new Vector2(0, 0);
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("GetElectricSide") || animator.GetCurrentAnimatorStateInfo(0).IsName("GetElectricDown")) &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                break;
            }
            if(isHitted == true)
            {
                break;
            }
            yield return null;
        }
        
        if (isHitted == false)
        {
            animator.SetTrigger("ChangeState");
            player.ChangeState(BALLOONSTATE.ELECTRIC);
            StartCoroutine(IInvincible());
        }
        
        isInteract = false;
    }

}

