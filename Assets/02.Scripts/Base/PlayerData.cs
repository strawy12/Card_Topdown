using System.Collections.Generic;
using UnityEngine;

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
