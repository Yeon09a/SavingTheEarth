using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBfishMonster : MonoBehaviour
{
    int HP = 1;

    Animator animator;
    Rigidbody2D rig;
    SpriteRenderer sr;

    float move = -1;

    enum State
    {
        Idle,
        MoveLeft,
        MoveRight,
        Chase
    }

    State state;
    Player player;

    bool cancelWait;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cancelWait = false;
        animator = GetComponent<Animator>();

        while (HP > 0)
        {
            yield return StartCoroutine(state.ToString());
        }
    }

    void StateChange(State next)
    {
        state = next;
        cancelWait = true;

        /*if (state == State.Chase)
        {
            animator.SetBool("IsCatch", true);
        }
        else
        {
            animator.SetBool("IsCatch", false);
        }*/
    }

    void SetNextRoaming()
    {
        // 현재의 state 가 Idle, MoveLeft, MoveRight 이면 다음 로밍 설정
        if (state == State.Idle || state == State.MoveLeft || state == State.MoveRight)
        {
            state = (State)Random.Range(0, 3);
        }
    }

    IEnumerator CancelableWait(float t)
    {
        var d = Time.deltaTime;
        cancelWait = false;

        // 지정한 시간 t 만큼 대기
        // cancelWait == true 면 중단
        while (d < t && cancelWait == false)
        {
            d += Time.deltaTime;
            yield return null;
        }

        if (cancelWait == true) print("Cancled");
    }

    IEnumerator Idle()
    {
        // 움직이지 않음
        move = 0;
        // 1~2 초 대기
        yield return StartCoroutine(CancelableWait((Random.Range(1f, 3f))));

        SetNextRoaming();
    }

    IEnumerator MoveLeft()
    {
        move = -1;
        sr.flipX = false;
        yield return StartCoroutine(CancelableWait((Random.Range(3f, 6f))));

        SetNextRoaming();
    }

    IEnumerator MoveRight()
    {
        move = 1;
        sr.flipX = true;
        yield return StartCoroutine(CancelableWait((Random.Range(3f, 6f))));

        SetNextRoaming();
    }

    IEnumerator Chase()
    {
        if (player != null)
        {
            Vector3 vec;

            do
            {
                yield return new WaitForSeconds(0.5f);

                // Player와의 방향 벡터
                vec = player.transform.position - transform.position;

                // Player 가 오른쪽에 있으면
                if (vec.x > 0)
                {
                    sr.flipX = true;
                    move = 2f;
                }
                // Player 가 왼쪽에 있으면
                else
                {
                    sr.flipX = false;
                    move = -2f;
                }
            }
            // 거리가 6 이내인 동안 따라 다니기
            while (vec.magnitude < 6f);

            print("End of chase");

            // Player 가 멀어지면 State 를 Idle 로 변경
            StateChange(State.Idle);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move 값에 따라 x 축으로 이동
        rig.velocity = new Vector2(move, rig.velocity.y);
    }

    // Sensor 객체의 CircleCollider2D와 충돌시 발생하는 이벤트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체의 이름이 Player 가 아니면 return
        if (collision.gameObject.name != "Player") return;
        player = collision.gameObject.GetComponent<Player>();
        if (state != State.Chase) StateChange(State.Chase);
    }
}