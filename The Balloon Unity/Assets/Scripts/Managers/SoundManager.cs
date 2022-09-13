using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Sound[] bgms;
    public Sound[] sfxs;

    public AudioSource asBGM;
    public AudioSource[] asSFX;

    public string[] nowPlay;

    float bgmvolume = 1;
    float sfxvolume = 1;

 
    void Start()
    { 
        nowPlay = new string[asSFX.Length];

    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < sfxs.Length; i++)
        {
            if (name == sfxs[i].name)
            {
                for (int j = 0; j < asSFX.Length; j++)
                {
                    if (!asSFX[j].isPlaying)
                    {
                        asSFX[j].clip = sfxs[i].clip;
                        asSFX[j].Play();
                        nowPlay[j] = sfxs[i].name;
                        return;
                    }
                }
                return;
            }
        }
        Debug.Log(name + "does not exist");
    }

    public void PlayBGM(string name, bool loop)
    {
        for (int i = 0; i < bgms.Length; i++)
        {
            if (name == bgms[i].name)
            {
                asBGM.clip = bgms[i].clip;
                asBGM.Play();
                asBGM.loop = loop;
                return;
            }
        }
        Debug.Log(name + "does not exist");
    }

    public void StopAll()
    {
        for (int i = 0; i < asSFX.Length; ++i)
        {
            asSFX[i].Stop();
        }
        asBGM.Stop();
    }

    public void StopSFX(string name)
    {
        for (int i = 0; i < asSFX.Length; ++i)
        {
            if(nowPlay[i] == name)
            {
                asSFX[i].Stop();
                break;
            }
        }
    }

    public void BGMvolume(int volume)
    {
        bgmvolume = (float)volume / 10;
        asBGM.volume = bgmvolume;
    }

    public void SFXvolume(int volume)
    {
        sfxvolume = (float)volume / 10;
        for (int i = 0; i < asSFX.Length; ++i)
        {
            asSFX[i].volume = sfxvolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
