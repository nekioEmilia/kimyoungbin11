using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    /* 세부 내용은 게임개발부 카페 참고 */
    [Header("스탯")]
    public float healthPoint; // HP (체력)
    public float ManaPoint; // MP (마나)
    public float attackPower; //ATK (공격력)
    public float magic; // Magic (마력)
    public float defensivePower; // DEF (방어력)
    public float attackSpeed; // AS (공격 속도)
    public float criticalStrike; // CRIT (크리티컬 확률)
    public float criticalDamage; // CRIT DMG (크리티컬 데미지)
    public float AGI; // 민첩 (공격 속도 및 이동 속도 증가)
    public float LUK; // 행운 (치명타 확률 및 아이템 드랍률 증가
    public float coolDown; // 쿨타임 감소 (스킬 잿가용 대기시간 단축)

    private float healthGrowthRate; // 성장 체력
    private float manaGrowthRate;  // 성장 마나
    private float attackGrowthRate; // 성장 공격력
    private float magicGrowthRate; // 성장 마력

    private int currentLevel = 1; // 현재 레벨 = 1
    private int currentExp = 0; // 현재 경험치 = 0
    private int expToLevelUp; // 경험치에 따라 레벨업 검사하는 변수

    private void CheckLevelUp()
    {
        while (currentLevel < 100 && currentExp >= expToLevelUp) // 레벨이 100보다 낮고 경험치 요구량보다 
        {
            LevelUP(); //레벨업 함수 호출
        }
    }

    private void LevelUP()
    {
        currentExp -= expToLevelUp; //경험치 차감
        currentLevel++; //레벨 업
        Debug.Log($"레벨업! 현재 레벨 : {currentLevel}");
    }

    //레벨 업 시 스탯 업
}
