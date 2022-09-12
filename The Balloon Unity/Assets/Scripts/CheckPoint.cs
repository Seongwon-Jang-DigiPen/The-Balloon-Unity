using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool canCheckPoint = false;
    private void Start()
    {
        StartCoroutine(Count());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Player.playerTag) && canCheckPoint)
        {
            Debug.Log("StartSave");
            Vector3 pos = GameObject.Find("Player").transform.position;
            SaveData d;
            d.SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            d.xPos = pos.x;
            d.yPos = pos.y;
            
            SaveLoadManager.Save(d);
            Debug.Log("SaveDone");
            StartCoroutine(Count());
        }
    }
    IEnumerator Count()
    {
        canCheckPoint = false;
        yield return YieldInstructionCache.WaitForSeconds(3f);
        canCheckPoint = true;
    }
}