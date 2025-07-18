using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  </summary>
public class A_SettingPanel : MonoBehaviour
{
    public GameObject onObj;
    public GameObject offObj;
    public Button musicBtn;
    public Button closeBtn;
    public Button continueBtn;

    private void Start()
    {
        Application.targetFrameRate = 60;

        int music = PlayerPrefs.GetInt("Music", 1);
        offObj.gameObject.SetActive(music != 1);
        onObj.gameObject.SetActive(music == 1);
        
        
        closeBtn.onClick.AddListener(() => { ClosePop(); });

        continueBtn.onClick.AddListener(() => { ClosePop(); });
        
        musicBtn.onClick.AddListener(() => { Music(); });
    }

    public void Music()
    {
        int music = PlayerPrefs.GetInt("Music", 1);
        music = music == 1 ? 0 : 1;
        PlayerPrefs.SetInt("Music", music);
        offObj.gameObject.SetActive(music != 1);
        onObj.gameObject.SetActive(music == 1);
        A_AudioManager.Instance.ToggleMusic();
    }

    public void ClosePop()
    {
        gameObject.SetActive(false);
    }
}