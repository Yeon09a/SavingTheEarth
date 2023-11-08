using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    public static DataManager instance = null; // 싱글턴 선언

    private string path;
    private string fileName = "PlayerSaveData";


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

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/09. Data/";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData(PlayerData playerData) // 저장하기
    {
        string data = PlayerJsonUtility.ToJson(playerData);
        File.WriteAllText(path + fileName, data);
    }

    public PlayerData LoadData() // 불러오기
    {
        string loadData = File.ReadAllText(path + fileName);
        PlayerData playerData = PlayerJsonUtility.FromJson(loadData);

        return playerData;
    }
}
