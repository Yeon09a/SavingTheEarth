using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public string itName; // 아이템 이름
    public int id; // 아이템 id(배열 인덱스일 예정)
    public string info; // 아이템 정보
    public Texture image; // 아이템 이미지
    public int type; // 아이템 종류
}
