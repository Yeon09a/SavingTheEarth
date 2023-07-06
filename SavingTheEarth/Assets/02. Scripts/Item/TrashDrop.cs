using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.inDrag) // 드래그 하고 있을 때
        {
            Destroy(Drag.draggingIcon);
        }
    }
}
