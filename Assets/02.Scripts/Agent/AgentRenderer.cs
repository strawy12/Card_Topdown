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
    public virtual void ChangeFace(Vector2 playerVec)
    {
        Vector2 nowDir = playerVec - (Vector2)transform.position;
        if (Vector2.Dot(nowDir, Vector2.right) > 0)
        {
            _transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Vector2.Dot(nowDir, Vector2.right) < 0)
        {
            _transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
