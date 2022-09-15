using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool canCheckPoint = true;
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Player.playerTag) && canCheckPoint)
        {
            Debug.Log("StartSave");
            ParticleManager.instance.PlayParticle(this.gameObject, ParticleManager.ParticleType.Jump);
            SoundManager.instance.PlaySound("GetItem");
            Vector3 pos = GameObject.Find("Player").transform.position;
            SaveData d;
            d.isSave = true;
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
