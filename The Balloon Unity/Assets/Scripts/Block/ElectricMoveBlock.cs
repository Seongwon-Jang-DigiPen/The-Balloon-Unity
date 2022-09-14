using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricMoveBlock : MoveBlock
{
    private bool CollidedPlayer;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public float ElectricTime = 0.5f;
    float timer = 0;
    public Sprite ActivateSprite;
    public Sprite DeactivateSprite;
    public RuntimeAnimatorController ActivateAnimator = null;
    public RuntimeAnimatorController DeactivateAnimator = null;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        CollidedPlayer = false;
        moveOffset = -1;
    }

    private void Update()
    {
        if (CollidedPlayer == true)
        {
            if (spriteRenderer.sprite != ActivateSprite)
            {
                spriteRenderer.sprite = ActivateSprite;
                animator.runtimeAnimatorController = ActivateAnimator;
                moveOffset = 1;
                if(index < posList.Count - 1) { index += 1; }
            }
            timer += Time.deltaTime;
            if (timer >= ElectricTime)
            {
                CollidedPlayer = false;
                moveOffset = -1;
                if(index > 0) { index -= 1; }
            }
        }
        else
        {
            if (spriteRenderer.sprite != DeactivateSprite)
            {
                spriteRenderer.sprite = DeactivateSprite;
                animator.runtimeAnimatorController = DeactivateAnimator;
            }
            moveOffset = -1;
        }
    }

    protected override void FixedUpdate()
    {
        BlockMove();
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);

        if(collision.collider.CompareTag(Player.playerTag) && OnCollider.IsTouching(collision.collider))
        {
            PlayerControl temp = collision.collider.GetComponent<PlayerControl>();
            if(collision.collider.GetComponent<Player>().balloonState.state == BALLOONSTATE.ELECTRIC)
            {
                if(CollidedPlayer == false)
                {
                    SoundManager.instance.PlaySound("ElectricInteract");
                }
                CollidedPlayer = true;
                timer = 0;
            }
        }
    }

    protected override void BlockMove()
    {
        if (isStop == false)
        {
            if (posList.Count > index && index >= 0)
            {
                if (posList[index] != null)
                {
                    Vector2 temp = (posList[index] - transform.position);
                    length = temp.magnitude;
                    direction = temp.normalized;
                    rigid.velocity = direction * moveSpeed;
                }
                if (length <= (direction * moveSpeed * Time.fixedDeltaTime).magnitude + 0.01f)
                {
                    transform.position = posList[index];
                    rigid.velocity = Vector2.zero;
                    index += moveOffset;
                    if (index == posList.Count)
                    {
                        index = posList.Count - 1;
                        StartCoroutine(IStop());
                    }
                    if(index < 0)
                    {
                        index = 0;
                        StartCoroutine(IStop());
                    }
                }
            }
        }
    }
}
