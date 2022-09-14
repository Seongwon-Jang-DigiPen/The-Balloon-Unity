using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutScene01 : MonoBehaviour
{
    public GameObject Castlecut;
    public GameObject CastlecutLoop;
    public GameObject Kingcut;
    public GameObject KingcutLoop;
    public GameObject Kingtalk1;
    public GameObject Kingtalk2;
    public GameObject Kingtalk3;
    public GameObject KingtalkLoop;
    public GameObject Exilecut;
    public GameObject ExilecutLoop;

    // Start is called before the first frame update
    void Start()
    {
        Castlecut.GetComponent<Animator>().Play("Castlecut", -1 , 0);
        SoundManager.instance.PlayBGM("CutScene", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
            Debug.Log("Cutscene end");
        }
        if (Castlecut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Castlecut.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!CastlecutLoop.GetComponent<SpriteRenderer>().isVisible)
            {
                Castlecut.GetComponent<SpriteRenderer>().enabled = false;
                Castlecut.GetComponent<Animator>().enabled = false;
                CastlecutLoop.GetComponent<SpriteRenderer>().enabled = true;
                CastlecutLoop.GetComponent<Animator>().enabled = true;
                CastlecutLoop.GetComponent<Animator>().Play("CastlecutLoop", -1, 0);
            }
        }
        if (CastlecutLoop.GetComponent<SpriteRenderer>().isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            { 
                CastlecutLoop.GetComponent<SpriteRenderer>().enabled = false;
                CastlecutLoop.GetComponent<Animator>().enabled = false;
                Kingcut.GetComponent<SpriteRenderer>().enabled = true;
                Kingcut.GetComponent<Animator>().enabled = true;
                Kingcut.GetComponent<Animator>().Play("Kingcut", -1, 0);
            }
        }
        if (Kingcut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Kingcut.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!KingcutLoop.GetComponent<SpriteRenderer>().isVisible)
            {
                Kingcut.GetComponent<SpriteRenderer>().enabled = false;
                Kingcut.GetComponent<Animator>().enabled = false;
                KingcutLoop.GetComponent<SpriteRenderer>().enabled = true;
                KingcutLoop.GetComponent<Animator>().enabled = true;
                KingcutLoop.GetComponent<Animator>().Play("KingcutLoop", -1, 0);
            }
        }
        if (KingcutLoop.GetComponent<SpriteRenderer>().isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                KingcutLoop.GetComponent<SpriteRenderer>().enabled = false;
                KingcutLoop.GetComponent<Animator>().enabled = false;
                Kingtalk1.GetComponent<SpriteRenderer>().enabled = true;
                Kingtalk1.GetComponent<Animator>().enabled = true;
                Kingtalk1.GetComponent<Animator>().Play("Kingtalk1", -1, 0);
            }
        }
        if (Kingtalk1.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Kingtalk1.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!Kingtalk2.GetComponent<SpriteRenderer>().isVisible)
            {
                Kingtalk1.GetComponent<SpriteRenderer>().enabled = false;
                Kingtalk1.GetComponent<Animator>().enabled = false;
                Kingtalk2.GetComponent<SpriteRenderer>().enabled = true;
                Kingtalk2.GetComponent<Animator>().enabled = true;
                Kingtalk2.GetComponent<Animator>().Play("Kingtalk2", -1, 0);
            }
        }
        if (Kingtalk2.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Kingtalk2.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!Kingtalk3.GetComponent<SpriteRenderer>().isVisible)
            {
                Kingtalk2.GetComponent<SpriteRenderer>().enabled = false;
                Kingtalk2.GetComponent<Animator>().enabled = false;
                Kingtalk3.GetComponent<SpriteRenderer>().enabled = true;
                Kingtalk3.GetComponent<Animator>().enabled = true;
                Kingtalk3.GetComponent<Animator>().Play("Kingtalk3", -1, 0);
            }
        }
        if (Kingtalk3.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Kingtalk3.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!KingtalkLoop.GetComponent<SpriteRenderer>().isVisible)
            {
                Kingtalk3.GetComponent<SpriteRenderer>().enabled = false;
                Kingtalk3.GetComponent<Animator>().enabled = false;
                KingtalkLoop.GetComponent<SpriteRenderer>().enabled = true;
                KingtalkLoop.GetComponent<Animator>().enabled = true;
                KingtalkLoop.GetComponent<Animator>().Play("KingtalkLoop", -1, 0);
            }
        }
        if (KingtalkLoop.GetComponent<SpriteRenderer>().isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                KingtalkLoop.GetComponent<SpriteRenderer>().enabled = false;
                KingtalkLoop.GetComponent<Animator>().enabled = false;
                Exilecut.GetComponent<SpriteRenderer>().enabled = true;
                Exilecut.GetComponent<Animator>().enabled = true;
                Exilecut.GetComponent<Animator>().Play("Exilecut", -1, 0);
            }
        }
        if (Exilecut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Exilecut.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!ExilecutLoop.GetComponent<SpriteRenderer>().isVisible)
            {
                Exilecut.GetComponent<SpriteRenderer>().enabled = false;
                Exilecut.GetComponent<Animator>().enabled = false;
                ExilecutLoop.GetComponent<SpriteRenderer>().enabled = true;
                ExilecutLoop.GetComponent<Animator>().enabled = true;
                ExilecutLoop.GetComponent<Animator>().Play("ExilecutLoop", -1, 0);
            }
        }
        if (ExilecutLoop.GetComponent<SpriteRenderer>().isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
                Debug.Log("Cutscene end");
            }
        }
    }
}
