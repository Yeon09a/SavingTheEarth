using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiddleOctMonster : MonoBehaviour
{
    public Monster octMon;
    
    public GameObject[] beams;
    public GameObject[] bubbles;

    public GameObject leg;
    public GameObject head;

    /*private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f) };
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f) };*/

    public Animator legAnimator;


    public static bool isAttaking; // 공격 중인지 확인

    // 빔 개수
    private int maxBeam = 4; // 임시
    private int maxBubble = 6; // 임시

    public Action startBeamAnimation;
    public Action startBubbleMove;

    // 공격 번호
    private int attackNum; // 1, 2

    private int attackCount = 0;

    public float speed;

    [SerializeField]
    private UIManager uiMng;

    // Start is called before the first frame update
    void Start()
    {
       // StartAttack();
        uiMng = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

    }

    IEnumerator Attack() // 공격 함수
    {
        while (true)
        {
            attackCount++;

            if(attackCount < 4)
            {
                attackNum = Random.Range(1, 3);
                //attackNum = 2;
                if (attackNum == 1)
                {
                    ShootBeam();
                }
                else if (attackNum == 2)
                {
                    ShootBubble();
                }

                yield return new WaitForSeconds(10.0f);
            } else
            {
                StartCoroutine(MovePartsPos(leg.transform, head.transform));

                Debug.Log("이동 완료");

                yield return new WaitForSeconds(10.0f);

                StartCoroutine(MovePartsPos(head.transform, leg.transform));
                Debug.Log("돌아가기");
                attackCount = 0;
            }
        }
    }

    private void ShootBeam() // 빔 발사 함수
    {
        Debug.Log("먹물 기둥");
        int j = Random.Range(0, 4);
        startBeamAnimation = null;
        for (int i = 0; i < maxBeam; i++)
        {
            if (i != j)
            {
                beams[i].SetActive(true);
            }
        }
        startBeamAnimation();
    }

    private void ShootBubble()
    {
        Debug.Log("먹물 방울");
        legAnimator.SetTrigger("LegAttack");
        for (int i = 0; i < maxBubble; i++)
        {
            bubbles[i].SetActive(true);
        }

        startBubbleMove();
    }



    public void StartAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator MovePartsPos(Transform tf1, Transform tf2) // tf1 : 뒤로 가야 하는 애, tf2 : 앞으로 가야하는 애
    {
        while(tf1.localPosition.x < 12.0f && tf2.localPosition.x > 0.0000001f) {
            tf1.Translate(Vector3.right);
            tf2.Translate(-1 * Vector3.right);

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Debug.Log("공격 받음");
        }
    }
}
