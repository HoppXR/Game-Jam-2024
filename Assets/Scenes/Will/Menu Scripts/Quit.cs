using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quit : MonoBehaviour
{
    
    public void GameExit()
    {
        Application.Quit();
        Debug.Log("Attempted Quit");
    }

    
}
