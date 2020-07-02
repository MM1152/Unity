using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody;
    private Vector3 change;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        UpdateAnimationMove();
    }
    void UpdateAnimationMove()
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
    void MoveCharacter()
    {
        rigidbody.MovePosition(
                transform.position + change * speed * Time.deltaTime
        ); 
    }
}
