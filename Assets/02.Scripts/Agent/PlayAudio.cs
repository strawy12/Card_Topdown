using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    [SerializeField] protected float _pitchRandness = 0.2f;
    protected AudioSource _audioSource = null;
    protected float _basePitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _basePitch = _audioSource.pitch;
    }

    protected void PlayClipRandomPitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-_pitchRandness, _pitchRandness);
        _audioSource.pitch = _basePitch + randomPitch;
        PlayClip(clip);
    }

    protected void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
