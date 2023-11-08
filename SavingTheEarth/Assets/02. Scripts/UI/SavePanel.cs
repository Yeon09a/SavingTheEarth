using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.SceneManagement;

public class SavePanel : MonoBehaviour, IPointerUpHandler
{
    private string path;

    public bool isFile;
    public int slotNum;

    public GameObject saveInfoPanel;
    
    void Start()
    {
        path = Application.dataPath + "/09. Data/";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isFile)
        {
            DataManager.instance.LoadData();
            EnterGame();


        } else
        {
            saveInfoPanel.SetActive(true);
        }
    }

    public void EnterGame()
    {
        MapName nowMap = (MapName)DataManager.instance.nowPlayerData.placeNum;
        GameManager.instance.curMap = nowMap;
        GameManager.instance.preMap = MapName.SaveTitle;
        SceneManager.LoadScene(nowMap.ToString());
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }
}
