using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : PoolableMono
{
    private ParticleSystem _particleSystem;
    private EffectAudio _effectAudio;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _effectAudio = transform.Find("EffectSound").GetComponent<EffectAudio>();
    }
    private void Start()
    {
        StartCoroutine(ParticleCoroutine());
    }
    IEnumerator ParticleCoroutine()
    {
        _particleSystem.Play();
        _effectAudio.PlaySound();
        yield return new WaitForSeconds(1f);
        PoolManager.Inst.Push(this);
    }
    public override void Reset()
    {

    }
}
