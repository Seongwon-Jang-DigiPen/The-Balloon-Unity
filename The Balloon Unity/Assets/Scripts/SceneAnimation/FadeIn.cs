using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : SceneAnimation
{
    float blackColor = 1;
    public UnityEngine.UI.Image image;
    private void Awake()
    {
        image.color = Color.black;
    }

    protected override IEnumerator IAnimation()
    {
        while(blackColor > 0)
        {
            blackColor -= Time.unscaledDeltaTime / aniTime;
            image.color = Color.black * blackColor;
            yield return null;
        }
        end = true;
        isDoSceneAnimation = false;
    }
}
