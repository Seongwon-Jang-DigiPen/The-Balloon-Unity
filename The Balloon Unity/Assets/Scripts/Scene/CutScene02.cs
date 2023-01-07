using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutScene02 : MonoBehaviour
{
    public GameObject Ch1endcut;
    public GameObject Ch1endcutLoop;
    public GameObject Ch2startcut;
    public GameObject Ch2startcutLoop;

    // Start is called before the first frame update
    void Start()
    {
        Ch1endcut.GetComponent<Animator>().Play("Ch1endcut", -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
            Debug.Log("Cutscene end");
        }
        if (Ch1endcut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Ch1endcut.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!Ch1endcutLoop.GetComponent<SpriteRenderer>().isVisible)
            {
                Ch1endcut.GetComponent<SpriteRenderer>().enabled = false;
                Ch1endcut.GetComponent<Animator>().enabled = false;
                Ch1endcutLoop.GetComponent<SpriteRenderer>().enabled = true;
                Ch1endcutLoop.GetComponent<Animator>().enabled = true;
                Ch1endcutLoop.GetComponent<Animator>().Play("Ch1endcutLoop", -1, 0);
            }
        }
        
        if (Ch2startcut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Ch2startcut.GetComponent<SpriteRenderer>().isVisible)
        {
            if (!Ch2startcutLoop.GetComponent<SpriteRenderer>().isVisible)
            {
                Ch2startcut.GetComponent<SpriteRenderer>().enabled = false;
                Ch2startcut.GetComponent<Animator>().enabled = false;
                Ch2startcutLoop.GetComponent<SpriteRenderer>().enabled = true;
                Ch2startcutLoop.GetComponent<Animator>().enabled = true;
                Ch2startcutLoop.GetComponent<Animator>().Play("Ch2startcutLoop", -1, 0);
            }
        }
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (Ch1endcutLoop.GetComponent<SpriteRenderer>().isVisible)
        {
            Ch1endcutLoop.GetComponent<SpriteRenderer>().enabled = false;
            Ch1endcutLoop.GetComponent<Animator>().enabled = false;
            Ch2startcut.GetComponent<SpriteRenderer>().enabled = true;
            Ch2startcut.GetComponent<Animator>().enabled = true;
            Ch2startcut.GetComponent<Animator>().Play("Ch2startcut", -1, 0);
        }
        if (Ch2startcutLoop.GetComponent<SpriteRenderer>().isVisible)
        {
            EventManager.Instance.PostNotification(EVENT_TYPE.Player_Clear, this);
            if (context.started == true)
            {
                Debug.Log("Cutscene end");
            }
        }
    }
}
