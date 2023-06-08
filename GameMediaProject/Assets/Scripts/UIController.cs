using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] controller;//���� ui(�̵�, ��ų, ���� ĳ���� hp)
    public GameObject[] smallOp; //���� ����(hp�� ����)
    public GameObject bossOP;//����(hp�� ����)

    public Transform firstStagePos; // ù ��° �� ������ ��ġ �̵���
    public Transform secondStagePos;// �� ��° ��(����) ������ ��ġ �̵���

    public GameObject mainCharacter;

    public PlayerMove player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))//���� ui on/off
        {
            for (int i = 0; i < controller.Length; i++)
            {
                controller[i].SetActive(!controller[i].activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))//���� ���� on/off
        {
            for(int i = 0; i < smallOp.Length; i ++)
            {
                smallOp[i].SetActive(!smallOp[i].activeSelf);
            }
            player.targetIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.F3))//���� on/off
        {
            bossOP.SetActive(!bossOP.activeSelf);
            player.targetIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.F4))//ù ��° �� ������ ��ġ �̵�
        {
            mainCharacter.transform.position = firstStagePos.position;
            for (int i = 0; i < smallOp.Length; i++)
            {
                smallOp[i].SetActive(!smallOp[i].activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.F5))//�� ��° ��(����) ������ ��ġ �̵�
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
