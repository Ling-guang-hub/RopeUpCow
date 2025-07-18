// Project  RopeUp
// FileName  TimeBar.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 18:06:03 
//


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Text timeText;


    private int _countDownTime = 0;

    private Coroutine _countDownCoroutine;

    private bool _hasAddTime;


    public void InitData()
    {
        _hasAddTime = false;
        _countDownTime = CattleLevelManager.Instance.GetLevelTime();

        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
            _countDownCoroutine = null;
        }

        ShowTimeData();
    }

    public void StartTimeBar()
    {
        _countDownTime = CattleLevelManager.Instance.GetLevelTime();

        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
        }

        _countDownCoroutine = StartCoroutine("CountdownCoroutine");
    }


    public void StopTimeBar()
    {
        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
            _countDownCoroutine = null;
        }
    }

    public void AddTimeForTimeBar()
    {
        _countDownTime += 15;
        ShowTimeData();
        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
        }
        _countDownCoroutine = StartCoroutine("CountdownCoroutine");
    }


    private void ShowTimeData()
    {
        // TimeSpan t = TimeSpan.FromSeconds(_countDownTime);
        // timeText.text = $"{t.Hours:00}:{t.Minutes:00}:{t.Seconds:00}";
        timeText.text = "" + _countDownTime;
    }


    private void ShowFinishPanel()
    {
        if (!_hasAddTime)
        {
            _hasAddTime = true;
            CattleGamePanel.Instance.ShowTimeStore();
        }
        else
        {
            CattleGamePanel.Instance.ShowLosePanel();
        }

        StopTimeBar();
    }


    IEnumerator CountdownCoroutine()
    {
        while (_countDownTime > 0)
        {
            _countDownTime--;
            ShowTimeData();
            yield return new WaitForSeconds(1f);
            if (_countDownTime != 0) continue;
            ShowFinishPanel();
        }
    }
}