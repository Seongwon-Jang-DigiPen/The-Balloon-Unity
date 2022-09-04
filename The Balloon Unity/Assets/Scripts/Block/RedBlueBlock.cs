using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum REDBLUESTATE
{
    RED, BLUE
}



public class RedBlueBlock : MonoBehaviour, IEnumerable
{
    public REDBLUESTATE blockState = REDBLUESTATE.RED;

    public Sprite activateSprite = null;
    public Sprite deactivateSprite = null;

    [HideInInspector]
    public static RedBlueBlock lastCreated = null;
    [HideInInspector]
    public static RedBlueBlock firstCreated = null;
    [HideInInspector]
    public RedBlueBlock nextBlock = null;
    [HideInInspector]
    public RedBlueBlock prevBlock = null;

    private SpriteRenderer spriteRenderer = null;
    private BoxCollider2D collision2D = null;
    private void Awake()
    {
        if(firstCreated == null)
        {
            firstCreated = this;
        }
        if(RedBlueBlock.lastCreated != null)
        {
            lastCreated.nextBlock = this;
            prevBlock = lastCreated;
        }
        lastCreated = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        collision2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        ChangeState();
    }

    public void ChangeState()
    {
        if (RedBlueSwitch.WorldRedBlueState == blockState)
        {
            spriteRenderer.sprite = activateSprite;
            collision2D.enabled = true;
            //collision2D.isTrigger = false;
        }
        else
        {
            spriteRenderer.sprite = deactivateSprite;
            collision2D.enabled = false; 
            //collision2D.isTrigger = true;
        }
    }

    private void OnDestroy()
    {
        if(prevBlock != null)
        {
            prevBlock.nextBlock = nextBlock;
        }
        if(nextBlock != null)
        {
            nextBlock.prevBlock = prevBlock;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new RedBlueEnumerator();
    }
}
