using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcMonster : MonoBehaviour
{
    private BossMonster bossMonster;
    
    private void Start()
    {
        bossMonster = GameObject.FindGameObjectWithTag("BossMonster").GetComponent<BossMonster>();
    }

    
    
    private void OnDestroy()
    {
        bossMonster.CalEtcMonsCount();
    }
}
