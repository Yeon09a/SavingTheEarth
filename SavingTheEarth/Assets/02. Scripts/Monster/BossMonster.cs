using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public GameObject beam;
    private GameObject[] beams;
    private int beamCount = 0;
    private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f)};
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f)};

    public static bool isAttaking; // 공격 중인지 확인

    public delegate void beamDel();
    public static event beamDel startBeamAnimation;

    // 공격 관련 변수
    private int maxBeam = 8; // 빔 개수(임시)
    private int attackNum; // 공격 번호(1, 2, 3)
    private int attackCount = 0; // 공격 횟수 
    private bool isEyeOpen = false; // 눈
    //private Player player;

    // 몬스터 정보
    public Monster bossMonster;
    //private float mHP; // character 상속받아 사용.

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();

        beams = new GameObject[maxBeam]; // 임시

        //ShootBeam();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Attack()
    {
        while (true)
        {
            attackNum = Random.Range(1, 3);
            if (attackNum == 1)
            {
                ShootBeam();
            }
            else if (attackNum == 2)
            {
                Paralysis();
            }

            attackCount++;

            if (attackCount < 5)
            {
                yield return new WaitForSeconds(6.0f);
            } else { // 공격 5번 쿨타임
                yield return new WaitForSeconds(30.0f);
            }
        }
    }

    private void ShootBeam() // 빔 공격
    {
        Debug.Log("공격1");
        for (int i = 0; i < maxBeam; i++)
        {
            beams[i] = Instantiate(beam, setBeamPos(i), Quaternion.identity); // 위치는 후에 맵에 맞춰서 수정
        }

        startBeamAnimation();
    }

    private void Paralysis() // 마비 공격
    {
        isEyeOpen = true;
        /*if() // 스프라이트가 눈을 바라보던가 아님 변수 따로 설정
         * 움직임 고정(후에 player 스크립트에 bool 형 변수 넣어서 움직임 제어)
         * 마비는 4초정도?
         */

        Debug.Log("공격2");
    }

    private void MakeShield()
    {
        //  현재 체력 < max체력 / 2.0f 일 경우 몬스터 소환
        // 실드 작용. 공격을 받을 수 있는지에 대한 bool 체크 사용. bool 변함
    }

    // 빔 위치 설정 함수
    private Vector2 setBeamPos(int i)
    {
        Vector2 pos = new Vector2(0f, 0f);

        if(i % 4 == 0)
        {
            pos.x = Random.Range(boundarysX[0].x, boundarysX[0].y);
        }
        else if (i % 4 == 1)
        {
            pos.x = Random.Range(boundarysX[1].x, boundarysX[1].y);
        } 
        else if (i % 4 == 2)
        {
            pos.x = Random.Range(boundarysX[2].x, boundarysX[2].y); 
        }
        else if (i % 4 == 3)
        {
            pos.x = Random.Range(boundarysX[3].x, boundarysX[3].y);
        }

        if(i < 4)
        {
            pos.y = Random.Range(boundarysY[0].x, boundarysY[0].y);
        } 
        else {
            pos.y = Random.Range(boundarysY[1].x, boundarysY[1].y);
        }


        return pos;
    }

}
