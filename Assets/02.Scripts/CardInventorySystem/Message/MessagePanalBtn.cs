using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanalBtn : Button
{
    private Text _buttonText;
    public Text ButtonText
    {
        get
        {
            _buttonText ??= transform.Find("Text").GetComponent<Text>();

            return _buttonText;
        }
    }
}
