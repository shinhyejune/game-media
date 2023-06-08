using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpBulletSpawn : MonoBehaviour
{

    //ÃÑ¾Ë »ý¼º
    public GameObject bulletPrefab;
    public float attackRate = 3f;
    public Transform target;
    private float timeAfterAttack;
    public bool isBoss = false;

    public Transform pos;

    // Start is called before the first frame update

    private void OnEnable()
    {
        timeAfterAttack = 0f;
        attackRate = Random.Range(5f, 10f);
        if (isBoss)
            attackRate = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterAttack += Time.deltaTime;

        if (timeAfterAttack >= attackRate)
        {
            timeAfterAttack = 0f;

            GameObject bullet = Instantiate(bulletPrefab, pos.position, pos.rotation);
            bullet.SetActive(true);
        }
    }
}
