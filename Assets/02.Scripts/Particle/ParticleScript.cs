using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : PoolableMono
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        StartCoroutine(ParticleCoroutine());
    }
    IEnumerator ParticleCoroutine()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(1f);
        PoolManager.Inst.Push(this);
    }
    public override void Reset()
    {

    }
}
