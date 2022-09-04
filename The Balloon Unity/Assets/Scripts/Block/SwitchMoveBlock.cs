using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMoveBlock : MoveBlock
{
    public Switch switchObj;

    SpriteRenderer spriteRenderer;
    public Sprite activateSprite = null;
    public Sprite deactivateSprite = null;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = deactivateSprite;
    }
    protected override void FixedUpdate()
    {
       if(switchObj.turnSwitch == true)
        {
            if(spriteRenderer.sprite != activateSprite)
            {
                spriteRenderer.sprite = activateSprite;
            }
            base.FixedUpdate();
        }
    }
}
