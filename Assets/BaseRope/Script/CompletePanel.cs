// Project  RopeUp
// FileName  CompletePanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-20 11:06:40 
//


using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CompletePanel: MonoBehaviour
{
    
    public Button getNormalBtn;
    
    public Button getMoreBtn;

    public Text rewardText;
    
    private readonly int _rewardMulti = 2;
    
    private readonly int _rewardNum = 10;

    
    private int _curCoin;


    private void Start()
    {
        
        getMoreBtn.onClick.AddListener(() =>
        {

            A_ADManager.Instance.playRewardVideo((success) =>
            {
                if (success)
                {
                    GetMoreReward();
                }
            });
        });

        getNormalBtn.onClick.AddListener(() =>
        {
            A_ADManager.Instance.ShowInterstitialAd();
            GetNormalReward();
        });
        
    }

    private void OnEnable()
    {
        _curCoin = _rewardNum;
        ShowUI();
    }

    private void ShowUI()
    {
        rewardText.text = _rewardNum.ToString();
        getMoreBtn.gameObject.SetActive(true);
        getNormalBtn.gameObject.SetActive(true);
    }
    
    
    private void ChangeNumber()
    {
        
            // AnimationController.ChangeNumber(_curCoin, _curCoin * _rewardMulti, 0.2f, rewardText,
            //     () => { });
            _curCoin *= _rewardMulti;
            rewardText.text = _curCoin.ToString();
    }
    
    private async void GetMoreReward()
    {
        getMoreBtn.gameObject.SetActive(false);
        getNormalBtn.gameObject.SetActive(false);
        ChangeNumber();
        await UniTask.Delay(500);
        GetRewardAndClose();
    }
    
    
    private async void GetNormalReward()
    {
        getMoreBtn.gameObject.SetActive(false);
        getNormalBtn.gameObject.SetActive(false);
        await UniTask.Delay(100);
        GetRewardAndClose();
    }
    

    private void GetRewardAndClose()
    {
        CattleGamePanel.Instance.AfterCompletePanel(_curCoin);
        gameObject.SetActive(false);
    }

    

}
