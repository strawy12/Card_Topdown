using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepAudio : PlayAudio
{
    [SerializeField] private AudioClip _stepClip;

    public void PlayStepSound()
    {
        PlayClipRandomPitch(_stepClip);
    }
}
