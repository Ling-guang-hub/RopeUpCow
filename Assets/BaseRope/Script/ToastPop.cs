// Project  RopeUp
// FileName  ToastPop.cs
// Author  AX
// Desc
// CreateAt  2025-06-30 18:06:26 
//


using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToastPop : MonoBehaviour
{
    public Text toastText;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowToast(string msg)
    {
        toastText.text = msg;
        StartCoroutine(nameof(autoCloseToast));
    }


    private IEnumerator autoCloseToast()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}