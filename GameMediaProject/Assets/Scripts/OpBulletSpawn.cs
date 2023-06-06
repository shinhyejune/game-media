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

    // Start is called before the first frame update
    void Start()
    {
        timeAfterAttack = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterAttack += Time.deltaTime;

        if (timeAfterAttack >= attackRate)
        {
            timeAfterAttack = 0f;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.SetActive(true);
        }
    }
}
