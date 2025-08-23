using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerEx
{
    public void Load(string SceneName, Action afterLoaded)
    {
        void Handler(Scene s, LoadSceneMode m)
        {
            SceneManager.sceneLoaded -= Handler;
            afterLoaded?.Invoke();
        }

        SceneManager.sceneLoaded += Handler;
        SceneManager.LoadScene(SceneName);
    }
}
