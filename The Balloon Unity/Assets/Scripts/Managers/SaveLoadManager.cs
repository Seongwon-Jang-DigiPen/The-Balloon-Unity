using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SaveData
{
    public string SceneName;
    public float xPos;
    public float yPos;
}

public class SaveLoadManager : MonoBehaviour
{
    public static void Save(SaveData data)
    {
        PlayerPrefs.SetString("SceneName", data.SceneName);
        PlayerPrefs.SetFloat("X", data.xPos);
        PlayerPrefs.SetFloat("Y", data.yPos);
    }
    public static SaveData LoadData()
    {
        SaveData d;
        d.SceneName = PlayerPrefs.GetString("SceneName");
        d.xPos = PlayerPrefs.GetFloat("X");
        d.yPos = PlayerPrefs.GetFloat("Y");
        return d;
    }


}

