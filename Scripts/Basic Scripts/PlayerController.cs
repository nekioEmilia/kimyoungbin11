using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;
    private Rigidbody2D rigid;
    [SerializeField]
    private float speed = 3.0f;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveVelocity = rigid.velocity;

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            moveVelocity.x = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            moveVelocity.x = speed;
        }
        else
        {
            moveVelocity.x *= 0.9f;
        }

        rigid.velocity = moveVelocity;

        //플레이어 점프 (스페이스 바를 누르면 점프)
        if (Input.GetKeyDown(KeyCode.C))
        {
            movement2D.Jump();
        }
    }
}
