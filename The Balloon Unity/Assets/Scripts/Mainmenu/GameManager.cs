using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Button[] menuButtons;
    public GameObject settingPanel;
    public Button BGM;
    public Button SFX;
    public GameObject creditPanel;
    public Text credits;
    public GameObject[] BGMrect;
    public GameObject[] SFXrect;
    public Sprite sprite;

    int BGMvol = 5;
    int SFXvol = 5;

    float speed = 0.7f;
    double time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        SoundManager.instance.BGMvolume(BGMvol);
        SoundManager.instance.SFXvolume(SFXvol);
        SoundManager.instance.PlayBGM("Mainmenu", true);
        settingPanel.SetActive(false);
        creditPanel.SetActive(false);
        Debug.Log("Mainmenu");
        BGMrect = new GameObject[10];
        Vector3 BGMpos = new Vector3(-195, 180, 0);
        for (int i = 0; i < 10; ++i)
        {
            GameObject bgm = new GameObject();
            bgm.transform.SetParent(settingPanel.transform);
            Image image = bgm.AddComponent<Image>();
            image.sprite = sprite;
            bgm.AddComponent<Outline>();
            bgm.GetComponent<RectTransform>().anchoredPosition = BGMpos + new Vector3(50 * i, 0, 0);
            bgm.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            bgm.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 80);
            BGMrect[i] = bgm;
        }
        SFXrect = new GameObject[10];
        Vector3 SFXpos = new Vector3(-195, 0, 0);
        for (int i = 0; i < 10; ++i)
        {
            GameObject sfx = new GameObject();
            sfx.transform.SetParent(settingPanel.transform);
            Image image = sfx.AddComponent<Image>();
            image.sprite = sprite;
            sfx.AddComponent<Outline>();
            sfx.GetComponent<RectTransform>().anchoredPosition = SFXpos + new Vector3(50 * i, 0, 0);
            sfx.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            sfx.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 80);
            SFXrect[i] = sfx;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (creditPanel.activeSelf == true)
        {
            if (credits.GetComponent<RectTransform>().anchoredPosition.y >= 1825)
            {
                credits.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -1845, 0);
            }
            credits.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            credits.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -1845, 0);
        }

        if (settingPanel.activeSelf == true)
        {
            for (int i = 0; i < 10; ++i)
            {
                if (i < BGMvol)
                {
                    Image image = BGMrect[i].GetComponent<Image>();
                    image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Image image = BGMrect[i].GetComponent<Image>();
                    image.color = new Color(1, 1, 1, 0);
                }
            }
            for (int i = 0; i < 10; ++i)
            {
                if (i < SFXvol)
                {
                    Image image = SFXrect[i].GetComponent<Image>();
                    image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Image image = SFXrect[i].GetComponent<Image>();
                    image.color = new Color(1, 1, 1, 0);
                }
            }
            SoundManager.instance.BGMvolume(BGMvol);
            SoundManager.instance.SFXvolume(SFXvol);
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (creditPanel.activeSelf == true)
        {
            if (inputValue.y < 0)
            {
                speed = 3.0f;
            }
            if (context.canceled == true)
            {
                speed = 0.7f;
            }
        }
        if (settingPanel.activeSelf == true)
        {
            if (context.startTime - time >= 0.05)
            {
                if (EventSystem.current.currentSelectedGameObject == BGM.gameObject)
                {
                    if (BGMvol < 10 && inputValue.x > 0)
                    {
                        BGMvol++;
                    }
                    if (BGMvol > 0 && inputValue.x < 0)
                    {
                        BGMvol--;
                    }
                }
                else if (EventSystem.current.currentSelectedGameObject == SFX.gameObject)
                {
                    if (SFXvol < 10 && inputValue.x > 0)
                    {
                        SFXvol++;
                    }
                    if (SFXvol > 0 && inputValue.x < 0)
                    {
                        SFXvol--;
                    }
                }
            }
        }
        time = context.startTime;
    }
}
