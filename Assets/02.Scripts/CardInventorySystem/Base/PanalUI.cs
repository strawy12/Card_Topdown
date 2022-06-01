using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanalUI : MonoBehaviour
{
    public void ActiveUI()
    {
        ChildActiveUI();
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.6f).SetUpdate(true).SetEase(Ease.InOutElastic);
        GameManager.Inst.UI.PushPanal(gameObject);
        Debug.Log(gameObject.activeSelf);
    }

    protected virtual void ChildActiveUI() { }

    public void UnActiveUI(System.Action action = null)
    {
        GameManager.Inst.UI.ClosePanal(gameObject, action);
        ChildUnActiveUI();
    }

    protected virtual void ChildUnActiveUI() { }

}
