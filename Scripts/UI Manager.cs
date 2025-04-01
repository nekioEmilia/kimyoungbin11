using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))  // 인벤토리 열기
        {
            Debug.Log("인벤토리 열기");
        }

        if (Input.GetKeyDown(KeyCode.M))  // 미니맵 열기
        {
            Debug.Log("미니맵 열기");
        }

        if (Input.GetKeyDown(KeyCode.K))  // 스킬창 열기
        {
            Debug.Log("스킬창 열기");
        }

        if (Input.GetKeyDown(KeyCode.J))  // 스탯 정보창 열기
        {
            Debug.Log("스탯 정보창 열기");
        }
    }
}
