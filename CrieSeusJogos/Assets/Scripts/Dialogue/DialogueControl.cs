using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Serializable]
    public enum Idiom
    {
        pt,
        eng,
        spa
    };
    public Idiom CurrentIdiom;

    [Header("Components")]
    public GameObject dialogueObj;
    public Image profileSprite;
    public TMP_Text speechText;
    public TMP_Text actorNameText;


    [Header("Settings")]
    public float typingSpeed;

    private bool isShowing;
    private int index;
    private string[] sentences;

    public static DialogueControl instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"There is already an instance of {nameof(DialogueControl)}!!");
            Destroy(this);
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach(var c in sentences[index].ToCharArray()){
            speechText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            speechText.text = string.Empty;
            if(index < sentences.Length - 1)
            {
                index++;
                StartCoroutine(TypeSentence());
            }
            else
            {
                index = 0;
                dialogueObj.SetActive(false);
                isShowing = false;
                sentences = null;
            }
        }
    }

    public void Speech(IEnumerable<string> txt)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt.ToArray();
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
