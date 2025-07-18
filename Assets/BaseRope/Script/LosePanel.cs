// Project  RopeUp
// FileName  LosePanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-20 09:06:18 
//


using System;
using UnityEngine.UI;

public class LosePanel: BaseUIForms
{

    public Button closeBtn;

    private void Start()
    {
        
        closeBtn.onClick.AddListener(() => { ClosePanel(); });
        
    }


    private void ClosePanel()
    {
        CattleGamePanel.Instance.FinishGame();
        gameObject.SetActive(false);
    }


}
