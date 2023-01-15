using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public static AudioController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static void PlayAudioClip(AudioClip audioClip)
    {
        instance._audioSource.clip = audioClip;
        instance._audioSource.Play();
    }
}
