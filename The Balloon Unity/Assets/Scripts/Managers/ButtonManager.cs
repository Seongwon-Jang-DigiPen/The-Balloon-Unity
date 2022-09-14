using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Button[] menuButtons;
    public GameObject settingPanel;
    public Button BGM;
    public GameObject creditPanel;
    public Button creditBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SoundManager.instance.PlaySound("Button");
        EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
    }

    public void Load()
    {
        SoundManager.instance.PlaySound("Button");
        SaveData data = SaveLoadManager.LoadData();
        if (data.isSave == true)
        {
            LoadingSceneController.LoadScene(data);
        }
    }

    public void Setting()
    {
        SoundManager.instance.PlaySound("Button");
        settingPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(BGM.gameObject);
        for (int i = 0; i < menuButtons.Length; ++i)
        {
            menuButtons[i].gameObject.SetActive(false);
        }
        Debug.Log("Setting");
    }

    public void Credit()
    {
        SoundManager.instance.PlaySound("Button");
        SoundManager.instance.PlayBGM("Credit", true);
        creditPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditBack.gameObject);
        for (int i = 0; i < menuButtons.Length; ++i)
        {
            menuButtons[i].gameObject.SetActive(false);
        }
        Debug.Log("Credit");
    }

    public void Quit()
    {
        SoundManager.instance.PlaySound("Button");
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Back()
    {
        SoundManager.instance.PlaySound("Button");
        settingPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButtons[0].gameObject);
        for (int i = 0; i < menuButtons.Length; ++i)
        {
            menuButtons[i].gameObject.SetActive(true);
        }
        Debug.Log("Back");
    }

    public void CreditBack()
    {
        SoundManager.instance.PlaySound("Button");
        SoundManager.instance.PlayBGM("Mainmenu", true);
        creditPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButtons[0].gameObject);
        for (int i = 0; i < menuButtons.Length; ++i)
        {
            menuButtons[i].gameObject.SetActive(true);
        }
        Debug.Log("End Credit");
    }
}
