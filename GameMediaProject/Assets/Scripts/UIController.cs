using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] controller;//조작 ui(이동, 스킬, 메인 캐릭터 hp)
    public GameObject[] smallOp; //작은 적들(hp바 포함)
    public GameObject bossOP;//보스(hp바 포함)

    public Transform firstStagePos; // 첫 번째 적 만나는 위치 이동용
    public Transform secondStagePos;// 두 번째 적(보스) 만나는 위치 이동용

    public GameObject mainCharacter;

    public PlayerMove player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))//조작 ui on/off
        {
            for (int i = 0; i < controller.Length; i++)
            {
                controller[i].SetActive(!controller[i].activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))//작은 적들 on/off
        {
            for(int i = 0; i < smallOp.Length; i ++)
            {
                smallOp[i].SetActive(!smallOp[i].activeSelf);
            }
            player.targetIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.F3))//보스 on/off
        {
            bossOP.SetActive(!bossOP.activeSelf);
            player.targetIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.F4))//첫 번째 적 만나는 위치 이동
        {
            mainCharacter.transform.position = firstStagePos.position;
            for (int i = 0; i < smallOp.Length; i++)
            {
                smallOp[i].SetActive(!smallOp[i].activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.F5))//두 번째 적(보스) 만나는 위치 이동
        {
            player.targetIndex = 3;
            mainCharacter.transform.position = secondStagePos.position;
            for (int i = 0; i < controller.Length; i++)
            {
                controller[i].SetActive(!controller[i].activeSelf);
            }
        }
    }
}
