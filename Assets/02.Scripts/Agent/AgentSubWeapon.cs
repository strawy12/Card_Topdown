using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;




public class AgentSubWeapon : MonoBehaviour
{
    public enum ESubWeaponType
    {
        None = -1,
        Book = 0,
        Axe,
        Fireball,
        MagicWand,
        Whip,
        WeaponCnt
    }

    private List<SubWeaponController> _subWeapons = new List<SubWeaponController>();
    private SubWeaponController[] _activeSubWeapons = new SubWeaponController[4];

    private void Start()
    {
        PEventManager.StartListening("CardAdd", ActiveAttack);

        _subWeapons = transform.Find("SubWeapon").GetComponents<SubWeaponController>().ToList();

        foreach (var ctrl in _subWeapons)
        {
            if (ctrl.IsActive)
            {
                ctrl.IsActive = false;
            }
        }
    }

    private void ActiveAttack(Param param)
    {
        ESubWeaponType type = ESubWeaponType.None;

        #region ���� ���� ����
        //switch ((EGenealogy)param.iParam)
        //{
        //    case EGenealogy.Rest:
        //        type = ESubWeaponType.Book;
        //        break;
        //    case EGenealogy.Pair:
        //        break;
        //    case EGenealogy.GuSa:
        //        break;
        //    case EGenealogy.PairHunter:
        //        break;
        //    case EGenealogy.ESibal:
        //        break;
        //    case EGenealogy.SeRyuk:
        //        break;
        //    case EGenealogy.Jangsa:
        //        break;
        //    case EGenealogy.BBing:
        //        break;
        //    case EGenealogy.Doksa:
        //        break;
        //    case EGenealogy.Ali:
        //        break;
        //    case EGenealogy.GabO:
        //        break;
        //    case EGenealogy.Mangtong:
        //        break;
        //    case EGenealogy.LightPair:
        //        break;
        //}

        #endregion

        switch(param.iParam)
        {
            case 0:
                type = ESubWeaponType.Whip;
                break;
            case 1:
                type = ESubWeaponType.Book;
                break;

            case 2:
                type = ESubWeaponType.Axe; // �Ǵ� ä�� (����)
                break;

            case 3:
                type = ESubWeaponType.MagicWand;
                break;

            case 4:
                type = ESubWeaponType.Fireball;
                break;

            default:
                return;
        }

        if (type == ESubWeaponType.None) return;

        var weapon = _subWeapons.Find(x => x.Type == type);

        weapon.IsActive = true;
        weapon.ActiveAttack();

    }
}
