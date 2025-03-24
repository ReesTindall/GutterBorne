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

    //audio mixer attributes
    const string MUSIC = "VolumeMusic";
    const string SFX = "VolumeSFX";

    void Awake() {
        musicSlider.onValueChanged.AddListener(SetVolumeMusic);
        sfxSlider.onValueChanged.AddListener(SetVolumeSFX);
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
