// Project  RopeUp
// FileName  GamePanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 15:06:34 
//


using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CattleGamePanel : BaseUIForms
{
    public static CattleGamePanel Instance;

    public GameObject bg;

    public GameObject ropeLoopObj;

    public Button closeBtn;

    public Button nextBtn;

    public Button startBtn;

    public Button settingBtn;

    public Button shopBtn;

    public Button homeBtn;

    // public Text levelText;

    public GameObject timeBar;

    // public GameObject skillSpeedObj;

    // public GameObject skillPowerObj;

    public GameObject topBar;

    public GameObject coinImg;

    public Text coinText;

    public GameObject gameArea;

    public GameObject playObj;

    private int _curLevel;

    private bool _isPower;

    private bool _isSpeed;

    private bool _isOnPlay;

    protected override void Awake()
    {
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
        startBtn.onClick.AddListener(() => { TryToStartGame(); });

        settingBtn.onClick.AddListener(() => { CowManager.Instance.ShowSettingPop(); });

        shopBtn.onClick.AddListener(() => { CowManager.Instance.ShowPropPop(); });

        homeBtn.onClick.AddListener(() => { FinishGame(); });
        
        DoAdaptation();

    }


    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);

        InitData();
        InitGame();
    }


    private void DoAdaptation()
    {
        
        float width = Screen.width;
        float height = Screen.height;
        float rate = width / height;
        
        if (rate > 0.5f)
        {
            topBar.gameObject.transform.localPosition = new Vector3(-340f, 850f, 0);
            settingBtn.gameObject.transform.localPosition = new Vector3(440f, 850f, 0);
            shopBtn.gameObject.transform.localPosition = new Vector3(290f, 850f, 0);
            homeBtn.gameObject.transform.localPosition = new Vector3(360f, 850f, 0);
        }
        else
        {
            // 1080*2340
            topBar.gameObject.transform.localPosition = new Vector3(-340f, 1020f, 0);
            settingBtn.gameObject.transform.localPosition = new Vector3(440f, 1020f, 0);
            shopBtn.gameObject.transform.localPosition = new Vector3(290f, 1020f, 0);
            homeBtn.gameObject.transform.localPosition = new Vector3(360f, 1020f, 0);
        }
    }

    private void PlayGame()
    {
        // ropeLoopObj.GetComponent<RopeLoopCtrl>().Flag = true;
    }

    private void OnEnable()
    {
        InitData();
        homeBtn.gameObject.SetActive(false);
    }


    private void InitData()
    {
        _curLevel = CattleLevelManager.Instance.GetCurLevel();
        _isOnPlay = false;
        ShowUI();
    }


    private void ShowUI()
    {
        // levelText.text = "Level " + _curLevel;
        coinText.text = "" + SaveDataManager.GetInt(CowConstant.CoinKey);
        // skillSpeedObj.gameObject.SetActive(false);
        // skillPowerObj.gameObject.SetActive(false);
        ropeLoopObj.gameObject.SetActive(false);
    }


    private void InitGame()
    {
        timeBar.GetComponent<TimeBar>().InitData();
    }

    private void CloseGame()
    {
        _isOnPlay = false;
        LairManager.Instance.ClearData();
    }


    private void TryToStartGame()
    {
        // if (GameDataManager.GetInstance().GetCoin() > 30)
        // {
        //     CowManager.Instance.ShowPropPop();
        // }
        // else
        // {
        StartGame();
        // }
    }


    private void StartGame()
    {
        if (_isOnPlay) return;

        startBtn.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(true);
        _isOnPlay = true;

        gameArea.gameObject.SetActive(true);
        playObj.gameObject.SetActive(true);

        shopBtn.gameObject.SetActive(false);
        settingBtn.gameObject.SetActive(false);

        ropeLoopObj.gameObject.SetActive(true);
        ropeLoopObj.gameObject.GetComponent<RopeLoopCtrl>().isPlaying = true;
        ropeLoopObj.gameObject.GetComponent<RopeLoopCtrl>().SetSkill(_isPower, _isSpeed);
        _isPower = false;
        _isSpeed = false;

        LairManager.Instance.InitData();
        timeBar.gameObject.SetActive(true);
        timeBar.GetComponent<TimeBar>().InitData();
        timeBar.GetComponent<TimeBar>().StartTimeBar();
    }


    private void ToNextLevel()
    {
        CattleLevelManager.Instance.GetNextLevel();
        InitData();
    }

    public void StopGame()
    {
        gameArea.gameObject.SetActive(false);
        playObj.gameObject.SetActive(false);
        timeBar.GetComponent<TimeBar>().StopTimeBar();
    }

    public void GoOnGame()
    {
        if (!gameArea.activeInHierarchy)
        {
            gameArea.gameObject.SetActive(true);
            playObj.gameObject.SetActive(true);
        }
    }


    public bool CheckIfShowComplete()
    {
        if (LairManager.Instance.cowList.Count > 0) return false;
        StopGame();
        CowManager.Instance.ShowFinishPop();
        return true;
    }

    public void ShowTimeStore()
    {
        StopGame();
        CowManager.Instance.ShowRestorePop();
    }


    public void FinishGame()
    {
        startBtn.gameObject.SetActive(true);
        shopBtn.gameObject.SetActive(true);
        settingBtn.gameObject.SetActive(true);
        gameArea.gameObject.SetActive(false);
        playObj.gameObject.SetActive(false);
        timeBar.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(false);
        _isOnPlay = false;
    }

    public async void ShowLosePanel()
    {
        _isOnPlay = false;
        await UniTask.Delay(1000);
        StopGame();
        CowManager.Instance.ShowLosePop();
    }

    public void AddTime()
    {
        timeBar.GetComponent<TimeBar>().AddTimeForTimeBar();
        GoOnGame();
    }

    public void AddTimeAct(Vector3 startPos)
    {
    }

    public void AfterCompletePanel(int num)
    {
        CattleLevelManager.Instance.GetNextLevel();
        AddCoinAndDoAnim(num, Vector3.zero);
        FinishGame();
    }


    private async void DoCoinAnim(int coinNum, Vector3 startPos)
    {
        int oldCoin = SaveDataManager.GetInt(CowConstant.CoinKey);
        await AnimationController.GoldMoveBest(coinImg, coinNum, startPos, coinImg.transform.position);
        AnimationController.ChangeNumber(oldCoin, oldCoin + coinNum, 0.01f, coinText, null);
        RopeUpMainManager.GetInstance().AddCoin(coinNum);
        coinText.text = "" + SaveDataManager.GetInt(CowConstant.CoinKey);
    }

    public void AddCoinAndDoAnim(int num, Vector3 startPos)
    {
        DoCoinAnim(num, startPos);
    }

    public void RemoveCattleItem(GameObject obj)
    {
        if (LairManager.Instance.cowList.Contains(obj))
        {
            LairManager.Instance.cowList.Remove(obj);
            LairManager.Instance.itemList.Remove(obj);
        }
    }


    public void SetSkill(RopeSkill skill)
    {
        switch (skill)
        {
            case RopeSkill.Speed:
                _isSpeed = true;
                // skillSpeedObj.gameObject.SetActive(true);
                break;
            case RopeSkill.Power:
                _isPower = true;
                // skillPowerObj.gameObject.SetActive(true);
                break;
        }

        coinText.text = "" + SaveDataManager.GetInt(CowConstant.CoinKey);
        ;
    }
}