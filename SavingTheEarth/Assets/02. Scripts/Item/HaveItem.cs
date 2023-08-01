using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HaveItem", menuName = "ScriptableObject/HaveItem")]
public class HaveItem : ScriptableObject
{
    [SerializeField]
    public Dictionary<int, HaveItemInfo> haveItems; // 현재 가지고 있는 아이템 딕셔너리<아이템 id, 아이템 정보>
}

[System.Serializable]
public class HaveItemInfo : ScriptableObject
{
    public int type; // 아이템 위치
    public int count; // 아이템 개수
    public int slotNum; // 아이템이 들어가있는 슬롯 인덱스

    public HaveItemInfo(int type, int count, int slotNum) // 생성자
    {
        this.type = type; // 분류(0(아이템 창), 1(소지품), 2(중요물품))
        this.count = count;
        this.slotNum = slotNum;
    }
}
