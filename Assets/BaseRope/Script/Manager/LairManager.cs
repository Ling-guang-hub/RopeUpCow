// Project  RopeUp
// FileName  LairPanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-18 15:06:17 
//


using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LairManager : MonoBehaviour
{
    
    public static LairManager Instance;
    
    public GameObject baseYellowCattle;
    public GameObject baseBlackCattle;
    public GameObject baseBoxCattle;
    public GameObject baseStone;

    public GameObject cattleArea;
    
    public List<GameObject> cowList;

    public List<GameObject> itemList;


    public Dictionary<CattleType, GameObject> _baseDic;


    public float areaWidth = 700f;
    public float areaHeight = 800f;

    private void Awake()
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
        
        
        _baseDic = new Dictionary<CattleType, GameObject>
        {
            { CattleType.Yellow, baseYellowCattle },
            { CattleType.Black, baseBlackCattle },
            { CattleType.Stone, baseStone },
            { CattleType.Box, baseBoxCattle }
        };
    }

    private void Start()
    {
        // InitData();
    }

    public void ClearData()
    {
        cowList = new List<GameObject>();
        itemList = new List<GameObject>();
        int childCount = cattleArea.transform.childCount;
        if (childCount > 0)
        {
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(cattleArea.transform.GetChild(i).gameObject);
            }
        }
    }


    public void InitData()
    {
        ClearData();

        List<CattleType> cattleList = CattleLevelManager.Instance.GetCattleList();

        foreach (var t in cattleList)
        {
            GameObject baseObj = _baseDic[t];
            GameObject cowItem = Instantiate(baseObj, cattleArea.transform);
            Vector3 pos = new Vector3(Random.Range(-areaWidth / 2, areaWidth / 2),
                Random.Range(0, areaHeight), 0);
            cowItem.transform.localPosition = pos;
            if (50 > Random.Range(0, 100))
            {
                cowItem.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            cowItem.gameObject.SetActive(true);
            cowItem.GetComponent<BaseCattle>().InitData();
            cowItem.GetComponent<BaseCattle>().BeCatch(true);
            
            itemList.Add(cowItem);
            if (cowItem.GetComponent<BaseCattle>().cattleType == CattleType.Yellow ||
                cowItem.GetComponent<BaseCattle>().cattleType == CattleType.Black)
            {
                cowList.Add(cowItem);
            }
        }
    }
}