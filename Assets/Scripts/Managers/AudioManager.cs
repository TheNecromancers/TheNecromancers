using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource MainTheme;

    private AudioSource[] Sources;

    private void Start()
    {
        Sources = GetComponents<AudioSource>();
    }

    public void PlayClip(AudioClip _clip)
    {
        for (int i = 0; i < Sources.Length; i++)
        {
            if (!Sources[i].isPlaying)
            {
                Sources[i].clip = _clip;
                Sources[i].Play();
                break;
            }
        }
    }

    public void PlayRandomClip(AudioClip[] clips)
    {
        AudioClip _clip = clips[Random.Range(0, clips.Length)];

        for (int i = 0; i < Sources.Length; i++)
        {
            if (!Sources[i].isPlaying)
            {
                Sources[i].clip = _clip;
                Sources[i].Play();
                break;
            }
        }
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
