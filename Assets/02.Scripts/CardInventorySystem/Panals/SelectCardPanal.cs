using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCardPanal : MonoBehaviour
{
    private int _currentMonth;

    private Text _mouthText;
    private Image _cardImage;

    public void Init(Param param)
    {
        _mouthText ??= transform.Find("MouthText").GetComponent<Text>();
        _cardImage ??= transform.Find("CardImage").GetComponent<Image>();

        _mouthText.text = $"{param.iParam}¿ù";
        _cardImage.sprite = GameManager.Inst.FindCardDataWithID(param.sParam).CardSprite;
    }

}
