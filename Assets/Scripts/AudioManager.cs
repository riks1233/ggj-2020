using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] bgSound;
    public AudioSource bgSoundSource;
    public AudioSource pairingSoundSource;
    private int bgSoundIndex;
    //private void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //}

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        bgSoundIndex = 0;
        // set sound
        UpdateBackgroundSound();
    }

    public void UpdateBackgroundSound()
    {
        if (bgSoundIndex < bgSound.Length)
        {
            bgSoundSource.Stop();
            bgSoundSource.time = 0;
            bgSoundSource.clip = bgSound[bgSoundIndex];
            bgSoundSource.Play();

        }
    }

    public void ResetAudio() {
        Init();
    }

    public void NextBackgroundSound()
    {
        bgSoundIndex++;
        // set sound
        UpdateBackgroundSound();
    }

    public void LaunchPairSound()
    {
        pairingSoundSource.Stop();
        pairingSoundSource.time = 0;
        pairingSoundSource.Play();
    }
}
