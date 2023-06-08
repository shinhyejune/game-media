using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Spine.Unity;

public class OponentMove : MonoBehaviour
{
  
    [SerializeField]
    GameObject attackparticle;

    [SerializeField]
    UnityEvent attackEvent;


    public float speed = 1f;
    public float pauseTime = 0.5f;

    private Vector3 startPosition;
    private bool isMoving = true;

    //�Ѿ� ����
    public GameObject bulletPrefab;
    public float attackRate = 3f;
    public Transform target;
    private float timeAfterAttack;

    public PlayerMove player;


    //ü�� ��
    [SerializeField]
    private Slider hpBar;

    private float maxHp = 100;
    private float currentHp = 100;

    public float getDamage = 50f;//�޴� ������


    //������ �ִϸ��̼�
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    private bool isFirst = true;
    private int state = 0;

    public enum AnimState
    {
        Idle,Hit,Die
    }

    //���� �ִϸ��̼� ó���� ���������� ���� ����
    private AnimState _AnimState;

    //���� � �ִϸ��̼��� ����ǰ� �ִ����� ���� ����
    private string CurrentAnimation;

    private void Start()
    {
        timeAfterAttack = 0f;
        //startPosition = transform.position;
        //StartCoroutine(MoveObject());
    }

    private void FixedUpdate()
    {
        if(state == 0)
        {
            _AnimState = AnimState.Idle;
            SetCurrentAnimation(_AnimState);
        }
    }

    //���� �޴°�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Magic(Clone)")
        {
            state = 2;
            Destroy(other.gameObject);
            _AnimState = AnimState.Hit;
            CurrentAnimation = AnimClip[(int)AnimState.Hit].name;
            skeletonAnimation.state.SetAnimation(0, AnimClip[(int)AnimState.Hit], false);
            StartCoroutine(WaitForHit());
            currentHp -= getDamage;
            HandleHp();
            skeletonAnimation.GetComponent<SpineShader>().Active();
            attackEvent?.Invoke();
        }
    }

    private void HandleHp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / maxHp, Time.deltaTime * getDamage);
        if(currentHp <= 0f)
        {
            SetDie();
            player.targetIndex++;
        }
    }

    private void SetDie()
    {
        state = 1;
        _AnimState = AnimState.Die;
        CurrentAnimation = AnimClip[(int)AnimState.Die].name;
        skeletonAnimation.state.SetAnimation(0, AnimClip[(int)AnimState.Die], false);
        hpBar.gameObject.SetActive(false);
        StartCoroutine(WaitForDie());
    }

    private IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(0.5f);
        if (state == 2)
            state = 0;
    }

    private IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    //private void Update()
    //{
    //    timeAfterAttack += Time.deltaTime;

    //    if(timeAfterAttack >= attackRate)
    //    {
    //        timeAfterAttack = 0f;

    //        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    //        bullet.SetActive(true);
    //    }
    //}

    //private IEnumerator MoveObject()
    //{
    //    float journeyLength = Vector3.Distance(startPosition, target.position);
    //    float startTime = Time.time;

    //    while (true)
    //    {
    //        // ���� ��ġ���� ��ǥ �������� �̵�
    //        while (isMoving)
    //        {
    //            float distanceCovered = (Time.time - startTime) * speed;
    //            float fractionOfJourney = distanceCovered / journeyLength;
    //            transform.position = Vector3.Lerp(startPosition, target.position, fractionOfJourney);

    //            if (fractionOfJourney >= 1f)
    //            {
    //                // ��ǥ ������ ������ ��� �̵��� ���߰� ���� ��ġ�� �̵�
    //                isMoving = false;
    //                startTime = Time.time;
    //                break;
    //            }

    //            yield return null;
    //        }

    //        // ���� ��ġ���� ��ǥ �������� �̵�
    //        while (!isMoving)
    //        {
    //            float distanceCovered = (Time.time - startTime) * speed;
    //            float fractionOfJourney = distanceCovered / journeyLength;
    //            transform.position = Vector3.Lerp(target.position, startPosition, fractionOfJourney);

    //            if (fractionOfJourney >= 1f)
    //            {
    //                // ���� ��ġ�� ������ ��� �̵��� �ٽ� ����
    //                isMoving = true;
    //                startPosition = transform.position;
    //                yield return new WaitForSeconds(pauseTime);
    //                startTime = Time.time;
    //                break;
    //            }

    //            yield return null;
    //        }
    //    }
    //}

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
        }

        //����ġ�� ���
        //_AsncAnimation(AnimClip[(int)_state], true, 1f);
    }

    
    private IEnumerator AttackEffect()
    {
        attackparticle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackparticle.SetActive(false);

    }

    public void ActiveAttackEffect()
    {
        StartCoroutine(AttackEffect());
    }
}
