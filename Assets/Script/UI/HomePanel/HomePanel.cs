// Project  RopeUp
// FileName  HomePanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 15:06:55 
//


using System;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : BaseUIForms
{
    
    public Button startBtn;


    private void Start()
    {
        startBtn.onClick.AddListener(() => { StartGame(); });
    }


    private void StartGame()
    {
        OpenUIForm("CattleGamePanel");
        // OpenUIForm("GamePanel");
        CloseUIForm(GetType().Name);
    }
}