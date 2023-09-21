using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance = null; // 싱글턴 선언

    // 플레이어 관련 변수
    public string playerName; // 플레이어 이름
    
    // 아이템 관련 변수
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

    
}
