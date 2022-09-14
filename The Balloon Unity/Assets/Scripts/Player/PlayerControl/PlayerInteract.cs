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
    private Vector3 boxDistance;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && canMove == true)
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
            else if (player.balloonState.state == BALLOONSTATE.ELECTRIC)
            {
                if (checker.interactedObj != null)
                {
                    SoundManager.instance.PlaySound("ElectricInteract");
                    boxDistance = checker.interactedObj.transform.position - player.transform.position;
                    isCatched = true;
                    flipLock = true;
                }
            }
            else if (player.balloonState.state == BALLOONSTATE.WATER)
            {
                if (isHitted == false && isInteract == false && isDoAction == false && isCatched == false)
                {
                    SprinkleNum = 1;
                    Sprinkle();
                }
            }
        }
        
        if(context.canceled)
        {
            isCatched = false;
            flipLock = false;
            if (checker.interactedObj != null)
            {
                checker.interactedObj?.GetComponent<ElectricBox>()?.isCatched(false);
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
            SprinkleNum = 3;
            StartCoroutine(IInvincible());
        }
        isInteract = false;
    }
    
    void CatchBox()
    {
        if(player.balloonState.state == BALLOONSTATE.ELECTRIC)
        {
            animator.SetBool("IsCatched", isCatched);
            animator.SetBool("LookRight", transform.localScale.x > 0);
            animator.SetFloat("Horizontal", inputValue.x);
        }
        if (isCatched == true)
        {
            if (isHitted == true || isBoost == true || isTouchingGround == false || checker.interactedObj == null)
            {
                isCatched = false;
                flipLock = false;
                checker.interactedObj?.GetComponent<ElectricBox>()?.isCatched(false);
                return;
            }
            if (checker.interactedObj != null)
            {
                if (checker.interactedObj.GetComponent<ElectricBox>() != null)
                {
                    checker.interactedObj.GetComponent<ElectricBox>()?.isCatched(true);
                    checker.interactedObj.transform.position = boxDistance + player.transform.position;
                }
            }
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

