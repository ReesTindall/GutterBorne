using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Mixer + Sliders")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MUSIC_KEY = "VolumeMusic";
    private const string SFX_KEY = "VolumeSFX";
    private const string MUSIC_PARAM = "MusicVolume";
    private const string SFX_PARAM = "SFXVolume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolume();
    }

    void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void LoadVolume()
    {
        float musicVol = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        mixer.SetFloat(MUSIC_PARAM, Mathf.Log10(Mathf.Clamp(musicVol, 0.0001f, 1f)) * 20);
        mixer.SetFloat(SFX_PARAM, Mathf.Log10(Mathf.Clamp(sfxVol, 0.0001f, 1f)) * 20);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat(MUSIC_PARAM, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat(MUSIC_KEY, value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(SFX_PARAM, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat(SFX_KEY, value);
    }
}