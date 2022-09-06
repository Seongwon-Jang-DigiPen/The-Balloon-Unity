using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl
{
    bool isInsideWater = false;
    bool isNearFurryBlock = false;
    bool isOnFurryBlock = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            Hitted();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("FurryBlock"))
        {
            if(transform.position.y > collision.gameObject.transform.position.y)
            {
                isOnFurryBlock = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("FurryBlock"))
        {
            isNearFurryBlock = false;
            if (transform.position.y > collision.gameObject.transform.position.y)
            {
                isOnFurryBlock = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isInsideWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isInsideWater = false;
        }
    }
}
