using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSwitch : Switch
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            spriteRenderer.sprite = activateSprite;
            turnSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            spriteRenderer.sprite = deactivateSprite;
            turnSwitch = false;
        }
    }
}
