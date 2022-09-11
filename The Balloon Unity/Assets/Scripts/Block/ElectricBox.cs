using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    public Sprite activateBlock;
    public Sprite deactivateBlock;
    public float electricTime;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    private float timeCycle = 0.1f;
    

    bool isElectric = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Player.playerTag))
        {
            if (collision.gameObject.GetComponent<Player>().balloonState.state == BALLOONSTATE.ELECTRIC)
            {
                isElectric = true;
                rigid.drag = 7;
                rigid.mass = 1;
                spriteRenderer.sprite = activateBlock;
            }
            else
            {
                isElectric = false;
                StartCoroutine(EndElectric());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Player.playerTag))
        {
            isElectric = false;
            StartCoroutine(EndElectric());
        }
    }

    IEnumerator EndElectric()
    {
        float timer = 0;
        while(electricTime > timer)
        {
            if (isElectric == true) { break; }

            timer += timeCycle;
           
            yield return YieldInstructionCache.WaitForSeconds(timeCycle);
        }
        
        if(isElectric == false)
        {
            rigid.drag = 10000;
            rigid.mass = 100;
            spriteRenderer.sprite = deactivateBlock;
        }

    }
}
