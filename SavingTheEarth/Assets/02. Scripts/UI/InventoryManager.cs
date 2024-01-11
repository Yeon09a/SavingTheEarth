using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HaveItemInfo
{
    public int place; // 아이템 위치
    public int count; // 아이템 개수
    public int slotNum; // 아이템이 들어가있는 슬롯 인덱스

    public HaveItemInfo(int place, int count, int slotNum) // 생성자
    {
        this.place = place; // 분류(0(아이템 창), 1(소지품), 2(중요물품))
        this.count = count;
        this.slotNum = slotNum;
    }
}

public class InventoryManager : MonoBehaviour
{
    public GameObject[] itemListSlots; // 화면 하단의 퀵슬롯(아이템 창) 슬롯 배열
    public GameObject[] itemSlots; // 인벤토리의 소지품 슬롯 배열
    public GameObject[] pItemSlots; // 인벤토리의 중요 물품 슬롯 배열
    public GameObject[] bItemSlots; // 상자 슬롯 배열

    public static bool[] checkItemList; // 퀵슬롯(아이템 창) 슬롯이 비어있는지 체크하기 위한 배열
    public static bool[] checkItem; // 인벤토리의 소지품 슬롯이 비어있는지 체크하기 위한 배열
    public static bool[] checkPItem; // 인벤토리의 중요 물품 슬롯이 비어있는지 체크하기 위한 배열
    public static bool[] checkBItem; // 상자 슬롯이 비어있는지 체크하기 위한 배열

    public GameObject itemIcon; // 아이템 아이콘 프리팹

    public ItemDic items; // 모든 아이템 데이터를 가지고 있는 ScriptableObject(아이템 id에 따른 아이템 정보가 배열로 존재)

    public SlotClick[] slots; // 슬롯 배열
    private SlotClick selectedSlot = null; // 선택된 슬롯

