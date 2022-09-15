using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool canCheckPoint = true;
    public Animator SaveAni;
    public SpriteRenderer render;
    private void Start()
    {
        render.sprite = null;
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
        SaveAni.SetTrigger("Start");
        canCheckPoint = false;
        yield return YieldInstructionCache.WaitForSeconds(3f);
        render.sprite = null;
        canCheckPoint = true;
    }
}
