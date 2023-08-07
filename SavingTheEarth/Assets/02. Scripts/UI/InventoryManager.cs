using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] itemListSlots; // 하단 아이템 창 슬롯 배열
    public GameObject[] itemSlots; // 소지품 슬롯 배열
    public GameObject[] pItemSlots; // 중요 물품 슬롯 배열

    private bool[] checkItemList; // 아이템 창 슬롯이 비어있는지 체크
    private bool[] checkItem; // 소지품 슬롯이 비어있는지 체크
    private bool[] checkPItem; // 중요 물품 슬롯이 비어있는지 체크

    public GameObject itemIcon; // 아이템 아이콘 프리팹

    public HaveItem haveItems; // 가지고 있는 아이템 에셋
    public ItemDic items; // 아이템 종류 에셋

    private void Awake()
    {
        // 나중에 저장 데이터 필요
        checkItemList = new bool[5];
        checkItem = new bool[15];
        checkPItem = new bool[15];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int CheckHaveItem(int id) // 아이템을 현재 가지고 있는지 확인
    {
        if (haveItems.haveItems.ContainsKey(id)) // 아이템을 가지고 있는 경우
        {
            return haveItems.haveItems[id].type;
        }
        else // 가지고 있지 않은 경우
        {
            return -1;
        }
    }

    private int CheckItemList() // 아이템 창의 빈 슬롯 찾기
    {
        for (int i = 0; i < checkItemList.Length; i++)
        {
            if(checkItemList[i] == false) // 빈 슬롯 발견 시
            {
                return i;
            }
        }

        return -1;
    }

    private int CheckItemSlots() // 소지품 창의 빈 슬롯 찾기
    {
        for (int i = 0; i < checkItem.Length; i++) 
        {
            if (checkItem[i] == false) // 빈 슬롯 발견 시
            {
                return i;
            }
        }

        return -1;
    }

    private int CheckPItemSlots() // 중요물품 창의 빈 슬롯 찾기
    {
        for (int i = 0; i < checkPItem.Length; i++)
        {
            if (checkPItem[i] == false) // 빈 슬롯 발견 시
            {
                return i;
            }
        }

        return -1;
    }

    private bool PutNewItem(int id) // 새로 얻은 아이템 획득
    {
        int lIndex = CheckItemList();

        if (lIndex == -1) // 아이템 창에 빈 슬롯이 없는 경우
        {
            if (items.items[id].type == 1) // 아이템 종류가 소지품
            {
                int iIndex = CheckItemSlots();
                if (iIndex != -1) // 소지품 창에 빈 슬롯이 있는 경우
                {
                    CreateItemIcon(itemSlots, checkItem, id, iIndex, 1);
                    return true;
                }
                else // 빈 슬롯이 없는 경우
                {
                    return false;
                }
            }
            else // 아이템 종류가 중요 물품
            {
                int pIndex = CheckPItemSlots();
                if (pIndex != -1) // 중요 물품 창에 빈 슬롯이 있는 경우
                {
                    CreateItemIcon(pItemSlots, checkPItem, id, pIndex, 2);
                    return true;
                }
                else // 빈 슬롯이 없는 경우
                {
                    return false;
                }
            }
        }
        else // 아이템 창에 빈 슬롯이 있는 경우
        {
            // 아이템 리스트에 아이템 넣음
            CreateItemIcon(itemListSlots, checkItemList, id, lIndex, 0);
            return true;
        }
    }

    public void CreateItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int type) // 아이템 아이콘 생성
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon 속성 설정(itemicon으로 이동)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id];
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        checkSlots[index] = true ;
        haveItems.haveItems.Add(id, new HaveItemInfo(type, 1, index));
    }

    public void PutHaveItem(int id, GameObject[] slots) // 기존에 가지고 있던 아이템 추가
    {
        HaveItemInfo haveItemInfo = haveItems.haveItems[id];
        int slotNum = haveItemInfo.slotNum;
        haveItemInfo.count += 1;
        slots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
    }

    private bool PutItem(int id) // 아이템 획득
    {
        int type = CheckHaveItem(id);
        if(type == -1) // 새로 얻은 경우
        {
            return PutNewItem(id);
        } 
        else if(type == 0) // 해당 아이템이 아이템 창에 있는 경우
        {
            PutHaveItem(id, itemListSlots);
            return true;
        } 
        else if(type == 1) // 소지품 창에 있는 경우
        {
            PutHaveItem(id, itemSlots);
            return true;
        } 
        else if(type == 2) // 중요물품 창에 있는 경우
        {
            PutHaveItem(id, pItemSlots);
            return true;
        }
        else
        {
            return false;
        }
    }
}
