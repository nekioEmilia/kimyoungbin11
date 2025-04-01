using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpForce = 8.0f;
    //private bool leftMove;
    //private bool rightMove;
    //private bool attack;
    public bool isGrounded; //바닥 체크 (바닥에 닿아있을 때 true)
    public int maxJumpCount = 2; //땅을 밟기 전까지 할 수 있는 최대 점프 횟수
    public int currentJumpCount = 1; // 현재 가능한 점프 횟수

    Vector2 moveVelocity;
    public Vector3 footPosition; //발의 위치
    public LayerMask groundLayer; // 바닥 체크를 위한 충돌 레이어
 

    SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid2D;
    CapsuleCollider2D capsuleCollider2D; // 오브젝트의 충돌 범위 컴포넌트

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rigid2D = GetComponent<Rigidbody2D>();

    }
    public void Start()
    {
        moveVelocity = rigid2D.velocity;
    }

    public void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 좌우 입력 값 (-1, 0, 1)

        if (horizontalInput != 0) // 좌우 이동 입력이 있을 때
        {
            anim.SetBool("Walk", true);
            rigid2D.velocity = new Vector2(horizontalInput * speed, rigid2D.velocity.y);
        }
        else // 입력이 없으면 멈춤
        {
            anim.SetBool("Walk", false);
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
        }

        //플레이어 점프 (C 누르면 점프)
        if (Input.GetKey(KeyCode.C))
        {
            Jump();
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.X))  // 아이템 줍기
    {
        Debug.Log("아이템 줍기");
    }
    
    if (Input.GetKeyDown(KeyCode.F))  // 상호작용
    {
        Debug.Log("상호작용");
    }

    if (Input.GetKeyDown(KeyCode.A))  // 스킬 1 사용
    {
        Debug.Log("스킬 1 사용");
    }

    if (Input.GetKeyDown(KeyCode.S))  // 스킬 2 사용
    {
        Debug.Log("스킬 2 사용");
    }

    if (Input.GetKeyDown(KeyCode.D))  // 스킬 3 사용
    {
        Debug.Log("스킬 3 사용");
    }

    if (Input.GetKeyDown(KeyCode.Q))  // 스킬 4 사용
    {
        Debug.Log("스킬 4 사용");
    }

    if (Input.GetKeyDown(KeyCode.W))  // 스킬 5 사용
    {
        Debug.Log("스킬 5 사용");
    }

    if (Input.GetKeyDown(KeyCode.E))  // 스킬 6 사용
    {
        Debug.Log("스킬 6 사용");
    }

    if (Input.GetKeyDown(KeyCode.Alpha1))  // 소모성 아이템 1번 사용
    {
        Debug.Log("소모성 아이템 1 사용");
    }

    if (Input.GetKeyDown(KeyCode.Alpha2))  // 소모성 아이템 2번 사용
    {
        Debug.Log("소모성 아이템 2 사용");
    }

    if (Input.GetKeyDown(KeyCode.Alpha3))  // 소모성 아이템 3번 사용
    {
        Debug.Log("소모성 아이템 3 사용");
    }
    }
    private void FixedUpdate()
    {
        // 플레이어 오브젝트의 collider2D min, center, max 위치 정보
        Bounds bounds = capsuleCollider2D.bounds;
        // 플레이어의 발 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        //플레이어의 발 위치에 원을 생성하고, 원이 바닥과 닿아있으면 isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.2f, groundLayer);

        // 플레이어의 발이 땅에 닿아 있고, y축 속도가 0이하라면 점프 횟수 초기화
        // velocity.y <= 0을 추가하지 않으면 점프키를 누르면 순간에도 초기화가 되어
        // 최대 점프 횟수를 2로 설정하면 3번까지 점프가 가능하게 된다
        if (isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }
        Debug.Log("isGrounded :" + isGrounded);
    }
    public void Jump()
    {
        // if (isGrounded == true)
        if (currentJumpCount > 0)
        {
            /*  //jumpForce의 크기만큼 윗쪽 방향으로 속력 설정
              rigid2D.velocity = Vector2.up * jumpForce;
              // jump횟수 1 감소
              currentJumpCount--;
            */
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0); // 현재 Y 속도를 0으로 초기화
            rigid2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // AddForce로 자연스럽게 점프
            currentJumpCount--;
        }
    }

    // X (아이템 줍기)
    // F (상호작용)
    // I (인벤토리)
    // M (미니맵)
    // K (스킬창)
    // J (스탯 정보창)
    // A, S, D, Q, W, E (순서대로 스킬 1,2,3,4,5,6)
    // 1, 2, 3 (순서대로 소모성 아이템 1,2,3번 칸)
}
