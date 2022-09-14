using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Ch1End : SceneAnimation
{
    float blackColor = 0;
    public UnityEngine.UI.Image image;
    public GameObject player;
    public GameObject afterCameraBinding;
    public CinemachineVirtualCamera vCamera;
    public float cameraMoveTime = 2;
    public float endTitleTime = 2;

    private void Awake()
    {
    }
    protected override IEnumerator IAnimation()
    {
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        vCamera.Follow = null;
        float timer = 0;
        while (cameraMoveTime > timer)
        {
            vCamera.transform.position = Vector3.Lerp(vCamera.transform.position, afterCameraBinding.transform.position, Time.unscaledDeltaTime / 5);
            timer += Time.unscaledDeltaTime;
            yield return null;

        }

        while (blackColor < 1)
        {
            vCamera.transform.position = Vector3.Lerp(vCamera.transform.position, afterCameraBinding.transform.position, Time.unscaledDeltaTime / 5);
            blackColor += Time.unscaledDeltaTime / aniTime;
            image.color = Color.white * blackColor;
            yield return null;
        }
        timer = 0;
        while (endTitleTime > timer)
        {
            if (timer > endTitleTime / 2)
            {
                blackColor -= Time.unscaledDeltaTime;
                image.color = Color.white * blackColor;
            }
            vCamera.transform.position = Vector3.Lerp(vCamera.transform.position, afterCameraBinding.transform.position, Time.unscaledDeltaTime / 5);
            timer += Time.unscaledDeltaTime;
            yield return null;

        }
        end = true;
        isDoSceneAnimation = false;
    }
}
