using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Platform")
        {
            Destroy(this.gameObject);
        }
    }
}
