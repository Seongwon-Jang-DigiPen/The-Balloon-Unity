using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour, IListener
{
    [SerializeField]
    SceneAnimation startAni;

    [SerializeField]
    SceneAnimation dieAni;

    [SerializeField]
    SceneAnimation endAni;

    public string startSound = "";
    public string NextSceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.Player_Start, this);
        EventManager.Instance.AddListener(EVENT_TYPE.Player_Dead, this);
        EventManager.Instance.AddListener(EVENT_TYPE.Player_Clear, this);
        
        EventManager.Instance.PostNotification(EVENT_TYPE.Player_Start, this);
        if(startSound != "")
        {
            SoundManager.instance.PlayBGM(startSound, true);
        }
    }

    public void ChangeNextScene()
    {
        StartCoroutine(IChangeNextScene());
    }

    public void ReloadThisScene()
    {
        StartCoroutine(IReloadThisScene());
    }

    IEnumerator IChangeNextScene()
    {
        endAni.StartAnimation();
        while (!endAni.end)
        {
            yield return null;

        }

        LoadingSceneController.LoadScene(NextSceneName);
    }

    IEnumerator IReloadThisScene()
    {
        dieAni.StartAnimation();
        while (!dieAni.end)
        {
            yield return null;

        }

        LoadingSceneController.LoadScene(SaveLoadManager.LoadData());
    }

    public void OnEvent(EVENT_TYPE event_Type, Component sender, object Param = null)
    {
        switch (event_Type)
        {
            case EVENT_TYPE.Player_Dead:
                ReloadThisScene();
                break;
            case EVENT_TYPE.Player_Clear:
                ChangeNextScene();
                break;
            case EVENT_TYPE.Player_Start:
                if (startAni != null)
                {
                    startAni.StartAnimation();
                }
                break;
        }

    }
}

