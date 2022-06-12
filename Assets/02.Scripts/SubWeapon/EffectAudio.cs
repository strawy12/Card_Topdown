using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudio : PlayAudio
{
    [SerializeField] private AudioClip _effectClip;
    [SerializeField] private bool _useRandomPitch = true;
    public void PlaySound()
    {
        if (_useRandomPitch)
            PlayClipRandomPitch(_effectClip);

        else
            PlayClip(_effectClip);
    }
}
