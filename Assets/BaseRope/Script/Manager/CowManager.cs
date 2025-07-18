// Project  RopeUp
// FileName  CowManager.cs
// Author  AX
// Desc
// CreateAt  2025-06-18 15:06:46 
//


using System;
using UnityEngine;

public class CowManager : MonoBehaviour
{
    public static CowManager Instance;

    public GameObject cattleGamePanel;

    public GameObject losePop;

    public GameObject propPop;

    public GameObject restorePop;

    public GameObject finishPop;

    public GameObject toastPop;

    public GameObject settingPop;


    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        InitData();
    }


    private void InitData()
    {
        
        string newUserKey = CConfig.sv_IsNewPlayer + CowConstant.AppName;
        bool isNewPlayer = !PlayerPrefs.HasKey(newUserKey + "Bool") || SaveDataManager.GetBool(newUserKey);
        if (isNewPlayer)
        {
            SaveDataManager.SetInt(CowConstant.CoinKey, 0);
            SaveDataManager.SetBool(newUserKey, false);
        }
        
        
        CattleLevelManager.Instance.InitData();
        
        RopeUpMainManager.GetInstance().InitData();
    }


    public void ShowGamePanel()
    {
        // cattleHomePanel.SetActive(false);
        cattleGamePanel.SetActive(true);
    }

    public void ShowHomePanel()
    {
        // cattleHomePanel.SetActive(true);
        cattleGamePanel.SetActive(false);
    }

    public void ShowLosePop()
    {
        losePop.SetActive(true);
    }

    public void ShowPropPop()
    {
        propPop.SetActive(true);
    }

    public void ShowRestorePop()
    {
        restorePop.SetActive(true);
    }

    public void ShowFinishPop()
    {
        finishPop.SetActive(true);
    }

    public void ShowSettingPop()
    {
        settingPop.SetActive(true);
    }


    public void ShowToast(string msg)
    {
        toastPop.gameObject.SetActive(true);
        toastPop.GetComponent<ToastPop>().ShowToast(msg);
    }
}

public static class CowConstant
{
    public static readonly string AppName = "Cow";

    public static string CoinKey = AppName + "Coin";
    
}