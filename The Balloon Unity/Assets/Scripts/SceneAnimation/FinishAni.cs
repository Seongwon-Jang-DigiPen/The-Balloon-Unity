using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinishAni : SceneAnimation
{
    float blackColor = 0;
    public UnityEngine.UI.Image image;
    public float cameraMoveTime = 2;
    public float endTitleTime = 2;

    private void Awake()
    {
    }
    protected override IEnumerator IAnimation()
    {
        float timer = 0;
        while (cameraMoveTime > timer)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        while (blackColor < 1)
        {
            blackColor += Time.unscaledDeltaTime / aniTime;
            image.color = Color.black * blackColor;
            yield return null;
        }
        timer = 0;
        while (endTitleTime > timer)
        {
            if (timer > endTitleTime / 2)
            {
                blackColor -= Time.unscaledDeltaTime;
                image.color = Color.black * blackColor;
            }
            timer += Time.unscaledDeltaTime;
            yield return null;

        }
        end = true;
        isDoSceneAnimation = false;
    }
}
