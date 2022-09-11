using UnityEngine;


public enum EVENT_TYPE
{
    Player_Dead, Player_Clear, Player_Start, Player_Sprinkle, Player_Change_Normal, Player_Change_Flat, Player_Change_Water, Player_Change_Electric
}

public interface IListener
{
    public void OnEvent(EVENT_TYPE event_Type, Component sender, object Param = null);
}
