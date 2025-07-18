// Project  RopeUp
// FileName  PropPanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-20 10:06:36 
//


using System.Collections.Generic;
using UnityEngine.UI;

public class PropPanel : BaseUIForms
{
    public Button getSkill01Btn;

    public Button getSkill02Btn;

    public Button continueBtn;
    
    public Button closeBtn;

    public Text skill01Text;

    public Text skill02Text;

    private readonly int _powerCost = 50;

    private readonly int _speedCost = 30;

    private Dictionary<RopeSkill, int> _skillDic;
    
    
    private void Start()
    {
        closeBtn.onClick.AddListener(() => { ClosePanel(); });
        
        continueBtn.onClick.AddListener(() => { ClosePanel(); });

        getSkill01Btn.onClick.AddListener(() =>
        {
            if (!CheckCoin(_powerCost)) return;
            getSkill01Btn.enabled = false;
            GetSkill(RopeSkill.Power);
        });

        getSkill02Btn.onClick.AddListener(() =>
        {
            if (!CheckCoin(_speedCost)) return;
            getSkill02Btn.enabled = false;
            GetSkill(RopeSkill.Speed);
        });
    }

    protected override void Awake()
    {
        base.Awake();
        _skillDic = new Dictionary<RopeSkill, int>
        {
            { RopeSkill.Power, _powerCost },
            { RopeSkill.Speed, _speedCost }
        };

        skill01Text.text = _powerCost + "";
        skill02Text.text = _speedCost + "";
    }


    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        ShowUI();
    }


    private bool CheckCoin(int num)
    {
        return SaveDataManager.GetInt(CowConstant.CoinKey) > num;
    }


    private void ShowUI()
    {
        getSkill01Btn.enabled = true;
        getSkill02Btn.enabled = true;
    }

    private void GetSkill(RopeSkill skill)
    {
        RopeUpMainManager.GetInstance().SubCoin(_skillDic[skill]);
        CattleGamePanel.Instance.SetSkill(skill);
    }

    
    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}