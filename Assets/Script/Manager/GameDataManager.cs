public class GameDataManager : MonoSingleton<GameDataManager>
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitGameData()
    {
#if SOHOShop
        // 提现商店初始化
        // 提现商店中的金币、现金和amazon卡均为double类型，参数请根据具体项目自行处理
        SOHOShopManager.instance.InitSOHOShopAction(
            GetToken,
            GetGold, 
            GetAmazon,    // amazon
            (subToken) => { AddToken(-subToken); }, 
            (subGold) => { AddGold(-subGold); }, 
            (subAmazon) => { AddAmazon(-subAmazon); });
#endif
    }

    // 金币
    public int GetCoin()
    {
        return SaveDataManager.GetInt(CConfig.sv_GoldCoin);
    }

    public void SubCoin(int coin)
    {
        SaveDataManager.SetInt(CConfig.sv_GoldCoin, SaveDataManager.GetInt(CConfig.sv_GoldCoin) - coin);
    }

    public void AddGold(int coin)
    {
        SaveDataManager.SetInt(CConfig.sv_GoldCoin, SaveDataManager.GetInt(CConfig.sv_GoldCoin) + coin);
        SaveDataManager.SetInt(CConfig.sv_CumulativeGoldCoin,
            SaveDataManager.GetInt(CConfig.sv_CumulativeGoldCoin) + coin);
    }
}