using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CurMap
{ // 맵 열거형
    BaseMap
}
public class Player : Character
{
    public delegate void BaseMapDel(); // 기본맵 관련 델리게이트
    public event BaseMapDel activateFace; // BaseMapFace 활성화 이벤트
    public event BaseMapDel deactivateFace; // BaseMapFace 비활성화 이벤트

    // override는 상속받은 클래스의 메소드 중에서 virtual로 선언된 부분을 재정의
    protected override void Update()
    {
        GetInput();
        // base는 상속받은 클래스의 기능을 가리킴
        base.Move();
    }

    // 키보드 입력값을 받음 (방향 및 멈춤 제어)
    public void GetInput()
    {
        Vector2 moveVector;

        // Input.GetAxisRaw(): 수평, 수직 버튼 입력시에 -1f, 1f 반환, 멈춰있을 때는 0f 반환
        moveVector.x = Input.GetAxisRaw("Horizontal"); // A키 or 왼쪽 화살표 -1f, D키 or 오른쪽 화살표 1f 값 반환
        moveVector.y = Input.GetAxisRaw("Vertical"); // S키 or 아래쪽 화살표 -1f, W키 or 위쪽 화살표 1f 값 반환

        direction = moveVector;
    }

    // 마우스 상호작용 추가하기
    // 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("BaseFace"))
        {
            activateFace();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("BaseFace"))
        {
            deactivateFace();
        }
    }
}