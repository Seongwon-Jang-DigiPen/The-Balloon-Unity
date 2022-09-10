using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSwitch : Switch
{
    public SpriteRenderer bodyRenderer;
    public Sprite bodyActivate;
    public Sprite bodyDeactivate;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag) || collision.CompareTag("Box") || collision.CompareTag("Monster"))
        {
            spriteRenderer.sprite = activateSprite;
            if (bodyActivate != null)
                bodyRenderer.sprite = bodyActivate;
            turnSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag) || collision.CompareTag("Box") || collision.CompareTag("Monster"))
        {
            spriteRenderer.sprite = deactivateSprite;
            if(bodyActivate != null)
                bodyRenderer.sprite = bodyDeactivate;
           turnSwitch = false;
        }
    }
}
