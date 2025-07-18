// Project  RopeUp
// FileName  RopeUpMainManager.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 18:06:49 
//


using UnityEngine;

public class RopeUpMainManager : MonoSingleton<RopeUpMainManager>
{
    public void InitData()
    {
        // if (!SaveDataManager.HasKey(RopeConstant.IsNewCattle + "Bool"))
        // {
        //     // is new user
        //     SaveDataManager.SetInt(CConfig.sv_GoldCoin, 0);
        //     SaveDataManager.SetInt(CConfig.sv_CumulativeGoldCoin, 0);
        //
        //     SaveDataManager.SetInt(RopeConstant.CattleLevel, 1);
        //     SaveDataManager.SetBool(RopeConstant.IsNewCattle, true);
        // }

        CattleLevelManager.Instance.InitData();
    }


    public void AddCoin(int coin)
    {
        SaveDataManager.SetInt(CowConstant.CoinKey, SaveDataManager.GetInt(CowConstant.CoinKey) + coin);
        // GameDataManager.GetInstance().AddGold(coin);
    }

    public void SubCoin(int coin)
    {
        SaveDataManager.SetInt(CowConstant.CoinKey, SaveDataManager.GetInt(CowConstant.CoinKey) - coin);
        // GameDataManager.GetInstance().SubCoin(coin);
    }
}


public class RopeConstant
{
    public static string IsNewCattle = "IsNewCattle";
    public static string CattleLevel = "CattleLevel";
}