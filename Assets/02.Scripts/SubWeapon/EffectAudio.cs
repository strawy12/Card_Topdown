using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudio : PlayAudio
{
    [SerializeField] private AudioClip _effectClip;

    public void PlaySound()
    {
        PlayClipRandomPitch(_effectClip);
    }
}
