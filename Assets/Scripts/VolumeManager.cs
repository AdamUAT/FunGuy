using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


// Class manages the audio mixer for the game
public class VolumeManager : MonoBehaviour
{
    #region Singleton
    public static VolumeManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public AudioMixer mixer;

    public float master_volume { get; private set; }
    public float music_volume { get; private set; }
    public float sfx_volume { get; private set; }

    private void Start() 
    {
        // Load volumes in PlayerPrefs
        master_volume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        mixer.SetFloat("MasterVolume", Mathf.Log10(master_volume) * 20);
        
        music_volume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        mixer.SetFloat("MusicVolume", Mathf.Log10(music_volume) * 20);
        
        sfx_volume = PlayerPrefs.GetFloat("SfxVolume", 1.0f);
        mixer.SetFloat("SfxVolume", Mathf.Log10(sfx_volume) * 20);
    }

    public void SetMasterLevel(float sliderValue)
    {
        master_volume = sliderValue;
        SetLevel("MasterVolume", sliderValue); 
    }

    public void SetMusicLevel(float sliderValue)
    {
        music_volume = sliderValue;
        SetLevel("MusicVolume", sliderValue);
    }

    public void SetSfxLevel(float sliderValue)
    {
        sfx_volume = sliderValue;
        SetLevel("SfxVolume", sliderValue);
    }

    private void SetLevel(string name, float value)
    {
        // Calculate sound value, check for 0
        float level = value == 0? -80.0f : Mathf.Log10(value) * 20.0f;
        mixer.SetFloat(name, level);
        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();
    }
}