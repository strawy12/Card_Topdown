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
        PEventManager.StartListening(ENTER_CARDPANAL, ShowInfo);
        EventManager.StartListening(EXIT_CARDPANAL, UnShowInfo);
    }

    void ShowInfo(Param param)
    {
        ProcessCardData(param.sParam);

        transform.position = param.vParam + (Vector3)_offset;
        transform.DOScaleX(0f, 0f);
        transform.DOScaleX(1f, 0.5f);

        WriteCardInfo();
    }

    void UnShowInfo()
    {
        transform.DOScaleX(0f, 0.5f);
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

        string info = $"종류 : {Define.GetCardInfo(_currentCard.CardNum)}\n 월 : {_currentCard.CardNum}";
        _infoText.DOText(info, 0.5f);
    }

    private void OnDestroy()
    {
        PEventManager.StopListening(ENTER_CARDPANAL, ShowInfo);
        EventManager.StopListening(EXIT_CARDPANAL, UnShowInfo);
    }
}
