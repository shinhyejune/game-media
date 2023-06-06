using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

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

    private int state;

    //UI 버튼
    public Image leftBtn;
    public Image rightBtn;
    public Image jumpBtn;


    //스파인 애니메이션
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    public enum AnimState
    {
        Idle,Walk,Jump
    }

    //현재 애니메이션 처리가 무엇인지에 대한 변수
    private AnimState _AnimState;

    //현재 어떤 애니메이션이 재생되고 있는지에 대한 변수
    private string CurrentAnimation;

    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //jump
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            state = 1;
            _AnimState = AnimState.Jump;
            CurrentAnimation = AnimClip[(int)AnimState.Jump].name;
            skeletonAnimation.state.SetAnimation(0, AnimClip[(int)AnimState.Jump], false);
            jumpBtn.color = Color.gray;
            StartCoroutine(WaitForButton());
        }


        //button up speed
        if (Input.GetButtonUp("Horizontal"))
        {
            state = 0;
            rigidbody.velocity = new Vector2(rigidbody.velocity.normalized.x * 0.5f, rigidbody.velocity.y);
        }
        if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            state = 0;
            if (_AnimState != AnimState.Walk)
            {
                _AnimState = AnimState.Walk;
                SetCurrentAnimation(_AnimState);
            }
        }

        //direction flip
        //if(Input.GetButtonDown("Horizontal"))
        //{
        //    if (this.transform.rotation.y == 0)
        //        this.transform.rotation = new Quaternion(this.transform.rotation.x, 180f, this.transform.rotation.z, this.transform.rotation.w);
        //    else
        //        this.transform.rotation = new Quaternion(this.transform.rotation.x, 0f, this.transform.rotation.z, this.transform.rotation.w);
        //}

        //30분56초 이동 애니메이션 ->walk, idle 관리 추가하기

        


        if(player != null && image != null)
        {
            image.position = new Vector3(player.position.x, player.position.y + textPosition, player.position.z);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(state == 0)
        {
            if (h == 0f)
                _AnimState = AnimState.Idle;
            else
            {
                _AnimState = AnimState.Walk;
            }
        }

        SetCurrentAnimation(_AnimState);

        rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigidbody.velocity.x > maxSpeed)//right move
        {
            leftBtn.color = Color.white;
            rightBtn.color = Color.gray;
            rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);
            if (this.transform.rotation.y != 0)
            {
                this.transform.rotation = new Quaternion(this.transform.rotation.x, 0f, this.transform.rotation.z, this.transform.rotation.w);
            }
        }
        else if(rigidbody.velocity.x < maxSpeed * (-1))//left move
        {
            leftBtn.color = Color.gray;
            rightBtn.color = Color.white;
            rigidbody.velocity = new Vector2(maxSpeed * (-1), rigidbody.velocity.y);
            if(this.transform.rotation.y != 180)
            {
                this.transform.rotation = new Quaternion(this.transform.rotation.x, 180f, this.transform.rotation.z, this.transform.rotation.w);
            }
        }
        else
        {
            leftBtn.color = Color.white;
            rightBtn.color = Color.white;
        }
    }

    private void _AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        if (animClip.name.Equals(CurrentAnimation))
            return;


        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        CurrentAnimation = animClip.name;
    }

    private void SetCurrentAnimation(AnimState _state)
    {
        switch (_state)
        {
            case AnimState.Idle:
                _AsncAnimation(AnimClip[(int)AnimState.Idle], true, 1f);
                break;
            case AnimState.Walk:
                _AsncAnimation(AnimClip[(int)AnimState.Walk], true, 1f);
                break;
            case AnimState.Jump:
                _AsncAnimation(AnimClip[(int)AnimState.Jump], false, 1f);
                break;
        }

        //스위치문 요약
        //_AsncAnimation(AnimClip[(int)_state], true, 1f);
    }

    private IEnumerator WaitForButton()
    {
        yield return new WaitForSeconds(0.3f);
        jumpBtn.color = Color.white;
    }
}
