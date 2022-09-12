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
       if(switchObj?.turnSwitch == true)
        {
            if(spriteRenderer.sprite != activateSprite)
            {
                spriteRenderer.sprite = activateSprite;
            }
            base.FixedUpdate();
        }
        else
        {
            if (spriteRenderer.sprite != deactivateSprite)
            {
                spriteRenderer.sprite = deactivateSprite;
            }
            rigid.velocity = Vector2.zero;
        }
    }
    protected override void OnDrawGizmos()
    {
        if(switchObj != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, switchObj.transform.position);
        }
        base.OnDrawGizmos();
    }
}
