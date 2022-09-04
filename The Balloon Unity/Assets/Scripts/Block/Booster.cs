using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float boostPower = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Player.playerTag))
        {
            Vector2 angle = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z), Mathf.Sin( Mathf.Deg2Rad * transform.localRotation.eulerAngles.z)).normalized;
            collision.GetComponent<PlayerControl>().Boost(angle, boostPower);   
        }
    }

}
