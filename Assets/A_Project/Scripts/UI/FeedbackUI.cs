using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackUI : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject PlayUI;
    public TMP_Text TimeText;
    public TMP_Text CountText;
    public TMP_Text ReasonText;

    public void OnClick()
    {
        StartUI.SetActive(false);
        PlayUI.SetActive(true);
        ReadTimeLog();
        ReadCountLog();
        ReadReasonLog();
    }

    void ReadTimeLog()
    {
        string allText = "";
        foreach (string item in GameManager.timeLog)
        {
            allText += item + "\n"; // 각 항목 뒤에 줄 바꿈 문자를 추가합니다.
        }
        TimeText.text = allText;
    }
    void ReadCountLog()
    {
        string allText3 = GameManager.CountLog.ToString();
        CountText.text = allText3;
    }
    void ReadReasonLog()
    {
        string allText2 = "";
        foreach (string item in GameManager.WhyLog)
        {
            allText2 += item + "\n"; // 각 항목 뒤에 줄 바꿈 문자를 추가합니다.
        }
        ReasonText.text = allText2;
    }
}
