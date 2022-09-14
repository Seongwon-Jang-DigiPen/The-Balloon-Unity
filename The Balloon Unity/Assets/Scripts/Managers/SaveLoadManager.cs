using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SaveData
{
    public bool isSave;
    public string SceneName;
    public float xPos;
    public float yPos;
}

public class SaveLoadManager : MonoBehaviour
{
    public static void Save(SaveData data)
    {
        PlayerPrefs.SetInt("isSave", 1);
        PlayerPrefs.SetString("SceneName", data.SceneName);
        PlayerPrefs.SetFloat("X", data.xPos);
        PlayerPrefs.SetFloat("Y", data.yPos);
    }
    public static SaveData LoadData()
    {
        SaveData d;
        d.isSave = PlayerPrefs.HasKey("isSave");
        d.SceneName = PlayerPrefs.GetString("SceneName");
        d.xPos = PlayerPrefs.GetFloat("X");
        d.yPos = PlayerPrefs.GetFloat("Y");
        return d;
    }


}

