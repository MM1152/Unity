using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody;
    private Vector3 change;
    private Animator animator;
    public PlayerStatus currenState;
    void Start()
    {
        currenState = PlayerStatus.walk;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currenState != PlayerStatus.attack)
        {
            StartCoroutine(AttackCo());
        }
        if(currenState == PlayerStatus.walk)
        {
            UpdateAnimationMove();
        }
    }

    private IEnumerator AttackCo() // 공격 모션 구현
    {
        animator.SetBool("attacking", true);
        currenState = PlayerStatus.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        currenState = PlayerStatus.walk;
    }

    void UpdateAnimationMove() // 블러드트리 내 moveX moveY 값 입력을 통해 캐릭터가 바라볼 방향을 구현
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    void MoveCharacter() // 직접적인 캐릭터 이동
    {
        change.Normalize();
        rigidbody.MovePosition(
                transform.position + change * speed * Time.deltaTime
        ); 
    }
}
