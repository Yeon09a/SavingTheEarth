using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDic", menuName = "ScriptableObject/ItemDic")]
public class ItemDic : ScriptableObject
{
    [SerializeField]
    public Item[] items;
}
