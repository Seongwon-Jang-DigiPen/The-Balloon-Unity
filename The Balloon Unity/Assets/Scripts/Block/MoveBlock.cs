using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVEWAY
{
    ReturnPrevPos, ReturnFirstPos
}

public class MoveBlock : MonoBehaviour
{
    public List<Transform> posList;
    public float MoveSpeed;
    public MOVEWAY moveWay;

    protected Rigidbody2D rigid;
    private Vector2 direction = new Vector2(0, 0);
    private float length = 0;
    private int index;
    private int moveOffset = 1;
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        BlockMove();
    }

    protected void BlockMove()
    {
        if (posList.Count > index)
        {
            if (posList[index] != null)
            {
                Vector2 temp = (posList[index].position - transform.position);
                length = temp.magnitude;
                direction = temp.normalized;
                rigid.velocity = direction * MoveSpeed;
            }
            if (length <= (direction * MoveSpeed * Time.fixedDeltaTime).magnitude + 0.01f)
            {
                transform.position = posList[index].position;
                rigid.velocity = Vector2.zero;
                index += moveOffset;
                if (index == -1 || index == posList.Count)
                {
                    if (moveWay == MOVEWAY.ReturnPrevPos)
                    {

                        moveOffset = -moveOffset;
                        index += moveOffset;
                    }
                    else
                    {
                        index = 0;
                    }
                }
            }
        }
    }

}
