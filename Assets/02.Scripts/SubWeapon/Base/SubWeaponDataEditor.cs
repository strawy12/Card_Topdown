using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

[CustomEditor(typeof(SubWeaponDataSO))]
public class SubWeaponDataEditor : Editor
{
    int selling = 30;
    public SubWeaponDataSO Target
    {
        get => target as SubWeaponDataSO;
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(selling);

        Target.delayTime = EditorGUILayout.FloatField("DelayTime", Target.delayTime);
        EditorGUILayout.Space(selling);


        ActiveTypeToggles();


    }

    public void ActiveTypeToggles()
    {
        ActiveMoveType();
        ActiveAttackType();
        ActiveSummonType();
        ActiveStatType();
        ActiveCrowdCtrlType();

        if (((SubWeaponDataSO)target).isSpawn == false)
        {
            ActiveLifeType();

            EditorGUILayout.Space();
        }
    }

    public bool ActiveLabel(string label, bool isActive)
    {

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField($"{label} : {(isActive ? 'O' : 'X')}");
        bool isClick = GUILayout.Button(isActive ? "▼" : "◀", GUILayout.Width(20), GUILayout.Height(20));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        return isClick;
    }

    public void ActiveMoveType()
    {
        if (ActiveLabel("움직임", Target.needMove))
        {
            Target.needMove = !Target.needMove;
        }


        if (Target.needMove)
        {



            Target.movementSpeed = EditorGUILayout.FloatField("MovementSpeed", Target.movementSpeed);


        }
        EditorGUILayout.Space(selling);
    }


    public void ActiveAttackType()
    {
        if (ActiveLabel("공격", Target.isAttack))
        {
            Target.isAttack = !Target.isAttack;
        }

        if (Target.isAttack)
        {
            Target.damageAmount = EditorGUILayout.FloatField("DamageAmount", Target.damageAmount);
        }
        EditorGUILayout.Space(selling);

    }

    public void ActiveSummonType()
    {
        if (ActiveLabel("생성체", Target.isSpawn))
        {
            Target.isSpawn = !Target.isSpawn;
            Target.needLifeTime = Target.isSpawn;
        }

        if (Target.isSpawn)
        {
            Target.prefab = (PoolableMono)EditorGUILayout.ObjectField("Prefab", Target.prefab, typeof(PoolableMono), true);
            Target.maxGenerateCnt = EditorGUILayout.IntField("MaxGenerateCnt", Target.maxGenerateCnt);
            Target.isBounce = EditorGUILayout.Toggle("IsBounce", Target.isBounce);
            EditorGUILayout.Space();
            ActiveLifeType();
        }
        EditorGUILayout.Space(selling);

    }
    public void ActiveLifeType()
    {
        if (ActiveLabel("지속시간", Target.needLifeTime))
        {
            Target.needLifeTime = !Target.needLifeTime;
        }

        if (Target.needLifeTime)
        {

            Target.isInfinite = EditorGUILayout.Toggle("IsInfinite", Target.isInfinite);
            if (!Target.isInfinite)
            {
                Target.lifeTime = EditorGUILayout.FloatField("LifeTime", Target.lifeTime);

            }
        }
    }



    public void ActiveStatType()
    {
        if (ActiveLabel("스탯 변화", Target.changeStat))
        {
            Target.changeStat = !Target.changeStat;
        }

        if (Target.changeStat)
        {
            Target.statType = (EStatType)EditorGUILayout.EnumPopup("StatType", Target.statType);
            Target.statAmount = EditorGUILayout.FloatField("StatAmount", Target.statAmount);

        }
        EditorGUILayout.Space(selling);

    }

    public void ActiveCrowdCtrlType()
    {
        if (ActiveLabel("CC기", Target.isCrowdCtrl))
        {
            Target.isCrowdCtrl = !Target.isCrowdCtrl;
        }

        if (Target.isCrowdCtrl)
        {
            Target.crowdCtrlType = (ECrowdControlType)EditorGUILayout.EnumPopup("CrowdCtrlType", Target.crowdCtrlType);
            Target.crowdCtrlAmount = EditorGUILayout.FloatField("CrowdCtrlAmount", Target.crowdCtrlAmount);

        }
        EditorGUILayout.Space(selling);
    }

}
