using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : AgentAudio
{
    [SerializeField] private AudioClip _attackSound;

    public void AttackSound()
    {
        PlayClip(_attackSound);
    }
}
