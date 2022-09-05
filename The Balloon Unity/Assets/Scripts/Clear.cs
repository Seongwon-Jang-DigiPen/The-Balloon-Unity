using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Clear : MonoBehaviour
{
    public string nextScene;
    public bool isClear = false;
    public float moveNextSceneTime;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.playerTag))
        {
            isClear = true;
            if (moveNextSceneTime > 0)
            {
                virtualCamera.Follow = null;
                StartCoroutine(IClearCheck());
            }
        }
    }
    IEnumerator IClearCheck()
    {
        yield return new WaitForSeconds(moveNextSceneTime);
        ChangeScene();
    }
}
