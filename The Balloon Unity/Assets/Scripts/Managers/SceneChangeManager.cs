using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField]
    SceneAnimation startAni;

    [SerializeField]
    SceneAnimation endAni;

    public string NextSceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        if (startAni != null)
        {
            startAni.StartAnimation();
        }
    }

    public void ChangeNextScene()
    {
        StartCoroutine(IChangeNextScene());
    }
    IEnumerator IChangeNextScene()
    {
        endAni.StartAnimation();
        while (!endAni.end)
        {
            yield return null;

        }
        Debug.Log(endAni.end);
        LoadingSceneController.LoadScene(NextSceneName);
    }
}

