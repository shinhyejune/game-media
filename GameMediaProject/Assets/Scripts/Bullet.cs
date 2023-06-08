using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    Vector2 dir;
    private bool isTarget = false;
    public void SetTarget(Transform target)
    {
        dir = target.position - transform.position;
        isTarget = true;
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if(isTarget)
            transform.Translate(dir.normalized * Time.deltaTime * bulletSpeed);
    }
}
