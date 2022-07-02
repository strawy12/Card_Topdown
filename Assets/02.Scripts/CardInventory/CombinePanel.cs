using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UtilDefine;

public class CombinePanel : MonoBehaviour
{
   [SerializeField] private CardPanel[] _cardPanals = new CardPanel[2];
    private Text _combineEnforceText;
    private bool _isEmpty;

    private int _currentIndex;
    private int _currentEnforceCnt = 0;
    private int _needEnforceCnt = 0;

    public bool IsEmpty
    {
        get => _isEmpty;
    }

    public bool CanEnForce
    {
        get
        {
            return _currentEnforceCnt >= _needEnforceCnt;
        }
    }

    public void Init()
    {
        _combineEnforceText = transform.Find("CombineEnforceText").GetComponent<Text>();
        _combineEnforceText.text = "";
        _currentIndex = transform.GetSiblingIndex() - 1;
        _isEmpty = true;

        CardPanalsInit();
    }

    public void CardPanalsInit()
    {
        foreach(var panel in _cardPanals)
        {
            panel.Init();
        }
    }

    private void ChangePanal()
    {
        if (_cardPanals[1].IsEmpty) return;


        if (_currentIndex != 0)
            CalcCombination(_cardPanals[0].CurrentCardData, _cardPanals[1].CurrentCardData);

        if (_currentIndex != 0 && !_isEmpty)
        {
            SetCombineText();
            _combineEnforceText.gameObject.SetActive(true);
        }

        else
        {
            _combineEnforceText.gameObject.SetActive(false);
            _combineEnforceText.text = "";
        }
    }

    private void SetCombineText()
    {
        _combineEnforceText.text = $"+ {_currentEnforceCnt}";
    }

    private void CalcCombination(CardData cardData1, CardData cardData2)
    {
        int num1 = cardData1.CardNum;
        int num2 = cardData2.CardNum;

        if (num1 == num2)
        {
            return;
        }

        ESubWeaponType subweaponType = ESubWeaponType.None;

        if (num1 > num2)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }

        switch ((ECardType)num1)
        {
            case ECardType.Sun:

                if (CompareValue(num2, new int[] { 2, 3, 4, 5, 6, 7 }))
                {
                    subweaponType = ESubWeaponType.SunLight;
                }

                else
                {
                    subweaponType = ESubWeaponType.SunExplosion;
                }

                break;

            case ECardType.Mountain:

                if (CompareValue(num2, new int[] { 3, 4, 5 }))
                {
                    subweaponType = ESubWeaponType.ReducedShield;
                }

                else if (CompareValue(num2, new int[] { 6, 7 }))
                {
                    subweaponType = ESubWeaponType.InvincibleShield;
                }

                else
                {
                    subweaponType = ESubWeaponType.MountainAnimal;
                }

                break;

            case ECardType.River:

                if (CompareValue(num2, new int[] { 4, 5, 8 }))
                {
                    subweaponType = ESubWeaponType.RiverBarrier;
                }

                else if (CompareValue(num2, new int[] { 6, 7 }))
                {
                    subweaponType = ESubWeaponType.VineBondage;
                }

                else
                {
                    subweaponType = ESubWeaponType.ClockStun;
                }

                break;

            case ECardType.Rock:

                if (CompareValue(num2, new int[] { 5, 6, 7 }))
                {
                    subweaponType = ESubWeaponType.RockProjectile;
                }

                else
                {
                    subweaponType = ESubWeaponType.RockReflection;
                }

                break;

            case ECardType.Cloud:

                if (CompareValue(num2, new int[] { 6, 7 }))
                {
                    subweaponType = ESubWeaponType.RainCloud;
                }

                else
                {
                    subweaponType = ESubWeaponType.CloudBounce;
                }

                break;

            case ECardType.Bamboo:

                if (CompareValue(num2, 7))
                {
                    subweaponType = ESubWeaponType.ForestTrail;
                }

                else if (CompareValue(num2, 8))
                {
                    subweaponType = ESubWeaponType.ThornShield;
                }

                else
                {
                    subweaponType = ESubWeaponType.BambooSpear;
                }

                break;

            case ECardType.Pine:
                subweaponType = ESubWeaponType.RainPine;
                break;
            case ECardType.Turtle:
                subweaponType = ESubWeaponType.TurtleProjectile;
                break;
            case ECardType.Crane:
                subweaponType = ESubWeaponType.HornTrap;
                break;
        }


        Param param = new Param();
        param.iParam = (int)subweaponType;

        PEventManager.TriggerEvent("CardAdd", param);
        _isEmpty = false;
    }

    private void CloseMessage()
    {

    }

    //EnterUI 제작해야함
    private bool CheckEnforceMaterial(string id)
    {
        for (int i = 0; i < _cardPanals.Length; i++)
        {
            if (!_cardPanals[i].IsEmpty && _cardPanals[i].CurrentCardData.ID.Equals(id))
            {
                return true;
            }
        }

        return false;
    }

    private void EnForceMountMessage(string panelID, CardData data)
    {
        ButtonStyle btn1 = new ButtonStyle(EButtonStyle.Okay, () =>
        {
            _currentEnforceCnt++;
            SetCombineText();
        });

        ButtonStyle btn2 = new ButtonStyle(EButtonStyle.Cancel, () =>
        {
            CardInventoryManager.Inst.ReturnCard(panelID, data);
        });

        GameManager.Inst.UI.TriggerMessage($"해당 카드가 사라집니다.\n {Constant.MESSAGE_ENFORCE}", btn1, btn2);
    }
}
