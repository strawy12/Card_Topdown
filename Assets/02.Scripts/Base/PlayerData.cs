using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float effectSoundVolume;
    public float bgmSoundVolume;

    public bool isTutorial;

    public PedigreeData[] pedigreeDatas;

    public PlayerData(float soundVolume)
    {
        effectSoundVolume = soundVolume;
        bgmSoundVolume = soundVolume;
        isTutorial = false;

        pedigreeDatas = new PedigreeData[5];
    }
}
