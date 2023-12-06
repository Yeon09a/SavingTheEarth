using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform canvasTr; // 인벤토리 transform

    private CanvasGroup canvasGroup; // 캔버스 그룹

    public Transform oriParentTr; // 원래 부모(슬롯) transform

    public static GameObject draggingIcon = null; // 드래그 하고 있는 아이콘
    
    public static bool inDrag = false; // 드래그 하고 있는지
    
    // Start is called before the first frame update
    void Start()
    {
        canvasTr = GameObject.FindWithTag("Canvas").transform;
        canvasGroup = GetComponent<CanvasGroup>();
        oriParentTr = this.transform.parent;
    }

    public void OnPointerDown(PointerEventData eventData) // 후에 플레이어 조작(마우스 조작에서 if문으로 적용할 예정)
    {
        inDrag = true;

    }

    public void OnBeginDrag(PointerEventData eventData) // 드래그 시작
    {
        if (inDrag) // 드래그 하고있을 때
        {
            this.transform.SetParent(canvasTr);
            draggingIcon = this.gameObject;

            canvasGroup.blocksRaycasts = false; 
        }
    }

    public void OnDrag(PointerEventData eventData) // 드래그 중
    {
        if (inDrag) // 드래그 하고있을 때
        {
            this.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData) // 드래그 완료
    {
        if (inDrag) // 드래그 하고있을 때
        {
            draggingIcon = null;
            canvasGroup.blocksRaycasts = true;

            if(this.transform.parent == canvasTr) // 현재 아이콘의 부모가 canvas일 때(아이콘이 빈 슬롯 위에 있지 않을 경우)
            {
                this.transform.SetParent(oriParentTr);
            }

        }
    }
}
