using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class OponentMove : MonoBehaviour
{
    public float speed = 1f;
    public float pauseTime = 0.5f;

    private Vector3 startPosition;
    private bool isMoving = true;

    //�Ѿ� ����
    public GameObject bulletPrefab;
    public float attackRate = 3f;
    public Transform target;
    private float timeAfterAttack;



    //������ �ִϸ��̼�
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    private bool isFirst = true;

    public enum AnimState
    {
        Idle
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
        _AnimState = AnimState.Idle;
        SetCurrentAnimation(_AnimState);
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
}
