using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    private TextMeshProUGUI _soundVolumeText;

    private void Awake()
    {
        _soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
        _soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();


        transform.Find("increaseSoundButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            _soundManager.IncreaseVolume();
            UpdateSoundVolumeText();
            Debug.Log("basti mi");
        });

        transform.Find("decreaseSoundButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            _soundManager.DecreaseVolume();
            UpdateSoundVolumeText();
        });

        transform.Find("increaseMusicButton").GetComponent<Button>().onClick.AddListener(() =>
        {

        });

        transform.Find("decreaseMusicButton").GetComponent<Button>().onClick.AddListener(() =>
        {

        });

        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() =>
        {

        });
    }

    private void Start()
    {
        UpdateSoundVolumeText();
    }

    private void UpdateSoundVolumeText()
    {
        _soundVolumeText.SetText(Mathf.RoundToInt(_soundManager.volume * 10).ToString());
    }
}
