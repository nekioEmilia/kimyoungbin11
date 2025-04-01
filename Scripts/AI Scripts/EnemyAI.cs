using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] private int nextMove; // 다음 움직임 할 때 필요한 변수
    [SerializeField] private float detectionRange = 5f; // 탐지 범위
    [SerializeField] private float attackRange = 1f; // 공격 범위
    [SerializeField] private float attackCooldown = 1f; // 공격 쿨타임..?
    [SerializeField] private float aggroDuration = 5f; // 공격받고 유지되는 시간
    [SerializeField] private Collider2D attackHitbox; // 공격 히트박스

    private Enemy enemyATK; // Enemy 클래스 타입의 변수 enemyATK
    private Transform player; // Transform 클래스 타입의 변수 player
    private bool canAttack = true; // 공격할 수 있는지 확인하는 변수
    private bool isAggressive = false; // 적이 플레이어를 감지했는지 여부
    private SpriteRenderer spriteRenderer; // SpriteRenderer 클래스 타입의 변수 spriteRenderer


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // spriteRenderer 변수에 SpriteRenderer 컴포넌트 할당
        enemyATK = GetComponent<Enemy>(); // enemyATK 변수에 Enemy 컴포넌트 할당
        attackHitbox = GetComponentInChildren<Collider2D>(); // attackHitbox 변수에 Collider2D 컴포넌트 할당
        rigid = GetComponent<Rigidbody2D>(); // rigid 변수에 Rigidbody2D 컴포넌트 할당
        player = GameObject.FindGameObjectWithTag("Player").transform; // "Player" 태그가 붙은 게임 오브젝트를 찾아서, 그 오브젝트의 transform을 player 변수에 저장

        if (player != null) // player 변수에 값이 들어왔다면
        {
           Debug.Log("플레이어 찾기 성공"); // 출력
        }

        if (attackHitbox == null) // attackHitbox 변수에 값이 null이라면
        {
           Debug.LogError("공격 히트박스를 찾을 수 없습니다."); // 출력
        }
        Invoke("Think", 3); // Think 함수를 3초후에 실행`
    }

    void Update()
    {
        if (player != null) // player 변수에 값이 들어 있다면
        {
            if (Vector2.Distance(transform.position, player.position) <= detectionRange) // 이 스크립트가 부착된 게임 오브젝트의 위치 (a)와 player의 위치 (b)
                                                                                         // 사이의 거리를 계산하며, 탐지범위(5f)보다 작거나 같으면 true
            {
                isAggressive = true; // false에서 true로 바꿈
                TrackingPlayer(); // 함수호출
            }
            else
            {
                // 위 조건문이 충족되지 않으면 실행
                // 감지 범위 밖에 있으면 순찰
                if (isAggressive != true) // 변수 값이 true가 아니라면
                {
                    Patrol(); // 함수호출
                }
            }
        }
       
    }
    void TrackingPlayer() // 플레이어 따라가는 함수
    {
        float direction = (player.position.x > transform.position.x) ? 1 : -1; // player의 x좌표가 transform의 x좌표보다 크면 1 할당, 아니면 -1 할당

        rigid.velocity = new Vector2(direction * 2, rigid.velocity.y); // direction에 저장된 값에 *2 해주고 위 아래 이동은 유지한 값을 할당
        nextMove = (int)direction; // 플레이어 쪽으로 이동
        Debug.Log("플레이어 발견! 추적 중!");

       /* // ▶ 방향 전환 (플레이어 기준으로 좌우 반전) //오류 수정 필
        if (direction == 1)
            spriteRenderer.flipX = false; // 오른쪽 보기
        else
            spriteRenderer.flipX = true; // 왼쪽 보기
       */

        CheckGround(); // 땅이 없으면 방향 변경

        Debug.DrawRay(transform.position, new Vector2(direction * 2, 0), Color.red, 0.5f); // 적 위치에서 direction * 2만큼 x축 방향으로 빨간색 레이를 0.5초 동안 그림

        if (Vector2.Distance(transform.position, player.position) <= attackRange && canAttack) // 적의 현재 위치 (transform.position)와
                                                                                               // 플레이어 위치 (player.position)사이의 거리를 계산하고
                                                                                               // 이 거리가 attackRange 이하이며, canAttack이 true일 경우 Attack 코루틴을 실행
        {
            StartCoroutine(Attack());
        }
    }

    void Patrol() // 돌아다니는 함수
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y); // nextMove에 할당된 값으로 속력 재설정 (Think함수 참조)
      // Debug.Log("순찰 중");
        CheckGround();
    }


    void CheckGround() // 낭떠러지 확인하는 함수
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y); // 현재 적의 x좌표에 조금 앞쪽의 좌표(nextMove * 0.2f)
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // frontVec에서 아래 방향 (Vector3.down)으로 광선을 그림 색상: (0,1,0) -> 초록색

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        // frontVec (Ray를 발사할 시작 위치), Vector3.down (Ray를 아래 방향으로 발사), 1 (Ray의 길이(거리), LayerMask.GetMask("Ground") -> "Ground"레이어만 감지
        // 감지한 값을 rayHit에 할당

        if (rayHit.collider == null) // rayHit에 할당된 값이 null이라면 (감지가 되지 않았다면("Ground"레이어가))
        {
          //  Debug.Log("어이쿠 절벽이네");
            nextMove *= -1; // 방향을 반대쪽으로 
        }
    }

    IEnumerator Attack() // 공격함수
    {
        canAttack = false; // 공격 중에는 다시 공격 못하게 설정
        attackHitbox.enabled = true; // 공격 범위 활성화 // 플레이어가 이 히트박스에 닿으면 데미지를 입게 됨
        Debug.Log("공격!"); 

        Debug.DrawRay(transform.position, Vector2.right * attackRange, Color.blue, 0.5f); // Vector2.right * attackRange(오른쪽 공격 범위 표시)
        Debug.DrawRay(transform.position, Vector2.left * attackRange, Color.blue, 0.5f); // Vector2.left * attackRange(왼쪽 공격 범위 표시)

        yield return new WaitForSeconds(0.2f); // 0.2초 동안 기다림 (공격 판정 유지)
        attackHitbox.enabled = false; // 공격 범위 비활성화 (0.2초 동안 공격 판정이 있고, 그 후에는 사라짐)

        yield return new WaitForSeconds(attackCooldown); // 공격 후 쿨타임 적용 
        canAttack = true; // 다시 공격하게 설정  
    }

    void Think()
    {
        if (isAggressive != true)
        {
            nextMove = Random.Range(-1, 2); // nextMove에 랜덤 값 -1, 0 ,1 중 하나 할당
        //    Debug.Log("생각중...");
        }
        float nextThinkTime = Random.Range(2f, 5f); // 다음 생각 시간 랜덤 2f, 3f, 4f 중 하나 할당
        Invoke("Think", nextThinkTime); // 일정 시간 후 다시 Think 실행
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌한 순간 // 수정해야함
    {
        if (collision.CompareTag("Player")) // 충돌한 객체의 태그가 "Player"라면
        {
            Attack player = collision.GetComponent<Attack>(); // collistion 객체에서 Enemy 컴포넌트를 찾고
                                                           // enemy라는 Enemy 타입의 변수에 할당
            {
                player.TakeDamage(enemyATK.enemyAttackPower);
                Debug.Log("플레이어에게 데미지를 입힘!");
            }
        }
    }
}
