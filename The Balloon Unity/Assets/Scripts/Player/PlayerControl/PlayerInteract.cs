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
        yield return new WaitForEndOfFrame();
        float curAnimTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(curAnimTime);
        if (isHitted == false)
        {
            animator.SetTrigger("ChangeState");
            player.ChangeState(BALLOONSTATE.WATER);
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
        animator.SetTrigger("GetElectric");
        yield return new WaitForEndOfFrame();
        float curAnimTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(curAnimTime);
        if (isHitted == false)
        {
            animator.SetTrigger("ChangeState");
            player.ChangeState(BALLOONSTATE.ELECTRIC);
        }
        isInteract = false;
    }

}

