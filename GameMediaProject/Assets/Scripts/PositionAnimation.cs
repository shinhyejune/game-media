using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionAnimation : MonoBehaviour
{
    

    private bool isActive = false;
    private float deltaTime = 0;
    private float endPositonAnimationCurveTime = 0;

    private Vector2 lerpSourcePositon = Vector2.zero;

    [SerializeField]
    private Vector2 lerpDestPositon;
    
    [SerializeField] private Transform target;
    [SerializeField] private AnimationCurve PositonAnimationCurve;

     private Vector2 targetPositon;
    [SerializeField] private float PositonTime = 1.0f;
    [SerializeField] private UnityEvent activeEvent;
    [SerializeField] private UnityEvent finishEvent;

    private void Start()
    {
        endPositonAnimationCurveTime = PositonAnimationCurve[PositonAnimationCurve.length - 1].time;
    }
    private void Update()
    {
        if (isActive)
        {
            PositonProgress();
        }
    }

    public void Active()
    {
        isActive = true;
        lerpSourcePositon = target.transform.position;
        deltaTime = 0;
        activeEvent?.Invoke();
    }
  

    private void PositonProgress()
    {
        deltaTime += Time.deltaTime / PositonTime;
        target.transform.position = Vector2.Lerp(lerpSourcePositon, lerpDestPositon, PositonAnimationCurve.Evaluate(deltaTime));

        if (deltaTime >= endPositonAnimationCurveTime)
        {
            target.transform.position = Vector2.Lerp(lerpSourcePositon, lerpDestPositon, PositonAnimationCurve.Evaluate(1));
            finishEvent?.Invoke();
            isActive = false;
        }
    }

}
