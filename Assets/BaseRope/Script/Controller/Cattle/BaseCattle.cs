// Project  RopeUp
// FileName  BaseCattle.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 13:06:47 
//


using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseCattle : MonoBehaviour
{
    public CattleType cattleType;

    public int RewardNum;

    public float actWeight;

    public float actHeight;

    protected Vector3 LastPos;

    public float duringTime;

    public int roadStep;

    public Vector3 createPos;

    public float offsetRange;

    public float shakeTime;

    public GameObject bg1;

    public GameObject bg2;

    public virtual void InitData()
    {
    }

    public virtual void BeCatch(bool isNew)
    {
        bg1.gameObject.SetActive(isNew);
        bg2.gameObject.SetActive(!isNew);
    }


    private KeyValuePair<Vector3, float> GetRandomPosAndTime()
    {
        LastPos = transform.localPosition;
        int tempX = Random.Range(-roadStep, roadStep);
        float targetX = LastPos.x + tempX;
        if (Math.Abs(targetX) >= actWeight / 2)
        {
            targetX = LastPos.x - tempX;
        }

        int tempY = Random.Range(-roadStep, roadStep);
        float targetY = LastPos.y + tempY;
        if (Math.Abs(targetY) >= actHeight / 2)
        {
            targetY = LastPos.y - tempY;
        }

        Vector3 nextPos = new Vector3(targetX, targetY, 0);

        double targetWay = Math.Sqrt(Math.Pow(tempX, 2) + Math.Pow(tempY, 2));
        float time = (float)targetWay / roadStep * duringTime;
        return new KeyValuePair<Vector3, float>(nextPos, time);
    }


    private void OnDestroy()
    {
        if (IsInvoking("DoMove"))
        {
            CancelInvoke("DoMove");
        }

        DOTween.Kill(transform);
    }


    public void StartDoMove()
    {
        Invoke("DoMove", 0.1f);
    }


    private void DoMove()
    {
        float delayTime = Random.Range(0, 2);
        // KeyValuePair<Vector3, float> pair = GetRandomPosAndTime();
        // transform.DOLocalMove(pair.Key, pair.Value).SetEase(Ease.Linear).SetDelay(delayTime)
        //     .OnComplete(() => { DoMove(); });

        Vector3 newPos = new Vector3(createPos.x + Random.Range(-offsetRange, offsetRange),
            createPos.y + Random.Range(-offsetRange, offsetRange), 0);

        transform
            .DOLocalMove(newPos, shakeTime).SetDelay(delayTime)
            .SetEase(Ease.Linear).SetLoops(1, LoopType.Yoyo).OnComplete(
                () => { DoMove(); });
    }
}