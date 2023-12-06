using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashDrop : MonoBehaviour, IDropHandler
{
    public InventoryManager invMng;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.inDrag) // 드래그 하고 있을 때
        {
            int id = Drag.draggingIcon.GetComponent<ItemIcon>().itemInfo.id;
            invMng.UseItem(id, DataManager.instance.nowPlayerData.haveItems[id].count);

            Destroy(Drag.draggingIcon);
        }
    }
}
