using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMove : MonoBehaviour
{
    private Rigidbody2D _rigid;

    [SerializeField][Range(1, 10)]
    private float maxSpeed = 5;
    [SerializeField][Range(0.1f, 100f)]
    private float acceleration = 50, deAcceleration = 50;

    protected float _currentVelocity = 3f;
    protected Vector2 _movementDirection;

    public UnityEvent<float> OnVelocityChange; //플레이어 속도가 바뀔때 실행될 이벤트

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            if (Vector2.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += acceleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0, maxSpeed);
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(_currentVelocity);
        _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
    }
}
