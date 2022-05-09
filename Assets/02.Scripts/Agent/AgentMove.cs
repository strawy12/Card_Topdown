using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMove : MonoBehaviour
{
    public MoveDataSO moveData;

    public UnityEvent<float> OnVelocityChange;
    public UnityEvent<Vector2> OnVectorChange;

    private float moveSpeed = 5f;
    private Vector2 nowMoveDirection;

    private Rigidbody2D rb2D;
    private BoxCollider2D boxCol2D;

    private AgentStateCheck agentStateCheck;

    //public static bool isDashing = false;
    //public static bool isStop = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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
            GameManager.Inst.PlayerTrm.position.x,
            GameManager.Inst.PlayerTrm.position.y);
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
        OnVectorChange?.Invoke(rb2D.velocity.normalized);
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
}
