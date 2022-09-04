using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public void Setting()
    {
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
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Back()
    {
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
