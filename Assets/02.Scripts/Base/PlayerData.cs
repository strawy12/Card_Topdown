using System.Collections.Generic;
using UnityEngine;
using static GenealogyDefine;

public class PlayerStat
{
    public float maxHp;
    public float attackDamage;
    public float attackSpeed;
    public float defence;
    public float criticalPercent;
    public float criticalDamage;
    public float CooldownDecrease;
}



[System.Serializable]
public class PlayerData
{
    public float effectSoundVolume;
    public float bgmSoundVolume;

    public bool isTutorial;

    public PlayerData(float soundVolume)
    {
        effectSoundVolume = soundVolume;
        bgmSoundVolume = soundVolume;
        isTutorial = false;
    }

}
