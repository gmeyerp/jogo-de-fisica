using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        GameManagement.instance.ReturnToMenu();   
    }

    public void ReloadLevel()
    {
        GameManagement.instance.ReloadLevel();
    }

    public void PauseButton()
    {
        GameManagement.instance.PauseGame();
    }

    public void RetryLevel()
    {
        GameManagement.instance.Retry();
    }

}