    void Update()
    {
        // 키보드 입력을 통해 슬롯 선택
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // 변경된 부분: 슬롯 번호를 SlotClick 객체로 변환
                SlotClick clickedSlot = slots[i];
                HandleSlotSelection(clickedSlot);
            }
        }
    }

    public void HandleSlotSelection(SlotClick clickedSlot)
    {
        bool isSelected = clickedSlot.IsSelected();

        if (isSelected)
        {
            // 이미 선택된 슬롯을 다시 선택한 경우
            clickedSlot.Deselect();
            selectedSlot = null; // 선택된 슬롯 업데이트
        }
        else
        {
            // 새로운 슬롯을 선택한 경우
            if (selectedSlot != null)
            {
                // 이전에 선택한 슬롯이 있으면 해당 슬롯을 선택 해제
                selectedSlot.Deselect();
            }

            clickedSlot.ToggleSelection();
            selectedSlot = clickedSlot; // 선택된 슬롯 업데이트

            // EventSystem을 사용하여 현재 선택된 게임 오브젝트 변경
            EventSystem.current.SetSelectedGameObject(clickedSlot.gameObject);
        }
    }

    public void SelectSlot(SlotClick clickedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.ToggleSelection(); // 이전에 선택한 슬롯 선택 해제
        }

        selectedSlot = clickedSlot; // 새로운 슬롯 선택
        selectedSlot.ToggleSelection();
    }

    private void Awake()
    {
        checkItemList = new bool[5];
        checkItem = new bool[15];
        checkPItem = new bool[15];
        checkBItem = new bool[25];
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateInventory();
    }



    public void UpdateInventory()
    {
        // slots 배열 초기화
        //slots = GetComponentsInChildren<SlotClick>();

        // 선택된 슬롯 초기화
        selectedSlot = null;

        // 소지한 아이템 불러오기
        foreach (KeyValuePair<int, HaveItemInfo> item in DataManager.instance.nowPlayerData.haveItems) // 소지한 아이템 딕셔너리 가져오기
        {
            if (item.Value.place == 0) // 소지한 아이템의 위치가 퀵슬롯인 경우
            {
                // 슬롯에 넣을 아이템 아이콘 생성
                LoadItemIcon(itemListSlots, checkItemList, item.Key, item.Value.slotNum, item.Value.count);
            }
            else if (item.Value.place == 1) // 소지한 아이템의 위치가 인벤토리 소지품인 경우
            {
                LoadItemIcon(itemSlots, checkItem, item.Key, item.Value.slotNum, item.Value.count);
            }
            else if (item.Value.place == 2) // 소지한 아이템의 위치가 인벤토리 중요물품인 경우
            {
                LoadItemIcon(pItemSlots, checkPItem, item.Key, item.Value.slotNum, item.Value.count);
            }
            else // 소지한 아이템의 위치가 상자인 경우
            {
                LoadItemIcon(bItemSlots, checkBItem, item.Key, item.Value.slotNum, item.Value.count);
            }
        }
    }

    private void LoadItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int count) // 아이템 아이콘 불러오기
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity); // 아이콘 생성
        // icon 속성 설정(itemicon으로 이동)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id]; // 아이템 정보 설정
        icon.GetComponent<ItemIcon>().SetItemImage(); // 아이템 이미지 설정
        icon.transform.SetParent(slots[index].transform.GetChild(0)); // 아이템을 슬롯에 넣음(아이템 아이콘의 부모 오브젝트를 슬롯으로 설정함)
        icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); // 아이템 아이콘 크기 설정
        checkSlots[index] = true; // 해당 슬롯에 아이템이 있다고 표시

        icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = count.ToString(); // 아이템 아이콘에 아이템 개수 표시
    }

    private int CheckHaveItem(int id) // 아이템을 현재 가지고 있는지 확인
    {
        if (DataManager.instance.nowPlayerData.haveItems.ContainsKey(id)) // 아이템을 가지고 있는 경우
        {
            return DataManager.instance.nowPlayerData.haveItems[id].place;
        }
        else // 가지고 있지 않은 경우
        {
            return -1;
        }
    }

    private int CheckItemSlot(bool[] checkSlots) // 빈 슬롯 찾기
    {
        for (int i = 0; i < checkSlots.Length; i++)
        {
            if (checkSlots[i] == false) // 빈 슬롯 발견 시
            {
                return i;
            }
        }

        return -1;
    }


    private bool PutNewItem(int id, int count, int wantPlace) // 새로 얻은 아이템 획득
    {
        if(wantPlace == 0)
        {
            int slotNum = CheckItemSlot(checkItemList);
            if(slotNum != -1)
            {
                CreateItemIcon(itemListSlots, checkItemList, id, slotNum, 0, count);
                return true;
            }
        }

        int type = items.items[id].type;
        
        if(wantPlace == 1 || type == 1)
        {
            int slotNum = CheckItemSlot(checkItem);
            if (slotNum != -1) // 소지품 창에 빈 슬롯이 있는 경우
            {
                CreateItemIcon(itemSlots, checkItem, id, slotNum, 1, count);
                return true;
            }
            else // 빈 슬롯이 없는 경우
            {
                return false;
            }
        }
        else if(wantPlace == 2 || type == 2)
        {
            int slotNum = CheckItemSlot(checkPItem);
            if (slotNum != -1) // 중요 물품 창에 빈 슬롯이 있는 경우
            {
                CreateItemIcon(pItemSlots, checkPItem, id, slotNum, 2, count);
                return true;
            }
            else // 빈 슬롯이 없는 경우
            {
                return false;
            }
        } 
        else if(wantPlace == 3 || type == 3)
        {
            int slotNum = CheckItemSlot(checkBItem);
            if (slotNum != -1) // 중요 물품 창에 빈 슬롯이 있는 경우
            {
                CreateItemIcon(bItemSlots, checkBItem, id, slotNum, 3, count);
                return true;
            }
            else // 빈 슬롯이 없는 경우
            {
                return false;
            }
        } else
        {
            return false;
        }
        
    }

    public void CreateItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int place, int count) // 아이템 아이콘 생성
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon 속성 설정(itemicon으로 이동)
        ItemIcon iIcon = icon.GetComponent<ItemIcon>();
        iIcon.itemInfo = items.items[id];
        iIcon.SetItemImage();
        icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = count.ToString();
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        checkSlots[index] = true;
        DataManager.instance.nowPlayerData.haveItems.Add(id, new HaveItemInfo(place, count, index));
    }

    public void PutHaveItem(int id, GameObject[] slots, int count) // 기존에 가지고 있던 아이템 추가
    {
        HaveItemInfo haveItemInfo = DataManager.instance.nowPlayerData.haveItems[id];
        int slotNum = haveItemInfo.slotNum;
        haveItemInfo.count += count;
        slots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
    }



    public bool PutItem(int id, int count, int wantPlace) // 아이템 획득
    {
        int place = CheckHaveItem(id); // 얻은 아이템이 기존에 가지고 있는지 확인한다.
        if (place == -1) // 새로 얻은 경우
        {
            return PutNewItem(id, count, wantPlace); // 새로 인벤토리에 아이템을 추가한다.
        }
        else if (place == 0) // 해당 아이템이 아이템창(퀵슬롯)에 있는 경우
        {
            PutHaveItem(id, itemListSlots, count); // 기존에 가지고 있는 아이템의 개수를 추가한다.
            return true;
        }
        else if (place == 1) // 소지품 창에 있는 경우
        {
            PutHaveItem(id, itemSlots, count); // 기존에 가지고 있는 아이템의 개수를 추가한다.
            return true;
        }
        else if (place == 2) // 중요물품 창에 있는 경우
        {
            PutHaveItem(id, pItemSlots, count); // 기존에 가지고 있는 아이템의 개수를 추가한다.
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UseItem(int id, int count) // 아이템 사용
    {
        HaveItemInfo haveItemInfo = DataManager.instance.nowPlayerData.haveItems[id]; // 사용할 아이템의 소지 정보 가져오기
        int slotNum = haveItemInfo.slotNum; // 아이템의 슬롯 번호 가져오기
        int ItemType = haveItemInfo.place; // 아이템의 분류 위치 가져오기
        int iCount = haveItemInfo.count;

        if (iCount == count) // 아이템을 사용했을 때 개수가 0개가 남을 경우
        {
            if (ItemType == 0) // 해당 아이템이 퀵슬롯에 있는 경우
            {
                DelItem(id, slotNum, itemListSlots, checkItemList); // 퀵슬롯에서 아이템 아이콘 제
            }
            else if (ItemType == 1) // 소지품 창에 있는 경우
            {
                DelItem(id, slotNum, itemSlots, checkItem);
            }
            else if (ItemType == 2) // 중요물품 창에 있는 경우
            {
                DelItem(id, slotNum, pItemSlots, checkPItem);
            }
        }
        else if (iCount > count) // 아이템 사용 후 개수가 남았을 경우
        {
            haveItemInfo.count -= count;

            if (ItemType == 0) // 해당 아이템이 퀵슬롯에 있는 경우
            {
                itemListSlots[slotNum].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString(); // 아이템 개수 UI 업데이트
            }
            else if (ItemType == 1) // 소지품 창에 있는 경우
            {
                itemSlots[slotNum].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
            }
            else if (ItemType == 2) // 중요물품 창에 있는 경우
            {
                pItemSlots[slotNum].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
            }
        }
    }

    public void DelItem(int id, int slotNum, GameObject[] slots, bool[] checkSlots) // 아이템 삭제
    {
        Destroy(slots[slotNum].transform.GetChild(0).GetChild(0).gameObject);
        checkSlots[slotNum] = false;
        DataManager.instance.nowPlayerData.haveItems.Remove(id);
    }
}
