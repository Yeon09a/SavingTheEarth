using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; // 시간 text

    private int curTime; // 현재 시간(데이터에서 가져오기) // 임시(게임시각 오전 9시)
    private int uiMinutes; // UI에 표시된 분
    private int uiHours; // UI에 표시될 시간
    private string ampm; // 오전 오후 표시

    private int standTime = 300; // 기준 시간(초단위 / 게임에서 30분이 현실 5분)
    private int gameStandTime = 30; // 게임 시간(분단위)

   

    private void Awake()
    {
        curTime = 540;// 어떻게 설정할 것인지는 개발하면서 
        uiMinutes = 0;
        uiHours = 9;
        ampm = " a.m.";
    }
    void Start()
    {
        StartCoroutine(setTime());
    }

    void Update()
    {
        
    }

    private void calTime()
    {
        uiHours = curTime / 60;
        uiMinutes = curTime % 60;
        if(uiHours == 13 && uiMinutes == 0)
        {
            uiHours = 1;
            curTime = 60;
            if (ampm.Equals(" a.m."))
            {
                ampm = " p.m.";
            } else if (ampm.Equals(" p.m."))
            {
                ampm = " a.m.";
            }
        }
    }
    IEnumerator setTime()
    {
        while (true)
        {
            calTime();
            timeText.text = uiHours.ToString() + " : " + uiMinutes.ToString("D2") + ampm;
            yield return new WaitForSeconds(standTime);
            curTime += gameStandTime;
        }
    }
}
