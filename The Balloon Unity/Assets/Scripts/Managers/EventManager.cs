using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance
    {
        get { return instance; }
        set { }
    }

    private static EventManager instance = null;

    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
            SceneManager.sceneLoaded += SceneLoaded; // 초기화를 할 때 SceneLoaded에 함수를 빼는 처리를 어케 해야할까 고민된다
        }

    }


    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RemoveRedundancies();
    }

    public void AddListener(EVENT_TYPE eventType, IListener listener)
    {
        List<IListener> listenList = null;

        if (Listeners.TryGetValue(eventType, out listenList) == true)
        {
            listenList.Add(listener);
            return;
        }
        listenList = new List<IListener>();
        listenList.Add(listener);
        Listeners.Add(eventType, listenList);

    }
    public void PostNotification(EVENT_TYPE eventType, Component Sender, object Param = null)
    {
        List<IListener> listenList = null;

        if (Listeners.TryGetValue(eventType, out listenList) == false)
        {
            return;
        }

        for (int i = 0; i < listenList.Count; ++i)
        {
            if (listenList[i].Equals(null) == false)
            {
                listenList[i].OnEvent(eventType, Sender, Param);
            }
            else
            {
                Debug.Log("In EventManager, listenList have null");
            }
        }
    }

    public void RemoveEvent(EVENT_TYPE eventType)
    {
        Listeners.Remove(eventType);
    }
    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<IListener>> tempListeners = new Dictionary<EVENT_TYPE, List<IListener>>();
        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> item in Listeners)
        {
            for (int i = item.Value.Count - 1; i >= 0; i--)
            {
                if (item.Value[i].Equals(null))
                {
                    item.Value.RemoveAt(i);
                }
            }
            if (item.Value.Count > 0)
            {
                tempListeners.Add(item.Key, item.Value);
            }
        }
        Listeners = tempListeners;
    }



}
