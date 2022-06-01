using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class AttackCalculate : MonoBehaviour
{
    [SerializeField] private AudioClip _attackClip;

    private AudioSource _audioSource;

    public float beforeDelay = 0.25f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartDelay()
    {
        StartCoroutine(BeforeDelay());
    }

    private void PlayAttackSound()
    {
        _audioSource.Stop();
        _audioSource.clip = _attackClip;
        _audioSource.Play();
    }


    IEnumerator BeforeDelay()
    {
        PlayAttackSound();
        yield return new WaitForSeconds(beforeDelay);

        StartSpawnEffect();
    }

    private void StartSpawnEffect()
    {
        AttackStart attackEffect = PoolManager.inst.Pop("DefaultAttackEffect") as AttackStart;
        Vector2 direction = MousePos - attackEffect.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackEffect.transform.position = new Vector2(PlayerTrm.position.x, PlayerTrm.position.y + 0.75f);
        attackEffect.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        attackEffect.transform.position = attackEffect.transform.position - attackEffect.transform.up;
    }
}
