//https://cpp11.tistory.com/67
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpineShader : MonoBehaviour
{

    [SerializeField]
    MeshRenderer meshRenderer;

    MaterialPropertyBlock _block;


    Color color = Color.white;


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

    int id;

    private void Update()
    {
        if (isActive)
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
    void Start()
    {
        endAnimationTime = animationCurve[animationCurve.length - 1].time;

        _block = new MaterialPropertyBlock();
        meshRenderer.SetPropertyBlock(_block);
        id = Shader.PropertyToID("_Color");

        _block.SetColor(id, color);
        meshRenderer.SetPropertyBlock(_block);
    }
    public void TextAlpha()
    {
        deltaTime += Time.deltaTime / animationTime;
        color.a = Mathf.Lerp(start, end, animationCurve.Evaluate(deltaTime));
        _block.SetColor(id, color);
        meshRenderer.SetPropertyBlock(_block);
        Debug.Log(color.a);


        if (deltaTime >= endAnimationTime)
        {
            color.a = Mathf.Lerp(start, end, animationCurve.Evaluate(1));

            Debug.Log(color.a);

            //³¡
            Debug.Log("end event");

            isActive = false;
            endEvent?.Invoke();

        }
    }
}
