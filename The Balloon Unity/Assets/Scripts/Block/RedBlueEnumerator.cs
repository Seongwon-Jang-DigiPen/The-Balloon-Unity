using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlueEnumerator : IEnumerator
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