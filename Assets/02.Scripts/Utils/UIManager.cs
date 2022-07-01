using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private MessagePanal _messagePanal;
    [SerializeField] private TextMeshProUGUI _waveCountText;
    [SerializeField] private TextMeshProUGUI _remainMonsetText;
    [SerializeField] private GameObject _winPanal;
    [SerializeField] private GameObject _nextWavePanal;
    public UnityEvent<bool> OnUI;

    private Stack<GameObject> _panalStack = new Stack<GameObject>();


    private UIAudio _uiAudio;


    private void Awake()
    {
        _uiAudio = GetComponent<UIAudio>();
    }

    public void TriggerMessage(string message, ButtonStyle btnStyle, ButtonStyle btnStyle2 = null)
    {
        Debug.Log("dd");
        _messagePanal.ShowMessagePanal(message, btnStyle, btnStyle2);
    }

    public void PlayBtnClickSound()
    {
        _uiAudio.PlayButtonClickSound();
    }

    public void PlayAddCardSound()
    {
        _uiAudio.PlayAddCardSound();
    }

    public void PushPanal(GameObject panal)
    {
        _panalStack.Push(panal);
        OnUI?.Invoke(true);
    }

    private void UnActiveUI(GameObject obj, System.Action action = null)
    {
        obj.transform.DOScale(Vector3.zero, 0.6f)
                .SetUpdate(true)
                .SetEase(Ease.InOutElastic)
                .OnComplete(() =>
                {
                    obj.SetActive(false);
                    action?.Invoke();
                }
                );
    }

    public void ClosePanal()
    {
        if (_panalStack.Count == 0) return;

        GameObject panal = _panalStack.Pop();
        UnActiveUI(panal);
    }

    public void ClosePanalAll()
    {
        GameObject panal;
        while (_panalStack.Count != 0)
        {
            panal = _panalStack.Pop();
            panal.transform.localScale = Vector3.zero;
            panal.SetActive(false);
        }
        
    }

    public void ClosePanal(GameObject panal, System.Action action = null)
    {
        if (_panalStack.Count == 0) return;

        if (_panalStack.Contains(panal) == false)
        {
            UnActiveUI(panal, action);
            return;
        }

        GameObject tempPanal;

        while(true)
        {
            tempPanal = _panalStack.Pop();
            if (tempPanal != panal)
            {
                tempPanal.transform.localScale = Vector3.zero;
                tempPanal.SetActive(false);
            }

            else
            {
                UnActiveUI(tempPanal, action);
                break;
            }


        }
    }
    public void UpdateWaveInfo(int count, int maxCount)
    {
        _waveCountText.SetText($"Wave {count}/{maxCount}");
    }
    public void UpdateRemainMonsterInfo(int count, int maxCount)
    {
        _remainMonsetText.SetText($"Monster {count}/{maxCount}");
    }
    public void OpenGameClearUI()
    {
        _winPanal.SetActive(true);
    }
    public void OnClearWaveUI()
    {
        _nextWavePanal.SetActive(true);
    }
    public void ReStart()
    {
        SceneManager.LoadScene("Main");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
