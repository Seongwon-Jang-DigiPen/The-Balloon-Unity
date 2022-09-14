using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    public static SaveData saveData;
    public static bool isDataLoad = false;
    [SerializeField]
    Image progressBar;
    [SerializeField]
    Image Background;
    [SerializeField]
    List<Sprite> spriteList;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
        isDataLoad = false;
    }

    public static void LoadScene(SaveData da)
    {
        saveData = da;
        SceneManager.LoadScene("Loading");
        nextScene = saveData.SceneName;
        isDataLoad = true;
    }

    private void Start()
    {
        Background.sprite = spriteList[Random.Range(0, spriteList.Count)];
        SoundManager.instance.StopAll();
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0;
        while(op.isDone == false)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
 
}
