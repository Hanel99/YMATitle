using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{

    public GameObject timerObject;
    public Text timerText;

    bool isTimerAct = false;
    float leftTime = 0f;
    Action callbackAction = null;
    float setTimerTime = 0f;

    Color startColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    Color endColor = new Color32(0xE7, 0x32, 0x32, 0xFF);


    public void Init()
    {
        isTimerAct = false;
        leftTime = 0f;
        callbackAction = null;
        timerText.color = startColor;

        ShowTimer(false);
    }


    public void SetTimer(float time, Action callback = null)
    {
        setTimerTime = time;
        leftTime = time;
        callbackAction = callback;
        timerText.color = startColor;

        isTimerAct = true;
    }


    public void AddTimer(float time, Action callback = null)
    {
        leftTime += time;
        if (leftTime < 0) leftTime = 0;

        callbackAction = callback;

        isTimerAct = true;
    }

    public void ResetTimer()
    {
        leftTime = setTimerTime;
        timerText.text = $"{leftTime.ToString("F1")} 초";
        timerText.color = startColor;

        isTimerAct = false;
        callbackAction?.Invoke();
    }



    public void ShowTimer(bool show)
    {
        timerObject.SetActive(show);
    }



    void Update()
    {
        if (isTimerAct && leftTime >= 0)
        {
            leftTime -= Time.deltaTime;
            timerText.text = $"{leftTime:F1} 초";

            // 타이머가 종료에 가까워질수록 텍스트 색상이 빨간색으로 변함
            float t = Mathf.Clamp01(1 - (leftTime / setTimerTime));
            timerText.color = Color.Lerp(startColor, endColor, t);

            if (leftTime <= 0)
            {
                leftTime = 0f;
                isTimerAct = false;
                timerText.text = "종료!";
                timerText.color = endColor;
                callbackAction?.Invoke();
                SoundManager.Instance.PlaySound(SoundType.TimeOver);
            }
        }
    }
}
