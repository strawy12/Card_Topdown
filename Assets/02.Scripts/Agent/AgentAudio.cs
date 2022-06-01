using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudio : PlayAudio
{
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _deathClip;

    public void PlayHitSound()
    {
        PlayClipRandomPitch(_hitClip);
    }

    public void PlayDeathSound() 
    {
        PlayClip(_deathClip);
    }
}
