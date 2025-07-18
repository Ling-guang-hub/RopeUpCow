// Project  RopeUp
// FileName  CattleLevelManager.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 18:06:47 
//


using System.Collections.Generic;
using UnityEngine;

public class CattleLevelManager : MonoBehaviour
{
    
    public static CattleLevelManager Instance;
    
    public int curLevel;

    public int maxTime = 120;

    public int lessTime = 60;

    public List<CattleType> cattleTypes;
    

    private  void Awake()
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

        
        cattleTypes = new List<CattleType>();
    }

    public int GetLevelTime()
    {
        if (curLevel > 60) return 60;
        return maxTime - SaveDataManager.GetInt(RopeConstant.CattleLevel) / 10 * 10;
        // return 30;
    }


    public void InitData()
    {
        curLevel = SaveDataManager.GetInt(RopeConstant.CattleLevel);
        ResetLevelType();
    }

    public int GetCurLevel()
    {
        curLevel = SaveDataManager.GetInt(RopeConstant.CattleLevel);
        return curLevel;
    }


    public void GetNextLevel()
    {
        AddLevel();
    }


    private void AddLevel()
    {
        SaveDataManager.SetInt(RopeConstant.CattleLevel, SaveDataManager.GetInt(RopeConstant.CattleLevel) + 1);
        curLevel = SaveDataManager.GetInt(RopeConstant.CattleLevel);
        ResetLevelType();
    }

    private void ResetLevelType()
    {
        curLevel = SaveDataManager.GetInt(RopeConstant.CattleLevel);
        cattleTypes = new List<CattleType>();
        if (curLevel >= 0)
        {
            cattleTypes.Add(CattleType.Yellow);
        }

        if (curLevel >= 4)
        {
            cattleTypes.Add(CattleType.Black);
        }

        if (curLevel >= 7)
        {
            cattleTypes.Add(CattleType.Stone);
        }

        if (curLevel >= 9)
        {
            cattleTypes.Add(CattleType.Box);
        }
    }

    private CattleType GetRandomCattleType()
    {
        int idx = Random.Range(0, cattleTypes.Count);
        return cattleTypes[idx];
    }

    private int GetCattleNum()
    {
        return curLevel switch
        {
            < 4 => 5,
            < 8 => 6,
            < 10 => 8,
            _ => 9
        };
    }

    public List<CattleType> GetCattleList()
    {
        int stoneNum = 0;
        int boxNum = 0;

        int itemNum = GetCattleNum();

        List<CattleType> list = new List<CattleType>();
        while (list.Count < itemNum)
        {
            CattleType type = GetRandomCattleType();
            switch (type)
            {
                case CattleType.Stone when stoneNum < 3:
                    stoneNum++;
                    list.Add(type);
                    break;
                case CattleType.Box when boxNum < 2:
                    boxNum++;
                    list.Add(type);
                    break;
                case CattleType.Yellow:
                case CattleType.Black:
                    list.Add(type);
                    break;
            }
        }

        return list;
    }
}