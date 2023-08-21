using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract를 통한 추상 클래스 사용
public abstract class Character : MonoBehaviour
{
    // 인스펙터창에 보여짐
    [SerializeField]
    protected float hp;
    public float speed;
    protected Vector2 direction;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // protected는 상속받은 클래스에서만 접근 가능
    // virtual을 통해서 상속 가능
    protected virtual void Update()
    {
        Move();
    }

    // 캐릭터 이동
    public void Move()
    {
        // direction 값 0f일 시에 멈춤
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    //파라미터 값에 따른 애니메이션 변환
    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    // 스피드 setter
    public void SetSpeed(float sp)
    {
        speed = sp;
    }

    // 스피드 getter
    public float GetSpeed()
    {
        return speed;
    }

    // 애니메이터 setter
    public void SetAnimator(bool b)
    {
        animator.enabled = b;
        animator.Play("idle_Up", 0);
    }
}