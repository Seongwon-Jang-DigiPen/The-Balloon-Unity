using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    // Start is called before the first frame update
    void Start()
    {
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
            if (credits.GetComponent<RectTransform>().anchoredPosition.y >= 1840)
            {
                credits.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -1900, 0);
            }
            credits.transform.Translate(Vector3.up * 0.7f * Time.deltaTime);
        }

        if (settingPanel.activeSelf == true)
        {
            if (EventSystem.current.currentSelectedGameObject == BGM.gameObject)
            {
                if (BGMvol < 10 && Input.GetKeyUp(KeyCode.RightArrow))
                {
                    BGMvol++;
                }
                if (BGMvol > 0 && Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    BGMvol--;
                }
            }
            else if (EventSystem.current.currentSelectedGameObject == SFX.gameObject)
            {
                if (SFXvol < 10 && Input.GetKeyUp(KeyCode.RightArrow))
                {
                    SFXvol++;
                }
                if (SFXvol > 0 && Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    SFXvol--;
                }
            }
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
}
