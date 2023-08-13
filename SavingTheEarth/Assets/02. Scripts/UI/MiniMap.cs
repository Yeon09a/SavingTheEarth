using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour, IPointerClickHandler
{
    public GameObject fullMap; // ÀüÃ¼ ¸Ê Ã¢

    public void OnPointerClick(PointerEventData eventData)
    {
        fullMap.SetActive(true);
    }
}
