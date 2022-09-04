using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricMoveBlock : MoveBlock
{
    private bool CollidedPlayer;
    SpriteRenderer spriteRenderer;
    public float ElectricTime = 0.5f;
    float timer = 0;
    public Sprite ActivateSprite;
    public Sprite DeactivateSprite;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void FixedUpdate()
    {
        if (CollidedPlayer == true)
        {
            if(spriteRenderer.sprite != ActivateSprite)
            {
                spriteRenderer.sprite = ActivateSprite; 
            }
            BlockMove();
            timer += Time.fixedDeltaTime;
            if(timer >= ElectricTime)
            {
                CollidedPlayer = false;
            }
        }
        else
        {
            if (spriteRenderer.sprite != DeactivateSprite)
            {
                spriteRenderer.sprite = DeactivateSprite;
            }
            rigid.velocity = Vector2.zero;
        }
    
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag(Player.playerTag))
        {
            PlayerControl temp = collision.collider.GetComponent<PlayerControl>();
            if(temp.isTouchingGround == true)
            {
                CollidedPlayer = true;
                timer = 0;
            }
        }
    }
}
