using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESubWeaponType
{
    None,
    /// <summary>
    /// 카서스 W
    /// </summary>
    SunLight,
    /// <summary>
    /// 카서스 Q
    /// </summary>
    SunExplosion,
    /// <summary>
    /// 피해감소 실드
    /// </summary>
    ReducedShield,
    /// <summary>
    /// 무적 실드
    /// </summary>
    InvincibleShield,
    /// <summary>
    /// 소환수(콩콩이)
    /// </summary>
    MountainAnimal,
    /// <summary>
    /// 시셀라 W
    /// </summary>
    RiverBarrier,
    /// <summary>
    /// 속박
    /// </summary>
    VineBondage,
    ClockStun,
    RockProjectile,
    /// <summary>
    /// 반사 실드
    /// </summary>
    RockReflection,
    RainCloud,
    CloudBounce,
    ForestTrail,
    ThornShield,
    BambooSpear,
    RainPine,
    TurtleProjectile,
    HornTrap
}

public enum EStatType
{
    AttackDamage,
    Defense,
    AttackSpeed,
    CriticalPercent,
    CriticalDamage,
    HpReproduction,
    DamageReduction
}

public enum ECrowdControlType
{
    None = 0,
    KnockBack =  1 << 0,
    Fetter = 1 << 1,
    Stun = 1 << 2,
    Slow = 1 << 3,
    All = ~0
}

[System.Serializable]
public class StatPair
{
    public EStatType statType;
    public float statAmount;
}


[CreateAssetMenu(menuName = "SO/Weapon/SubWeapon")]
public class SubWeaponDataSO : ScriptableObject
{
    public float delayTime;

    public bool isAttack;
    public float damageAmount;

    public bool isSpawn;
    public PoolableMono prefab;
    public bool isBounce;
    public int maxGenerateCnt;

    public bool needLifeTime;
    public bool isInfinite;
    public float lifeTime;

    public bool changeStat;
    public List<StatPair> changeStatList;

    public bool isCrowdCtrl;
    public int crowdCtrlTypes;
    public float crowdCtrlAmount;

    public bool needMove;
    public float movementSpeed;
}
