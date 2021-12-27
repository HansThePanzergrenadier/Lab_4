using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void StartButtonPressed()
    {
        SceneManager.LoadScene("GameWorld");
    }

    public void QuitButtonPressed()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            SceneManager.UnloadSceneAsync(0);
        }
        Application.Quit();
    }
}

