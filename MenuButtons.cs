using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Entrance");
    }
    public void Load()
    {
        //SceneManager.LoadScene("LoadGame");
    }
    public void Configuration()
    {
        //ScenneManager.LoadScene("Configuration");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
