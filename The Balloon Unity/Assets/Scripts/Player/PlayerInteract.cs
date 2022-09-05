using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl
{
    bool isInteract = false;

    void GetWater() 
    {
        if(isHitted == false && isTouchingGround == true)
        {
            StartCoroutine(IGetWater());
        }
    }
    IEnumerator IGetWater()
    {
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

