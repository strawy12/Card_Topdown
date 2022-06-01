using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonClickClip;
    [SerializeField] private AudioClip _addCardClip;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }

    public void PlayButtonClickSound()
    {
        PlaySound(_buttonClickClip);
    }

    public void PlayAddCardSound()
    {
        PlaySound(_addCardClip);
    }


    public void PlaySound(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
