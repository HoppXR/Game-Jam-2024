using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadScene4()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadScene5()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadScene6()
    {
        SceneManager.LoadScene(6);
    }

    public void GameExit()
    {
        Application.Quit();
        Debug.Log("Attempted Quit");
    }
}
