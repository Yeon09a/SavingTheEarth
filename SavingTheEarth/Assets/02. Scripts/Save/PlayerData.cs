using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour // 플레이어 데이터 클래스
{
    [NonSerialized]
    public static PlayerData instance = null; // 싱글턴 선언

    // 플레이어 관련 변수
    public string playerName; // 플레이어 이름

    // 아이템 관련 변수
    [NonSerialized]
    public Dictionary<int, HaveItemInfo> haveItems; // 현재 가지고 있는 아이템 딕셔너리<아이템 id, 아이템 정보>

    private void Awake()
    {
        // 싱글턴
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public PlayerData(Dictionary<int, HaveItemInfo> haveItems, string playerName) // 후에 플레이어 변수 추가시 매개변수 추가할 예정
    {
        this.haveItems = haveItems;
        this.playerName = playerName;
    }
}
