using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool turnSwitch = false;
    public Sprite activateSprite = null;
    public Sprite deactivateSprite = null;

    protected SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = deactivateSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = activateSprite;
            if(turnSwitch == false)
            SoundManager.instance.PlaySound("Switch");
            CameraShake.instance.DoShake();
            turnSwitch = true;
        }
    }
}
