using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDic", menuName = "ScriptableObject/MonsterDic")]
public class MonsterDic : ScriptableObject
{
    [SerializeField]
    public Monster[] monsters;
}
