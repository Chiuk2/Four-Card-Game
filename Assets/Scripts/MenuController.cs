using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene("GameSelect");
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
