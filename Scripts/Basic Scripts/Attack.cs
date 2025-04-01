using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float playerATK; // 플레이어 공격력
    private float attackDelay = 0.5f; // 공격 딜레이 = 0.5초
    private bool canAttack = true; // 공격이 가능한가를 체크하는 변수
    private Ability ability; // .

    Collider2D attackHitbox; // 콜라이더2D 형 attackHitbox변수로 히트박스 체크
    Animator anim; // 애니메이션 변수

    private void Awake()
    {
        attackHitbox = GetComponentInChildren<Collider2D>(); 
        ability = GetComponent<Ability>();
        anim = GetComponent<Animator>();

        if (ability != null) // 어빌리티 변수에 값이 null이 아니라면
        {
            playerATK = ability.attackPower; // playerATK변수에 Ablilty스크립트에서 attackPower변수 가져오기
        }

        attackHitbox.enabled = false; // 히트박스 활성화 X
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canAttack) // Z키를 누르고 canAttack이 true일때
        {
            StartCoroutine(MeleeAttack()); // MeleeAttack코루틴 실행
        }

        if (Input.GetKeyDown(KeyCode.Z)) // 애니메이션 코드
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    IEnumerator MeleeAttack() // 근접공격 코루틴
    {
        canAttack = false; // 공격 중에는 다시 공격 못하게 설정
        attackHitbox.enabled = true; // 공격 범위 활성화

        yield return new WaitForSeconds(0.2f); // 공격 범위 유지 시간 0.2초
        attackHitbox.enabled = false; // 공격 범위 비활성화

        yield return new WaitForSeconds(attackDelay); // 공격 딜레이 적용 / 0.5초
        canAttack = true; // 다시 공격할 수 있게 해줌
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌한 순간
    {
        if (collision.CompareTag("Enemy")) // 충돌한 객체의 태그가 "Enemy"라면
        {
            Enemy enemy = collision.GetComponent<Enemy>(); // collistion 객체에서 Enemy 컴포넌트를 찾고
                                                           // enemy라는 Enemy 타입의 변수에 할당
            {
                enemy.TakeDamage(playerATK);
            }
        }
    }

    public void TakeDamage(float enemyDamage)
    {
        ability.healthPoint -= enemyDamage; // 체력 감소
        Debug.Log(gameObject.name + "이" + enemyDamage + "의 피해를 입음. 현재 체력: " + ability.healthPoint);

        if (ability.healthPoint <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("사망..");
        Destroy(gameObject);
    }
}
