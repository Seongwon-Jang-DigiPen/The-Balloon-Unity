using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float boostPower = 10;
    public Vector2 angle;
   
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.CompareTag(Player.playerTag))
        {
            collision.transform.position = this.transform.position;
            PlayerControl pc = collision.GetComponent<PlayerControl>();
            pc.boostAngle = angle.normalized;
            pc.boostPower = boostPower;
            SoundManager.instance.PlaySound("Booster");
            pc.Boost();
        }
    }

}
