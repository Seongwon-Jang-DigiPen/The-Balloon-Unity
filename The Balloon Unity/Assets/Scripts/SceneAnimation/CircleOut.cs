using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOut : SceneAnimation
{
    [SerializeField]
    private GameObject fog;
    [SerializeField]
    private GameObject circle;

    [SerializeField]
    private GameObject player;
    private void Start()
    {
        circle.SetActive(false);
        fog.SetActive(false);
    }

    protected override IEnumerator IAnimation()
    {
        circle.SetActive(true);
        fog.SetActive(true);
        
        Vector3 defaultScale = circle.transform.localScale;
        float timer = 0;
        while (circle.transform.localScale.x > 0)
        {
            transform.position = player.transform.position;
            circle.transform.localScale = Vector3.Lerp(defaultScale, Vector3.zero, timer / aniTime);

            timer += Time.deltaTime;
             yield return null;
        }
        end = true;
        isDoSceneAnimation = false;
        yield return null;
    }
}
