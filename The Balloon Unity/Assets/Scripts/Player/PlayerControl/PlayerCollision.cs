using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl
{
    bool isInsideWater = false;
    bool isNearFurryBlock = false;
    bool isOnFurryBlock = false;
    bool isInsideWind = false;
    WindArea windArea = null;
    bool isCollidedAnything = false;
    private void OnCollisionStay2D(Collision2D collision)
    {
        isCollidedAnything = true;
        if (collision.collider.CompareTag("Monster"))
        {
            if (isDash == false)
            {
                Hitted();
            }
        }
        else if (collision.collider.CompareTag("FurryBlock"))
        {
            if(transform.position.y - 0.5f > collision.gameObject.transform.position.y)
            {
                isOnFurryBlock = true;
            }
            isNearFurryBlock = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollidedAnything = false;
        if (collision.collider.CompareTag("FurryBlock"))
        {
            isNearFurryBlock = false;
            isOnFurryBlock = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            ParticleManager.instance?.PlayParticle(this.gameObject, ParticleManager.ParticleType.IntoWater);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isInsideWater = true;
            if(player.balloonState.state == BALLOONSTATE.ELECTRIC)
            {
                player.ChangeState(BALLOONSTATE.NORMAL);
            }
        }
        else if(collision.CompareTag("Wind"))
        {
            isInsideWind = true;
            windArea = collision.GetComponent<WindArea>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isInsideWater = false;
        }
        else if (collision.CompareTag("Wind"))
        {
            isInsideWind = false;
        }
    }
}
