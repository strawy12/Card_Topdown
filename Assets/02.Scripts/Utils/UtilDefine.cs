using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class UtilDefine
{
    public enum EButtonStyle
    {
        None = -1,
        Okay,
        Cancel,
        Close
    }

    public enum ESubWeaponType
    {
        None = -1,
    }

    private static Camera mainCam;
    public static Camera MainCam
    {
        get
        {
            mainCam ??= Camera.main;
            return mainCam;
        }
    }

    public static Vector3 MousePos
    {
        get
        {
            Vector3 pos = MainCam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
    }

    private static Player _player;

    public static Player PlayerRef
    {
        get
        {
            if (_player == null)
                _player = GameObject.FindObjectOfType<Player>();

            return _player;
        }
    }

    private static CinemachineVirtualCamera _cmVCam = null;
    
    public static CinemachineVirtualCamera VCam
    {
        get
        {
            if (_cmVCam == null)
            {
                _cmVCam = Object.FindObjectOfType<CinemachineVirtualCamera>();
            }
            return _cmVCam;
        }
    }

    public static float CalcPercent(float value, float percent)
    {
        return (value * 100) / percent;
    }
}

public static class Constant
{
    public const float           MAX_CARDGAUGE = 100f;

    public const string         POINTDOWN_CARD = "PD_CD";
    public const string           POINTUP_CARD = "PU_CD";
    public const string             CLICK_CARD = "CL_CD";
    public const string            RETURN_CARD = "RT_CD";


    public const string     RETURN_CARD_EFFECT = "RT_CD_EF";

    public const string      ENTER_MOUNTING_UI = "ET_MT_UI";
    public const string  NOT_ENTER_MOUNTING_UI = "N_ET_MT_UI";

    public const string               ENTER_UI = "ET_UI";
    public const string                EXIT_UI = "EX_UI";

    public const string       ENTER_CARD_PANAL = "ET_CD_PL";
    public const string        EXIT_CARD_PANAL = "EX_CD_PL";

    public const string   TRIGGER_CHANGE_EVENT = "TG_CG_EV";
    public const string TRIGGER_MOUNTING_EVENT = "TG_MT_EV";
    public const string         ADD_CARD_GAUGE = "AD_CD_GG";
    public const string       TRIGGER_ADD_CARD = "TR_AD_CD";
    public const string      TRIGGER_PICK_CARD = "TR_PC_CD";
    public const string      TRIGGER_WANT_PICK = "TG_WT_PK";
    public const string    TRIGGER_RANDOM_PICK = "TG_RD_PK";

    public const string          CLOSE_MESSAGE = "CL_MS";

    public const string       MESSAGE_MOUNTING = "정말로 장착하시겠습니까?";

    public const string         OPEN_INVENTORY = "OP_IV";
    public const string    PLAYER_ATTACK_START = "PL_AT_ST";
    public const string      PLAYER_ATTACK_END = "PL_AT_ED";

    public const string     ACTIVE_WANTPICK_UI = "AC_WP_UI";
    public const string     ACTIVE_DRAWPICK_UI = "AC_DP_UI";

    public const string  ACTIVE_PICK_SELECT_UI = "AC_PK_SL_UI";





}
