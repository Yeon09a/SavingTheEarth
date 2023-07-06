using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // 인벤토리 창
    public GameObject itemList; // 아이템 창
    public Button inventoryBtn; // 인벤토리(가방) 버튼
    public Toggle itemTgl; // 소지품 토글
    public GameObject itemPanel; // 소지품 판넬
    public GameObject pItemPanel; // 중요 물품 판넬

    private bool isInvenOpen = false; // 인벤토리 상태

    
    // Start is called before the first frame update
    void Start()
    {
        // UI 리스너 연결
        inventoryBtn.onClick.AddListener(SetInventory);
        itemTgl.onValueChanged.AddListener(OnItemPanel);
        inventoryBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(532, 40);
        itemList.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 40);
    }

    private void SetInventory() // 인벤토리 열기
    {
        if (isInvenOpen) // 인벤토리가 열려있을 때
        {
            inventoryPanel.SetActive(false);
            isInvenOpen = false;
        }
        else // 인벤토리가 닫혀있을 때
        {
            inventoryPanel.SetActive(true);
            isInvenOpen = true;
        }
    }

    private void OnItemPanel(bool itemTglOn) // 소지품, 중요물품 판넬 바꾸기
    {
        if (itemTglOn) // 소지품 토글 체크 시
        {
            itemPanel.SetActive(true);
            pItemPanel.SetActive(false);
        }
        else // 소지품 토글 미체크 시
        {
            itemPanel.SetActive(false);
            pItemPanel.SetActive(true);
        }
    }

}
