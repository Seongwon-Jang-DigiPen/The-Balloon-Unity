using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene03 : MonoBehaviour
{
    public GameObject Endingcut;
    public GameObject EndingcutLoop;

    // Start is called before the first frame update
    void Start()
    {
        Endingcut.GetComponent<Animator>().Play("Endingcut", -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
            Debug.Log("Cutscene end");
        }
        if (Endingcut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Endingcut.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!EndingcutLoop.GetComponent<SpriteRenderer>().isVisible)
            {
                Endingcut.GetComponent<SpriteRenderer>().enabled = false;
                Endingcut.GetComponent<Animator>().enabled = false;
                EndingcutLoop.GetComponent<SpriteRenderer>().enabled = true;
                EndingcutLoop.GetComponent<Animator>().enabled = true;
                EndingcutLoop.GetComponent<Animator>().Play("EndingcutLoop", -1, 0);
            }
        }
        if (EndingcutLoop.GetComponent<SpriteRenderer>().isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
                Debug.Log("Cutscene end");
            }
        }
    }
}
