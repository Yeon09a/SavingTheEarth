using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleDialog : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI dialogName;
    public TextMeshProUGUI dialogContent;
    public Image dialogToggle;
    public Transform selectPanel;
    public GameObject selectBtn;

    private int eventNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDialogName(string text)
    {
        dialogName.text = text;
    }

    public void SetDialogContent(int i, string text)
    {
        eventNum = i;
        dialogContent.text = text;
    }

    public void MakeSelect(string[] selects, TitleManager titleManager)
    {
        selectPanel.gameObject.SetActive(true);
        for(int i = 0; i < selects.Length; i++)
        {
            GameObject button = Instantiate(selectBtn, Vector2.zero, Quaternion.identity);
            button.transform.SetParent(selectPanel);
            button.GetComponentInChildren<TextMeshProUGUI>().text = selects[i];
            button.transform.tag = "Select" + i;
            button.GetComponent<Button>().onClick.AddListener(() => {titleManager.SetSelectResult(button.tag, DeleteSelect);});
        }
    }

    public void DeleteSelect()
    {
        int count = selectPanel.childCount;
        for(int i = 0; i < count; i++)
        {
            Destroy(selectPanel.GetChild(i).gameObject);
        }

        selectPanel.DetachChildren(); // 모든 자식들의 부모 해제
        selectPanel.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("TitleDialog"))
        {
            if(eventNum == 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
