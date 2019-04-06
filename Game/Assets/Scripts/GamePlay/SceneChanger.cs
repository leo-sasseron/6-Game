using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private bool pause;
    private float originalfixedTime;

    // Use this for initialization
    void Start()
    {
        pause = false;
        originalfixedTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            if (pause)
            {
                PauseGame();
                SceneManager.LoadScene(0);
            }

            else
            {
                UnPauseGame();
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }

    private void PauseGame()
    {        
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = originalfixedTime;
    }
}
