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
        if (plyaerVec.sqrMagnitude > 0) // ���� 0�� �ƴϸ� ��� Ȯ�ΰ���
        {
            if (Vector2.Dot(plyaerVec, nowMoveDirection) < 0) // �÷��̾� ������ ���ݿ����̴� ����� 90�� �ʰ��ؼ� ���������� �Ǵ�
            {
                Debug.Log("����");
                moveSpeed = 0f; // ��� ����
            }
            nowMoveDirection = plyaerVec.normalized; // ���� ������ ������ �÷��̾��� ���⺤�ͷ� �ٲ�
        }
        moveSpeed = ChangeSpeed(plyaerVec);
    }

    private float ChangeSpeed(Vector2 playerVec)
    {
        if (playerVec.sqrMagnitude > 0) // �÷��̾ �����̰� �ִٸ�
        {
            Debug.Log("������");
            moveSpeed += moveData.acceleration * Time.deltaTime; // ����
        }
        else // �����̰� ���� �ʴٸ�
        {
            Debug.Log("������");
            moveSpeed -= moveData.deceleration * Time.deltaTime; // ����
        }
        return Mathf.Clamp(moveSpeed, 0, moveData.maxSpeed); // �ּҰ��� �ִ밪�� Clamp�� ��������
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
