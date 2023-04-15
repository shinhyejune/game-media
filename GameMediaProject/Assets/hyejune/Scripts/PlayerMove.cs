using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    RectTransform image;
    [SerializeField]
    Transform player;

    [SerializeField]
    float textPosition;

    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //jump
        if (Input.GetButtonUp("Jump"))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }


        //button up speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.normalized.x * 0.5f, rigidbody.velocity.y);
        }

        //direction flip
        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //30분56초 이동 애니메이션 ->walk, idle 관리 추가하기


        if(player != null && image != null)
        {
            image.position = new Vector3(player.position.x, player.position.y + textPosition, player.position.z);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigidbody.velocity.x > maxSpeed)//right move
        {
            rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);
        }
        else if(rigidbody.velocity.x < maxSpeed * (-1))//left move
        {
            rigidbody.velocity = new Vector2(maxSpeed * (-1), rigidbody.velocity.y);
        }
    }
}
