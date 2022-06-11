using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AgentSubWeapon : MonoBehaviour
{
    [SerializeField] private List<SubWeaponController> _subWeapons = new List<SubWeaponController>();

    private void Start()
    {
        PEventManager.StartListening("CardAdd", ActiveAttack);
    }

    private void ActiveAttack(Param param)
    {
        ESubWeaponType type = ESubWeaponType.None;

        #region 추후 변경 예정
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

        }

        if (type == ESubWeaponType.None) return;

        var weapon = _subWeapons.Find(x => x.Type == type);

        weapon.ActiveWeapon();

    }
}
