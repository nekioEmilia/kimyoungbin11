using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private float healthPoint;
    [SerializeField] private float ManaPoint;
    [SerializeField] private float attackPower;
    [SerializeField] private float magic;
    [SerializeField] private float defensivePower;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalStrike;
    [SerializeField] private float criticalDamage;
    [SerializeField] private float AGI;
    [SerializeField] private float LUK;
    [SerializeField] private float coolDown;

    private float healthGrowthRate; 
    private float manaGrowthRate; 
    private float attackGrowthRate; 
    private float magicGrowthRate;

    private int currentLevel = 1;
    private int currentExp = 0;
    private int expToLevelUp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentExp += 100;
        }
        CheckLevelUp();
    }

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
}
