using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Clear : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
            PlayerControl.canMove = false;
            Destroy(this.gameObject);
        }
    }

}
