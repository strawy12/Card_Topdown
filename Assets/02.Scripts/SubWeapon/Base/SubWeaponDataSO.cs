using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESubWeaponType
{
    None,
    SunLight,
    SunExplosion,
    MountainImmune,
    MountainBarrier,
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
    KnockBack,
    Fetter,
    Stun,
    Slow,
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
    public EStatType statType;
    public float statAmount;

    public bool isCrowdCtrl;
    public ECrowdControlType crowdCtrlType;
    public float crowdCtrlAmount;

    public bool needMove;
    public float movementSpeed;
}
