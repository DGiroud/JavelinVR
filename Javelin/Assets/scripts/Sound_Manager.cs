using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    [System.Serializable]
    public struct SoundEffect
    {
        public AudioClip sound;

        [Range(0,1)]
        public float volume;

        [Range(0, 1)]
        public float pitch;
    }

    public AudioSource BGMSource;
    public AudioSource SFXSource;

    [Space]
    public SoundEffect[] soundEffects;

    public SoundEffect[] backgroundEffects;

    private bool isStarted = false;
    private int lastBGM;

    private void Awake()
    {
        isStarted = true;
        PlayBGM(0);
    }


    private void Update()
    {
        if (!isStarted && !BGMSource.isPlaying)
        {
            isStarted = true;

            int rand = Random.Range(0, backgroundEffects.Length);
            while (rand == lastBGM)            
                rand = Random.Range(0, backgroundEffects.Length);            

            lastBGM = rand;
            PlayBGM(rand);
        }
    }

    public void PlaySound(int effect)
    {
        SFXSource.clip = soundEffects[effect].sound;
        SFXSource.volume = soundEffects[effect].volume;
        SFXSource.pitch = soundEffects[effect].pitch;

        SFXSource.Play();
    }


    void PlayBGM(int effect)
    {
        BGMSource.clip = backgroundEffects[effect].sound;
        BGMSource.volume = backgroundEffects[effect].volume;
        BGMSource.pitch = backgroundEffects[effect].pitch;

        BGMSource.Play();
        isStarted = false;
    }

}
