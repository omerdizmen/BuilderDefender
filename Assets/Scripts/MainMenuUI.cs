using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private Button _playButton;
    private Button _quitButton;

    private void Awake()
    {
        _playButton = transform.Find("playButton").GetComponent<Button>();
        _quitButton = transform.Find("quitButton").GetComponent<Button>();

        _playButton.onClick.AddListener(PlayButton);
        _playButton.onClick.AddListener(QuitButton);
    }

    private void PlayButton()
    {
        GameSceneManager.Load(GameSceneManager.SceneName.GameScene);
    }

    private void QuitButton()
    {
        Application.Quit();
    }
}
