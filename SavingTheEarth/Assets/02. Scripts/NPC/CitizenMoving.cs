using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenMoving : MonoBehaviour
{
    [Tooltip("NPCMove를 체크하면 NPC가 움직임")]
    public bool NPCMove;
    public string[] direction; // npc가 움직일 방향 저장
    [Range(1, 5)]
    [Tooltip("1 = very slow, 2 = slow, 3 = normal, 4 = fast, 5 = continual")]
    public int frequency; // npc 움직임 속도 조절

    protected Animator anim;

    protected Vector3 vector; // 벡터
    public float speed; // 속도

    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(MoveCoroutine());
    }
    void Update()
    {

    }

    public void Moving(string dir)
    {
        StartCoroutine(MovingCoroutine(dir));
    }

    public IEnumerator MovingCoroutine(string dir)
    {
        vector.Set(0, 0, 0);

        switch (dir)
        {
            case "UP":
                vector.y = 1;
                break;
            case "DOWN":
                vector.y = -1;
                break;
            case "LEFT":
                vector.x = -1;
                break;
            case "RIGHT":
                vector.x = 1;
                break;
        }
        // 애니메이션
        anim.SetFloat("DirX", vector.x);
        anim.SetFloat("DirY", vector.y);
        anim.SetBool("Walking", true);

        // 이동
        transform.Translate(vector.x * speed, vector.y * speed, 0);
        yield return new WaitForSeconds(0.01f);

        anim.SetBool("Walking", false);
    }

    public void SetMove()
    {

    }
    public void SetNotMove()
    {

    }

    IEnumerator MoveCoroutine()
    {
        if (direction.Length != 0)
        {
            for (int i = 0; i < direction.Length; i++)
            {
                switch (frequency) // 이동 대기를 걸어서 움직임 속도 조절
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;
                }

                // 실질적인 이동
                Moving(direction[i]);

                if (i == direction.Length - 1) // i 초기화를 통한 무한 반복
                    i = -1;
            }
        }
    }
}
