using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BookController : MonoBehaviour
{

    //������ �ִϸ��̼�
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    public enum AnimState
    {
        Idle, Attack
    }

    //���� �ִϸ��̼� ó���� ���������� ���� ����
    public AnimState _AnimState;

    //���� � �ִϸ��̼��� ����ǰ� �ִ����� ���� ����
    public string CurrentAnimation;

    public int state = 0;

    private void FixedUpdate()
    {
        if(state == 0)
        {
            _AnimState = AnimState.Idle;
        }

        SetCurrentAnimation(_AnimState);
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
            case AnimState.Attack:
                _AsncAnimation(AnimClip[(int)AnimState.Attack], false, 1f);
                break;
        }

        //����ġ�� ���
        //_AsncAnimation(AnimClip[(int)_state], true, 1f);
    }

    public void SetAttack()
    {
        state = 1;
        _AnimState = AnimState.Attack;
        SetCurrentAnimation(_AnimState);
        StartCoroutine(WaitForHit());
    }

    private IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(1.6f);
        if (state == 1)
            state = 0;
    }

}
