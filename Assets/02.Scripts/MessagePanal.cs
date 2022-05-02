using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using static Define;

public class ButtonStyle
{
    public EButtonStyle buttonStyle;
    public UnityAction action;
}

public class MessagePanal : MonoBehaviour
{
    [SerializeField] private MessagePanalBtn _buttonPref;
    private Text _currentText;
    private Transform _buttonParent;

    void Start()
    {
        _buttonParent = transform.Find("Buttons");
        _currentText = transform.Find("Text").GetComponent<Text>();
    }


    public void InitMessagePanal(string message, ButtonStyle btn, ButtonStyle btn2 = null)
    {
        transform.localScale = Vector3.zero;
        _currentText.text = message;

        InstatiateBtn(btn);
        InstatiateBtn(btn2);

        transform.DOScale(Vector3.one, 0.5f);
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
        btn.onClick.AddListener(btnStyle.action);
    }
}
