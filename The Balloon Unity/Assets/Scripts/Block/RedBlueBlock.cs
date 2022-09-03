using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum REDBLUESTATE
{
    RED, BLUE
}

public class REDBLUEEnumerator : IEnumerator
{
    private RedBlueBlock currentObj = null;
    public bool MoveNext()
    {
        currentObj = (currentObj == null) ? RedBlueBlock.firstCreated : currentObj.nextBlock;

        return (currentObj != null);
    }

    public void Reset()
    {
        currentObj = null;
    }

    public object Current
    {
        get { return currentObj; }
    }

}

public class RedBlueBlock : MonoBehaviour, IEnumerable
{
    public static REDBLUESTATE WorldRedBlueState = REDBLUESTATE.RED;

    public static RedBlueBlock lastCreated = null;
    public static RedBlueBlock firstCreated = null;

    public RedBlueBlock nextBlock = null;
    public RedBlueBlock prevBlock = null;

    public REDBLUESTATE blockState = REDBLUESTATE.RED;

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
        return new REDBLUEEnumerator();
    }
}
