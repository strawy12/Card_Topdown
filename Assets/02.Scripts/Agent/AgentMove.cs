using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMove : MonoBehaviour
{
    public MoveDataSO moveData;

    public UnityEvent<float> OnVelocityChange;

    private float moveSpeed = 5f;
    private Vector2 nowMoveDirection;

    private Rigidbody2D rb2D;
    private BoxCollider2D boxCol2D;

    private bool isStop = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCol2D = GetComponentInChildren<BoxCollider2D>();
    }

    public void OnMove(Vector2 plyaerVec)
    {
        if (plyaerVec.sqrMagnitude > 0) // 값이 0이 아니면 모두 확인가능
        {
            if (Vector2.Dot(plyaerVec, nowMoveDirection) < 0) // 플레이어 방향이 지금움직이는 방향과 90도 초과해서 움직였는지 판단
            {
                Debug.Log("멈춤");
                moveSpeed = 0f; // 잠시 멈춤
            }
            nowMoveDirection = plyaerVec.normalized; // 현재 움직임 방향을 플레이어의 방향벡터로 바꿈
        }
        moveSpeed = ChangeSpeed(plyaerVec);
    }

    private float ChangeSpeed(Vector2 playerVec)
    {
        if (playerVec.sqrMagnitude > 0) // 플레이어가 움직이고 있다면
        {
            Debug.Log("가속중");
            moveSpeed += moveData.acceleration * Time.deltaTime; // 가속
        }
        else // 움직이고 있지 않다면
        {
            Debug.Log("감속중");
            moveSpeed -= moveData.deceleration * Time.deltaTime; // 감속
        }
        return Mathf.Clamp(moveSpeed, 0, moveData.maxSpeed); // 최소값과 최대값을 Clamp로 제한해줌
    }

    public void OnDash(Vector2 mouseWorldPosition)
    {
        isStop = true;
        rb2D.velocity = mouseWorldPosition.normalized * 40f;

        StartCoroutine(DashTimeCheck());
    }

    IEnumerator DashTimeCheck()
    {
        float time = 0.125f;

        yield return new WaitForSecondsRealtime(time);

        rb2D.velocity = Vector2.zero;
        isStop = false;
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(moveSpeed);
        if (isStop == true) return;
        rb2D.velocity = nowMoveDirection * moveSpeed;
    }
}
