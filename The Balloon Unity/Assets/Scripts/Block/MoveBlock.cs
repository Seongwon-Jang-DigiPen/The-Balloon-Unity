using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVEWAY
{
    ReturnPrevPos, ReturnFirstPos
}

public class MoveBlock : MonoBehaviour
{
    public BoxCollider2D OnCollider;
    public List<Vector3> posList;
    public float moveSpeed;
    public MOVEWAY moveWay;
    public float stopTime = 1.0f;
    protected Rigidbody2D rigid;
    protected Vector2 direction = new Vector2(0, 0);
    protected float length = 0;
    protected int index;
    protected int moveOffset = 1;
    protected bool isStop = false;
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        BlockMove();
    }

    protected virtual void BlockMove()
    {
        if (isStop == false)
        {
            if (posList.Count > index)
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
                        StartCoroutine(IStop());
                    }
                }
            }
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        
        if ( OnCollider != null && OnCollider.IsTouching(collision.collider))
        {
            collision.transform.Translate(rigid.velocity * Time.deltaTime);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        for(int i = 0; i < posList.Count - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(posList[i], posList[i + 1]);
        }
    }

    protected virtual IEnumerator IStop()
    {
        isStop = true;
        yield return YieldInstructionCache.WaitForSeconds(stopTime);
        isStop = false;
    }
}
