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


    private void Update()
    {
        if (!isStarted && !BGMSource.isPlaying)
        {
            isStarted = true;
            int rand = Random.Range(0, backgroundEffects.Length);
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
        BGMSource.clip = soundEffects[effect].sound;
        BGMSource.volume = soundEffects[effect].volume;
        BGMSource.pitch = soundEffects[effect].pitch;

        BGMSource.Play();
        isStarted = false;
    }

}
