using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseCanvas;
    

    private bool pause;
    private float originalfixedTime;

    public void Start()
    {
        pause = false;
        pauseCanvas.SetActive(false);
        originalfixedTime = Time.fixedDeltaTime;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            if (pause)
                PauseGame();

            else
                UnPauseGame();
        }
    }

    private void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;        
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = originalfixedTime;
        pauseCanvas.SetActive(false);
    }

    public void GoToMenu()
    {
        UnPauseGame();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
