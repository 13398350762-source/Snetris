using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    static private AudioManager instance=new AudioManager();
    static public AudioManager Instance => instance;
    private AudioSource audioMusic;
    private AudioSource audioSound;
    private AudioManager() 
    {
        GameObject audioObj = new GameObject("AudioManager");
        Object.DontDestroyOnLoad(audioObj);

        audioMusic = audioObj.AddComponent<AudioSource>();
        audioSound = audioObj.AddComponent<AudioSource>();

        audioMusic.playOnAwake = false;
        audioMusic.loop = true;
        audioMusic.spatialBlend = 0f;

        audioSound.playOnAwake = false;
        audioSound.loop = false;
        audioSound.spatialBlend = 0f;
        ApplySettings(DataManger.Instance.audioData);
    }

    public void ApplySettings(AudioData data)
    {
        float muteVolume = data.allValue;
        float musicVolume = data.musicValue;
        float soundVolume = data.soundValue;
        if (audioMusic != null)
        {
            audioMusic.volume = muteVolume*musicVolume;
            audioMusic.mute = !data.IsOpenAll || !data.IsOpenMusic;
        }
        if (audioSound != null)
        {
            audioSound.volume = muteVolume*soundVolume;
            audioSound.mute = !data.IsOpenAll || !data.IsOpenSound;
        }
    }

    public void PlayMusic(string path)
    {
        if (audioMusic == null)
            return;
        AudioClip music = Resources.Load<AudioClip>(path);
        if (music == null)
            return;
        audioMusic.clip = music;
        audioMusic.loop = true;
        audioMusic.Play();
    }

    public void PlaySound(string path)
    {
        if (audioSound == null)
            return;
        AudioClip sound = Resources.Load<AudioClip>(path);
        if (sound == null)
            return;
        audioSound.PlayOneShot(sound);
    }
}

