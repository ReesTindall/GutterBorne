using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    //AudioMixer attributes
    public const string MUSIC = "VolumeMusic";
    public const string SFX = "VolumeSFX";

    void Awake() {
        musicSlider.onValueChanged.AddListener(SetVolumeMusic);
        sfxSlider.onValueChanged.AddListener(SetVolumeSFX);
    }

    void Start() {
        /*
            UNCOMMENT BELOW WHEN PLAYER CREATED!
        */
        
        // musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        // sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    //save set volume values
    void OnDisable() {

        /*
            UNCOMMENT BELOW WHEN PLAYER CREATED!
        */

        // PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        // PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    //change volume of music
    void SetVolumeMusic(float value) {
        mixer.SetFloat(MUSIC, Mathf.Log10(value) * 20);
    }

    //change volume of sound effects
    void SetVolumeSFX(float value) {
        mixer.SetFloat(SFX, Mathf.Log10(value) * 20);
    }
}
