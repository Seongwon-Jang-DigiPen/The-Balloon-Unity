using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlueSwitch : MonoBehaviour
{
    public static REDBLUESTATE WorldRedBlueState = REDBLUESTATE.RED;

    public Sprite redOnSprite;
    public Sprite blueOnSprite;
    private SpriteRenderer spriteRenderer = null;
    public bool touched = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRenderer.sprite = (WorldRedBlueState == REDBLUESTATE.RED) ? redOnSprite : blueOnSprite;
    }

    private void Update()
    {
        spriteRenderer.sprite = (WorldRedBlueState == REDBLUESTATE.RED) ? redOnSprite : blueOnSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag) == true)
        {
            SoundManager.instance.PlaySound("RedBlueSwitch");
            WorldRedBlueState = (WorldRedBlueState == REDBLUESTATE.RED) ? REDBLUESTATE.BLUE : REDBLUESTATE.RED;
            spriteRenderer.sprite = (WorldRedBlueState == REDBLUESTATE.RED) ? redOnSprite : blueOnSprite;
            touched = true;
            if (RedBlueBlock.firstCreated != null)
            {
                foreach (RedBlueBlock rbBlock in RedBlueBlock.firstCreated)
                {
                    rbBlock.ChangeState();
                }
            }
        }
    }
}
