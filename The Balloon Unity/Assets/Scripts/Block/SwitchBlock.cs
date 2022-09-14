using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    public Switch switchObj;

    SpriteRenderer spriteRenderer;
    Collider2D col;
    public Sprite activateSprite = null;
    public Sprite deactivateSprite = null;
    public bool inverseCollision = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        spriteRenderer.sprite = deactivateSprite;
        col.enabled = inverseCollision;
    }

    private void Update()
    {
        if(switchObj?.turnSwitch == true && spriteRenderer.sprite != activateSprite)
        { 
            spriteRenderer.sprite = activateSprite;
            col.enabled = !inverseCollision;
        }
    }

    private void OnDrawGizmos()
    {
        if (switchObj != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, switchObj.transform.position);
        }
    }
}
