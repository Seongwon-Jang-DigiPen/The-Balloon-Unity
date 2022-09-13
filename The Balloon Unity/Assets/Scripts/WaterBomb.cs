using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    public float destroyTime = 3.0f;
    private void Start()
    {
        StartCoroutine(IDestroyTime());
    }

    IEnumerator IDestroyTime()
    {
        yield return YieldInstructionCache.WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }    
}
