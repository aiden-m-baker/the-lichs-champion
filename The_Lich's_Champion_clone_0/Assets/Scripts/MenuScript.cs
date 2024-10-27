using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainBuildTest");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Pressed Quit");
    }
}
