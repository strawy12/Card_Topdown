using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static Constant;

public class CardInfoPanal : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;
    private Text _infoText; 
    private CardData _currentCard;

    private void Awake()
    {
        _infoText = transform.Find("InfoText").GetComponent<Text>();
        PEventManager.StartListening(ENTER_CARD_PANAL, ShowInfo);
        EventManager.StartListening(EXIT_CARD_PANAL, UnShowInfo);
        EventManager.StartListening(CLICK_CARD, ImmediateUnShow);

    }

    private void ShowInfo(Param param)
    {
        ProcessCardData(param.sParam);

        transform.position = param.vParam + (Vector3)_offset;
        transform.DOScaleX(0f, 0f);
        transform.DOScaleX(1f, 0.5f);

        WriteCardInfo();
    }

    private void UnShowInfo()
    {
        transform.DOScaleX(0f, 0.5f);
        _infoText.text = "";
        _currentCard = null;
    }

    private void ImmediateUnShow()
    {
        if (transform.localScale.x == 0f) return;

        transform.DOScaleX(0f, 0f);
        _infoText.text = "";
        _currentCard = null;
    }

    private void ProcessCardData(string cardID)
    {
        _currentCard = GameManager.Inst.FindCardDataWithID(cardID);
        if(_currentCard == null)
        {
            Debug.LogError("카드 ID를 어디서 잘못 입력하셨습니다." + cardID);
            return;
        }
    }

    private void WriteCardInfo()
    {
        if (_currentCard == null) return;

        _infoText.text = "";

        string info = $"종류 : {GenealogyDefine.GetCardInfo(_currentCard.CardNum)}\n {_currentCard.CardNum}월";
        _infoText.DOText(info, 0.5f);
    }

    private void OnDestroy()
    {
        PEventManager.StopListening(ENTER_CARD_PANAL, ShowInfo);
        EventManager.StopListening(EXIT_CARD_PANAL, UnShowInfo);
    }
}
