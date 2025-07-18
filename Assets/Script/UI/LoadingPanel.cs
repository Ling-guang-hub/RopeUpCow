// Project  RopeUp
// FileName  LoadingPanel.cs
// Author  AX
// Desc
// CreateAt  2025-06-18 14:06:14 
//


using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    public Image sliderImage;

    public Text progressText;

    // Start is called before the first frame update
    void Start()
    {
        sliderImage.fillAmount = 0;
        progressText.text = "0%";
        Application.targetFrameRate = 60;

        // float width = Screen.width;
        // float height = Screen.height;
        // LocalCommonData.ScreenRate = width / height;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliderImage.fillAmount <= 0.8f || NetInfoMgr.instance.ready)
        {
            sliderImage.fillAmount += Time.deltaTime / 3f;
            progressText.text = (int)(sliderImage.fillAmount * 100) + "%";
            if (sliderImage.fillAmount >= 1)
            {
                // PostEventScript.GetInstance().SendEvent("1001");
                MainManager.instance.GameInit();

                Destroy(transform.parent.gameObject);
                UIManager.GetInstance().ShowUIForms("CattleHomePanel");
            }
        }
    }
}