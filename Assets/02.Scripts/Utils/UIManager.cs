using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MessagePanal _messagePanal;

    public void TriggerMessage(string message, ButtonStyle btnStyle, ButtonStyle btnStyle2 = null)
    {
        _messagePanal.ShowMessagePanal(message, btnStyle, btnStyle2);
    }

}
