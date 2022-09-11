using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : SceneAnimation
{
    float blackColor = 0;
    public UnityEngine.UI.Image image;
    private void Awake()
    {
        image.color = Color.clear;
    }
    protected override IEnumerator IAnimation()
    {

        while (blackColor < 1)
        {
            blackColor += Time.unscaledDeltaTime / aniTime;
            image.color = Color.black * blackColor;
            yield return null;
        }
        Debug.Log("End");
        end = true;
        isDoSceneAnimation = false;
    }
}
