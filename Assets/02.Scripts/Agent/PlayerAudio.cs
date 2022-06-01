using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : AgentAudio
{
    [SerializeField] private AudioClip _addCardGaugeClip;

    public void PlayAddCardGaugeSound()
    {
        PlayClip(_addCardGaugeClip);
    }
}
