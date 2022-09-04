using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArea : MonoBehaviour
{
    public float waterForce = 1.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag) == true && collision.GetComponent<Player>().balloonState.state == BALLOONSTATE.WATER)
        {
            return;
        }
        collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * waterForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
