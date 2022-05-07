using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void ChangeFace(Vector2 playerVec)
    {
        if (Vector2.Dot(playerVec, Vector2.right) > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (Vector2.Dot(playerVec, Vector2.right) < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
