using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            SoundManager.instance.PlayBGM("Chapter1BGM", true);
            Destroy(this.gameObject);
        }
    }
}
