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
    public virtual void ChangeFace(Vector2 mousePosition)
    {
        if (mousePosition.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }
}
