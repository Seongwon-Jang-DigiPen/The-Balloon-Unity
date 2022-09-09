using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public partial class PlayerControl
{
    [Header("Interact")]
    public InteractCheck checker;
    bool isInteract = false;
    bool isCatched = false;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (player.balloonState.state == BALLOONSTATE.NORMAL)
            {
                if (isDoAction == false && isHitted == false && isInteract == false)
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
            if (player.balloonState.state == BALLOONSTATE.ELECTRIC)
            {
                if(checker.interactedObj != null)
                {
                    isCatched = true;
                    flipLock = true;
                }
            }
        }
        
        if(context.canceled)
        {
            isCatched = false;
            flipLock = false;
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
        isInteract = true;
        animator.SetTrigger("GetWater");
        SoundManager.instance.PlaySound("NormalToWater");
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
    
    void CatchBox()
    {
        if(isCatched == true)
        {
            if (isHitted == true || isBoost == true || isTouchingGround == false)
            {
                isCatched = false;
                flipLock = false;
                return;
            }
            checker.interactedObj?.transform.Translate(playerRb.velocity * Time.fixedDeltaTime);
        }
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
            SoundManager.instance.PlaySound("NormalToElectric");
            StartCoroutine(IInvincible());
        }
        
        isInteract = false;
    }

}

