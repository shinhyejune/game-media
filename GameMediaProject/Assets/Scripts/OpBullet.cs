using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpBullet : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    public PlayerMove player;
    public Transform playerPos;
    public float bulletSpeed = 10f;
    Vector2 dir;

    private void OnEnable()
    {
        dir = playerPos.position - transform.position;

        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(dir.normalized * Time.deltaTime * bulletSpeed);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("aa");
    //    if (other.gameObject.name == "MainCharacter")
    //    {
    //        player._AnimState = PlayerMove.AnimState.Hit;
    //        player.CurrentAnimation = player.AnimClip[(int)PlayerMove.AnimState.Hit].name;
    //        player.skeletonAnimation.state.SetAnimation(0, player.AnimClip[(int)PlayerMove.AnimState.Hit], false);
    //        Destroy(gameObject);
    //    }
    //}


}
