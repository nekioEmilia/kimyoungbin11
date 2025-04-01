using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 8.0f;
    public Rigidbody2D rigid2D;

    [SerializeField]
    private LayerMask groundLayer; // 바닥 체크를 위한 충돌 레이어
    private CapsuleCollider2D capsuleCollider2D; // 오브젝트의 충돌 범위 컴포넌트
    private bool isGrounded; //바닥 체크 (바닥에 닿아있을 때 true)
    private Vector3 footPosition; //발의 위치

    [SerializeField]
    private int maxJumpCount = 2; //땅을 밟기 전까지 할 수 있는 최대 점프 횟수
    private int currentJumpCount = 0; // 현재 가능한 점프 횟수

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        // 플레이어 오브젝트의 collider2D min, center, max 위치 정보
        Bounds bounds = capsuleCollider2D.bounds;
        // 플레이어의 발 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        //플레이어의 발 위치에 원을 생성하고, 원이 바닥과 닿아있으면 isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        // 플레이어의 발이 땅에 닿아 있고, y축 속도가 0이하라면 점프 횟수 초기화
        // velocity.y <= 0을 추가하지 않으면 점프키를 누르면 순간에도 초기화가 되어
        // 최대 점프 횟수를 2로 설정하면 3번까지 점프가 가능하게 된다
        if (isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }
    }
    public void Jump()
    {
        // if (isGrounded == true)
        if (currentJumpCount > 0)
        {
            //jumpForce의 크기만큼 윗쪽 방향으로 속력 설정
            rigid2D.velocity = Vector2.up * jumpForce;
            // jump횟수 1 감소
            currentJumpCount--;
        }
    }
}
