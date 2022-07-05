using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public float maxHp;
    public float attackDamage;
    public float attackSpeed;
    public float defence;
    public float criticalPercent;
    public float criticalDamage;
    public float CooldownDecrease;
    public float maxSpeed;
}



[System.Serializable]
public class PlayerData
{
    public float effectSoundVolume;
    public float bgmSoundVolume;

    public bool isTutorial;

    public List<string> playerableCardInfoList;

    public PlayerData(float soundVolume)
    {
        effectSoundVolume = soundVolume;
        bgmSoundVolume = soundVolume;
        isTutorial = false;
        playerableCardInfoList = new List<string>(); 
    }

}
