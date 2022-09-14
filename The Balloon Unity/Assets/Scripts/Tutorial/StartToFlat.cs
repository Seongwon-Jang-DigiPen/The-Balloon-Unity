using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartToFlat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Player.playerTag) == true)
        {
            collision.GetComponent<Player>().ChangeState(BALLOONSTATE.Flat);
            Destroy(this.gameObject);
        }
    }
}
