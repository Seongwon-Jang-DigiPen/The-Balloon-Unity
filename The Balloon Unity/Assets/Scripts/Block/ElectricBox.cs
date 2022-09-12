using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    public Sprite activateBlock;
    public Sprite deactivateBlock;
    public bool electricActivate;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    

    bool isElectric = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void isCatched(bool catched)
    {
        electricActivate = catched;
        spriteRenderer.sprite = (catched) ? activateBlock : deactivateBlock;
        if (catched == false)
        {
            rigid.drag = 10000;
            rigid.mass = 100;
        }
        else
        {
            rigid.drag = 7;
            rigid.mass = 0.1f;
        }
    }
}
