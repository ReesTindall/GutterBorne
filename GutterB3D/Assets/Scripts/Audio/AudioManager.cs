using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "VolumeMusic";
    public const string SFX_KEY = "VolumeSFX";

    void Awake() {
        //if player set volume, load across scenes

        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    //the volumes are saved in VolumeSettings.cs (<-- in every scene; NOT this script)
    void LoadVolume() {

        /*
            UNCOMMENT BELOW WHEN PLAYER CREATED!
        */

        // float volumeMusic = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        // float volumeSFX = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        // mixer.SetFloat(VolumeSettings.MUSIC, Mathf.Log10(volumeMusic) * 20);
        // mixer.SetFloat(VolumeSettings.SFX, Mathf.Log10(volumeSFX) * 20);
    }
}
