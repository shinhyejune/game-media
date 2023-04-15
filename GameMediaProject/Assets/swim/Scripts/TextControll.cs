using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControll : MonoBehaviour
{

    public Text text;

    public string[] sentences = { "test 1", "test 2", "test 3" };
    private int currentIndex = 0;

    public float typeSpeed = 0.1f;

    private IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            StopAllCoroutines();
            StartTyping();
        }
    }

    public void StartTyping()
    {
        if(currentIndex < sentences.Length)
        {
            string sentence = sentences[currentIndex];
            currentIndex++;
            StartCoroutine(TypeSentence(sentence));
        }
    }
}
