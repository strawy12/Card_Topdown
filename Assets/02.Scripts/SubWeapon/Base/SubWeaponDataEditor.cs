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

        EditorUtility.SetDirty(Target);
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
        bool isClick = GUILayout.Button(isActive ? "��" : "��", GUILayout.Width(20), GUILayout.Height(20));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        return isClick;
    }

    public void ActiveMoveType()
    {
        if (ActiveLabel("������", Target.needMove))
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
        if (ActiveLabel("����", Target.isAttack))
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
        if (ActiveLabel("����ü", Target.isSpawn))
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
        if (ActiveLabel("���ӽð�", Target.needLifeTime))
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
        if (ActiveLabel("���� ��ȭ", Target.changeStat))
        {
            Target.changeStat = !Target.changeStat;
        }

        if (Target.changeStat)
        {
            SerializedProperty listProperty = serializedObject.FindProperty("changeStatList");

            EditorGUILayout.PropertyField(listProperty, true);
            Target.changeStat = true;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.Space(selling);

    }

    public void ActiveCrowdCtrlType()
    {
        if (ActiveLabel("CC��", Target.isCrowdCtrl))
        {
            Target.isCrowdCtrl = !Target.isCrowdCtrl;
        }

        if (Target.isCrowdCtrl)
        {
            Target.crowdCtrlTypes = (int)((ECrowdControlType)EditorGUILayout.EnumFlagsField("CrowdCtrlType", (ECrowdControlType)Target.crowdCtrlTypes));
            Target.crowdCtrlAmount = EditorGUILayout.FloatField("CrowdCtrlAmount", Target.crowdCtrlAmount);

        }
        EditorGUILayout.Space(selling);
    }

}