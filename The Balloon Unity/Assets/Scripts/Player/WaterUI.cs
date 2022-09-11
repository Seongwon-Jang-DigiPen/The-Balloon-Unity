using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterUI : MonoBehaviour, IListener
{
    SpriteRenderer SRenderer;
    PlayerControl PC;
    [SerializeField]
    Sprite water3;
    [SerializeField]
    Sprite water2;
    [SerializeField]
    Sprite water1;
    [SerializeField]
    Sprite water0;

    private void Start()
    {
        PC = GameObject.FindObjectOfType<PlayerControl>();
        SRenderer = GetComponent<SpriteRenderer>();
        SRenderer.enabled = false;
        EventManager.Instance.AddListener(EVENT_TYPE.Player_Sprinkle, this);
        EventManager.Instance.AddListener(EVENT_TYPE.Player_Change_Flat, this);
        EventManager.Instance.AddListener(EVENT_TYPE.Player_Change_Normal, this);
        EventManager.Instance.AddListener(EVENT_TYPE.Player_Change_Water, this);
    }

    public void OnEvent(EVENT_TYPE event_Type, Component sender, object Param = null)
    {
        if(event_Type == EVENT_TYPE.Player_Sprinkle)
        {
            if(PC.SprinkleNum > 3)
            {
                SRenderer.sprite = water3;
            }
            else if(PC.SprinkleNum == 2)
            {
                SRenderer.sprite = water2;
            }
            else if (PC.SprinkleNum == 1)
            {
                SRenderer.sprite = water1;
            }
            else if (PC.SprinkleNum == 0)
            {
                SRenderer.sprite = water0;
            }
        }
        else if(event_Type == EVENT_TYPE.Player_Change_Water)
        {
            SRenderer.sprite = water3;
            SRenderer.enabled = true;
        }
        else if (event_Type == EVENT_TYPE.Player_Change_Normal)
        {
            SRenderer.enabled = false;
        }
        else if (event_Type == EVENT_TYPE.Player_Change_Flat)
        {
            SRenderer.enabled = false;
        }
    }


}
