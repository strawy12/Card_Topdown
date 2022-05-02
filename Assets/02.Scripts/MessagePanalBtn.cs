using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanalBtn : Button
{
    private Text _buttonText;
    public Text ButtonText
    {
        get => _buttonText;
    }

    protected override void Start()
    {
        base.Start();
        _buttonText = transform.Find("Text").GetComponent<Text>();
    }
}
