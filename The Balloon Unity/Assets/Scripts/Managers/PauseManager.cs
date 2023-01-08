using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    static public bool paused = false;
    public Button resume;
    public GameObject pauseCanvas;
    public AudioSource audiosource;
    public AudioSource audiosource2;
    //public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
        audiosource.ignoreListenerPause = true;
        audiosource2.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resume.gameObject);
        AudioListener.pause = true;
        PlayerControl.canMove = false;
        Debug.Log("paused");
    }

    public void UnpauseGame()
    {
        paused = false;
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        AudioListener.pause = false;
        PlayerControl.canMove = true;
        Debug.Log("unpaused");
    }

    public void Resume()
    {
        SoundManager.instance.PlaySound("Button");
        UnpauseGame();
        Debug.Log("resume");
    }

    public void LastSave()
    {
        SoundManager.instance.PlaySound("Button");
        UnpauseGame();
        SaveData data = SaveLoadManager.LoadData();
        if (data.isSave == true)
        {
            LoadingSceneController.LoadScene(data);
        }
        Debug.Log("lastsave");
    }

    public void Mainmenu()
    {
        SoundManager.instance.PlaySound("Button");
        UnpauseGame();
        LoadingSceneController.LoadScene("TitleScreen");
        Debug.Log("mainmenu");
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (paused == false)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }
}
