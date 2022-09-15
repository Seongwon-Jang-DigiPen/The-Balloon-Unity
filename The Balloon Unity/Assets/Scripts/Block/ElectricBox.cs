using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    public Sprite activateBlock;
    public Sprite deactivateBlock;
    public bool electricActivate;
    public float activateMass = 0.1f;
    public float activateDrag = 7;
    public float deactivateMass = 10000;
    public float deactivateDrag = 1;
    bool ct = false;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rigid.drag = deactivateDrag;
        rigid.mass = deactivateMass;
    }

    public void isCatched(bool catched)
    {
        electricActivate = catched;
        spriteRenderer.sprite = (catched) ? activateBlock : deactivateBlock;
        if (catched == false)
        {
          
            rigid.drag = deactivateDrag;
            rigid.mass = deactivateMass;
        }
        else
        {
            rigid.drag = activateDrag;
            rigid.mass = activateMass;
        }

        ct = catched;
    }
}
