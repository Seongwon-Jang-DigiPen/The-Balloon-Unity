using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimation : MonoBehaviour
{
    static public bool isDoSceneAnimation = false;
    public bool end = false;
    public float aniTime = 1;

    public void StartAnimation()
    {
        if (isDoSceneAnimation == false && end == false)
        {
            isDoSceneAnimation = true;
            StartCoroutine(IAnimation());
        }
    }

    protected virtual IEnumerator IAnimation()
    {
        end = true;
        isDoSceneAnimation = false;
        yield return null;
    }

}
