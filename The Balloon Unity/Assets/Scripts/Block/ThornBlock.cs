using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag(Player.playerTag))
        {
            collision.collider.GetComponent<PlayerControl>().Hitted();
        }
    }
}
