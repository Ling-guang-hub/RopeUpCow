// Project  RopeUp
// FileName  RestorePanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-20 10:06:53 
//


using UnityEngine;
using UnityEngine.UI;

public class RestorePanel : BaseUIForms
{
    public Button addTimeBtn;

    public Button addByCoin;

    public Button closeBtn;

    private readonly int timeCost = 50;

    private void Start()
    {
        addTimeBtn.onClick.AddListener(() =>
        {
            
            A_ADManager.Instance.playRewardVideo((success) =>
            {
                if (success)
                {
                    AddTime();
                }
            });
     
        });

        closeBtn.onClick.AddListener(() => { NotAddTime(); });
        
        addByCoin.onClick.AddListener(() => { AddByCoin(); });
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);

        addByCoin.gameObject.SetActive(SaveDataManager.GetInt(CowConstant.CoinKey) >= 50);

        Invoke("NotAddTime", 6f);
    }


    private void AddByCoin()
    {
        if (IsInvoking("NotAddTime"))
        {
            CancelInvoke("NotAddTime");
        }

        if (SaveDataManager.GetInt(CowConstant.CoinKey) < 50) return;
        
        RopeUpMainManager.GetInstance().SubCoin(timeCost);

        CattleGamePanel.Instance.AddTime();
        ClosePanel();
    }

    private void AddTime()
    {
        if (IsInvoking("NotAddTime"))
        {
            CancelInvoke("NotAddTime");
        }
        CattleGamePanel.Instance.AddTime();
        ClosePanel();
    }

    private void NotAddTime()
    {
        if (IsInvoking("NotAddTime"))
        {
            CancelInvoke("NotAddTime");
        }

        CattleGamePanel.Instance.ShowLosePanel();
        ClosePanel();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}