using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Drop : MonoBehaviour, IDropHandler
{
    private Transform itemPanelTr; // 아이템 슬롯 transform
    private int type;
    private int num;

    void Start()
    {
        itemPanelTr = transform.GetChild(0); // 아이콘이 들어갈 부모 
        type = GetComponent<Slot>().type;
        num = GetComponent<Slot>().num;

    }

    public void OnDrop(PointerEventData eventData) // 드롭할 때
    {
        if (Drag.inDrag) // 드래그 하고 있을 때
        {
            int id = Drag.draggingIcon.GetComponent<ItemIcon>().itemInfo.id;
            int iconType = Drag.draggingIcon.GetComponent<ItemIcon>().itemInfo.type;
            
            if (itemPanelTr.childCount == 0 && (type == 0 || type == iconType)) // 아이콘이 들어갈 부모가 비어있을 경우(빈 슬롯일 경우) && 슬롯의 타입이 아이템 창이거나 슬롯의 타입과 아이콘의 타입이 같을 경우
            {
                DataManager.instance.nowPlayerData.haveItems[id].place = type;
                DataManager.instance.nowPlayerData.haveItems[id].slotNum = num;
                if(type == 0)
                {
                    InventoryManager.checkItemList[num] = true;
                } else if(type == 1)
                {
                    InventoryManager.checkItem[num] = true;
                } else if(type == 2)
                {
                    InventoryManager.checkPItem[num] = true;
                }
                Drag.draggingIcon.transform.SetParent(itemPanelTr);
                Drag.draggingIcon.GetComponent<Drag>().oriParentTr = itemPanelTr;
            }
        }
    }
}
