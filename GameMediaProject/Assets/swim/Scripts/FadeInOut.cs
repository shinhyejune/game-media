using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{

    public float fadeTime = 1.0f;
    public CanvasGroup canvas;

    private Material material;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(FadeIn());
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeTime;
            float alpha = Mathf.Lerp(0f, 1f, t);
            canvas.alpha = alpha;
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeTime;
            float alpha = Mathf.Lerp(1f, 0f, t);
            canvas.alpha = alpha;
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
            yield return null;
        }
    }
}
