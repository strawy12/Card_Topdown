using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using static UtilDefine;

public class ButtonStyle
{
    public EButtonStyle buttonStyle;
    public UnityAction _action;

    public ButtonStyle(EButtonStyle style, UnityAction action)
    {
        buttonStyle = style;
        _action = action;
    }
}

public class MessagePanal : MonoBehaviour
{
    [SerializeField] private MessagePanalBtn _buttonPref;
    private Text _currentText;
    private Transform _buttonParent;
    private Transform _background;


    private void Start()
    {
        Init();
        gameObject.SetActive(false);
    }

    private void Init()
    {
        _background = transform.Find("Background");

        _buttonParent = _background.transform.Find("Btns");
        _currentText = _background.transform.Find("Text").GetComponent<Text>();
    }


    public void ShowMessagePanal(string message, ButtonStyle btn, ButtonStyle btn2 = null)
    {
        gameObject.SetActive(true);
        _background.localScale = Vector3.zero;
        _currentText.text = message;

        InstatiateBtn(btn);
        InstatiateBtn(btn2);

        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(_background.DOScale(Vector3.one * 1.3f, 0.5f).SetEase(Ease.InOutBounce));

        seq.Play();
    }

    private void ClosePanal()
    {
        _background.DOScale(Vector3.zero, 0.3f).OnComplete(ResetPanal).SetUpdate(true);
    }

    private void ResetPanal()
    {
        for (int i = 0; i < _buttonParent.childCount; i++)
        {
            Destroy(_buttonParent.GetChild(i).gameObject);
        }

        gameObject.SetActive(false);
    }

    private void InstatiateBtn(ButtonStyle btnStyle)
    {
        if (btnStyle == null) return;


        MessagePanalBtn btn = Instantiate(_buttonPref, _buttonParent);
        string str = "";

        switch (btnStyle.buttonStyle)
        {
            case EButtonStyle.Okay:
                str = "확인";
                break;

            case EButtonStyle.Cancel:
                str = "취소";
                break;

            case EButtonStyle.Close:
                str = "닫기";
                break;
        }

        btn.ButtonText.text = str;
        btn.onClick.AddListener(btnStyle._action);
        btn.onClick.AddListener(ClosePanal);
    }
}
