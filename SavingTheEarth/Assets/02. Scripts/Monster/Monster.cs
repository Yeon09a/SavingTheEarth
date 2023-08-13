using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObject/Monster")]
public class Monster : ScriptableObject
{
    public int monsterId;
    public float hp;
    public float[] damage;
}
