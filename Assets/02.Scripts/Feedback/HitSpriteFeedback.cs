using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSpriteFeedback : Feedback
{
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField] private float flashDelayTime = 0.1f;
    private void Awake()
    {
        _spriteRenderer = transform.parent.Find("visualSprite").GetComponent<SpriteRenderer>();
    }
    public override void CompleteFeedback()
    {
        StopAllCoroutines();
        _spriteRenderer.enabled = true;
    }

    public override void CreateFeedback()
    {
        StartCoroutine(SpriteFlash());
    }
    private IEnumerator SpriteFlash()
    {
        for (int i = 0; i < 2; i++)
        {
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDelayTime);
            _spriteRenderer.enabled = true;
        }
    }
}
