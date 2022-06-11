using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilDefine;

public class AgentMove : MonoBehaviour, IKnockBack
{
    public MoveDataSO moveData;

    public UnityEvent<float> OnVelocityChange;
    public UnityEvent<Vector2> OnVectorChange;

    private float moveSpeed = 5f;
    private Vector2 nowMoveDirection;

    private Rigidbody2D rb2D;
    private BoxCollider2D boxCol2D;

    // 현재 상태
    private AgentStateCheck agentStateCheck;

    // 넉백
    private bool _isKnockBacking = false;
    private Coroutine _knockBackCoroutine = null;

    private void Start()
    {
        rb2D = GetComponentInParent<Rigidbody2D>();
        boxCol2D = GetComponentInChildren<BoxCollider2D>();
        agentStateCheck = GetComponent<AgentStateCheck>();
    }

    public void OnMove(Vector2 plyaerVec)
    {
        if (plyaerVec.sqrMagnitude > 0)
        {
            if (Vector2.Dot(plyaerVec, nowMoveDirection) < 0)
            {
                moveSpeed = 0f;
            }
            nowMoveDirection = plyaerVec.normalized;
        }
        moveSpeed = ChangeSpeed(plyaerVec);
    }

    private float ChangeSpeed(Vector2 playerVec)
    {
        if (playerVec.sqrMagnitude > 0)
        {
            moveSpeed += moveData.acceleration * Time.deltaTime;
        }
        else
        {
            moveSpeed -= moveData.deceleration * Time.deltaTime;
        }
        return Mathf.Clamp(moveSpeed, 0, moveData.maxSpeed);
    }

    public void OnDash(Vector2 mouseWorldPosition)
    {
        if (agentStateCheck.IsDashing == true) return;
        if (agentStateCheck.IsStop == true) return;
        agentStateCheck.IsDashing = true;
        Vector2 playerPos = new Vector2(
            PlayerTrm.position.x,
            PlayerTrm.position.y);
        Vector2 dashDir = mouseWorldPosition - playerPos;
        rb2D.velocity = dashDir.normalized * 40f;

        StartCoroutine(DashTimeCheck());
    }

    IEnumerator DashTimeCheck()
    {
        float time = 0.125f;

        yield return new WaitForSecondsRealtime(time);

        rb2D.velocity = Vector2.zero;
        agentStateCheck.IsDashing = false;
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(moveSpeed);
        
        if (agentStateCheck.IsDashing == true) return;
        if (agentStateCheck.IsStop == true) return;

        rb2D.velocity = nowMoveDirection * moveSpeed;
    }

    public void StopImmediatelly()
    {
        moveSpeed = 0;
        rb2D.velocity = Vector2.zero;
        agentStateCheck.IsStop = true;
    }

    public void EndMoveStop()
    {
        agentStateCheck.IsStop = false;
    }

    public void KnockBack(Vector2 dir, float power, float duration)
    {
        if (_isKnockBacking == false)
        {
            _isKnockBacking = true;
            StartCoroutine(KnockBackCoroutine(dir, power, duration));
        }
    }

    IEnumerator KnockBackCoroutine(Vector2 dir, float power, float duration)
    {
        Debug.Log(dir);
        Debug.Log(power);
        Debug.Log(duration);
        rb2D.AddForce(dir * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParem();
    }

    private void ResetKnockBackParem()
    {
        moveSpeed = 0;
        rb2D.velocity = Vector2.zero;
        _isKnockBacking = false;
        rb2D.gravityScale = 0;
    }
}
