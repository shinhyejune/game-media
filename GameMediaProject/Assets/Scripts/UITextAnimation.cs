using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class UITextAnimation : MonoBehaviour
{

    //text 알파값이 빠졌다 켜졌다
    //텍스트 한글자씩 써지기
    //[SerializeField]
    //Image text;
    [SerializeField]
    //CanvasGroup imgAlpha;
    SpriteRenderer img;

    [SerializeField]
    AnimationCurve animationCurve;

    float endAnimationTime = 0f;
    float deltaTime = 0f;

    [SerializeField]
    private float start = 1f;
    [SerializeField]
    private float end = 0f;

    [SerializeField]
    UnityEvent endEvent;

    [SerializeField]
    float animationTime = 0f;

    private void Start()
    {
        endAnimationTime = animationCurve[animationCurve.length - 1].time;
    }

    private void Update()
    {


        if(isActive)
        {

        TextAlpha();
        }
    }

    bool isActive = false;
    public void Active()
    {
        Debug.Log("active");
        isActive = true;
        deltaTime = 0;
    }
    public void Deactive()
    {
        isActive = false;
    }

    public void TextAlpha()
    {
        deltaTime += Time.deltaTime / animationTime;
        img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(start, end, animationCurve.Evaluate(deltaTime)));

        if(deltaTime>=endAnimationTime)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(start, end, animationCurve.Evaluate(1)));
            //imgAlpha.alpha = Mathf.Lerp(start, end, animationCurve.Evaluate(1));

            //끝
            Debug.Log("end event");

            isActive = false;     
            endEvent?.Invoke(); 

        }
    }
}
