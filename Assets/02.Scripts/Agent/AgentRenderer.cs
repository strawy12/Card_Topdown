using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    public virtual void ChangeFace(Vector2 Vec)
    {
        if (Vector2.Dot(Vec, Vector2.right) > 0)
        {
            _transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Vector2.Dot(Vec, Vector2.right) < 0)
        {
            _transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
