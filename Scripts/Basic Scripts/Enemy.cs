using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f; // 적 체력 설정 값
    private float currentHealth; // 현재 체력
    private bool isAggressive = false; // 현재 공격 상태인지 여부
    public float aggroDuration = 5f; // 공격 상태 지속 시간
    public float enemyAttackPower = 10f; // 적 공격력

    void Awake()
    {
        currentHealth = health; // 초기 체력 설정 // 현재 체력에 50f이란 값 할당
    }

    public void TakeDamage(float damage) // 데미지를 받는 함수 
    {
        currentHealth -= damage; // 현재 체력에서 매개변수 값 damage 만큼 빼고 할당 // currentHealth = currentHealth - damage
        Debug.Log(gameObject.name + "이" + damage + "의 피해를 입음. 현재 체력: " + currentHealth);
        Debug.Log("반격!!");

        isAggressive = true; // 공격 상태로 변..경?
        StopCoroutine(ResetAggro()); // 기존 코루틴 멈춤
        StartCoroutine(ResetAggro()); // 새 코루틴 실행 (5초 뒤 다시 순찰 모드)

        if (currentHealth <= 0) // 체력이 0 이하가 되면
        {
            Die(); // 적 사망
        }
    }
    IEnumerator ResetAggro()
    {
        yield return new WaitForSeconds(aggroDuration); // 5초 대기
        Debug.Log("시간 경과, 다시 순찰 모드로 변경");
        isAggressive = false; // 공격 상태 해제
    }

    private void Die()
    {
        Debug.Log(gameObject.name + "을 처치했습니다.");
        Destroy(gameObject); // 적 삭제
    }
}
