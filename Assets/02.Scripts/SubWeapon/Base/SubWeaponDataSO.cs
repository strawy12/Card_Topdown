using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESubWeaponType
{
    None,
    SunLight,
    SunExplosion,
    ReducedShield,
    InvincibleShield,
    MountainAnimal,
    RiverBarrier,
    VineBondage,
    ClockStun,
    StoneProjectile,
    StoneReflection,
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
