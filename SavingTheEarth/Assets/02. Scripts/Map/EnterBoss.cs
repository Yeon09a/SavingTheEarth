using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoss : MonoBehaviour
{
    public GameObject dontExitBoss;
    public MiddleOctMonster octMon;

    public bool isMove = false;
    public float speed = 1.5f;
    public float zoomSpeed = 1f;

    [SerializeField]
    private UIManager uiMng;

    // Start is called before the first frame update
    void Start()
    {
        uiMng = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        {
            Camera.main.transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (Camera.main.orthographicSize <= 6.1f)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize + Time.deltaTime * zoomSpeed;
            }
            if (Camera.main.transform.position.x >= 159f)
            {
                isMove = false;
                
                uiMng.OpenBossPanel("대왕 문어", "- 머리를 공격해서 물리쳐라 -", octMon.StartAttack);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            
            Camera.main.transform.SetParent(null);

            isMove = true;

            dontExitBoss.SetActive(true);
        }
        
        
    }
}
