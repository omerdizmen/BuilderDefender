using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager 
{
    public enum SceneName
    {
        GameScene,
        MainMenuScene
    }
    public static void Load(SceneName scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
