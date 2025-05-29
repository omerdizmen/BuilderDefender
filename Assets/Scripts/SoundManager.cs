using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit
    }

    public float volume => _volume;

    private AudioSource _audioSource;
    private Dictionary<Sound, AudioClip> _soundAudioClipDictionary;
    private float _volume = 0.5f;
    private void Awake()
    {
        Instance = this;

        _soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        _audioSource = GetComponent<AudioSource>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            _soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
        
    }

    public void PlaySound(Sound sound)
    {
        _audioSource.PlayOneShot(_soundAudioClipDictionary[sound], _volume);
    }

    public void IncreaseVolume()
    {
        _volume += 0.1f;
        _volume = Mathf.Clamp(_volume, 0, 1);
    }

    public void DecreaseVolume()
    {
        _volume -= 0.1f;
        _volume = Mathf.Clamp(_volume, 0, 1);
    }
}
