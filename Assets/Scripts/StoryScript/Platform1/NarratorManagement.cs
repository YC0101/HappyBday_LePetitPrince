using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarratorManagement : MonoBehaviour
{
    public float FadeRate = 0.5f;

    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextAsset file;
    [SerializeField]
    private GameObject player;
    private float timer;
    [SerializeField]
    private float timeType = 0.1f;
    private int wordCount;
    private bool isTyping = false;
    private float targetAlpha;

    private string narText = "Oh my dear, do you really think the game has ended?";
    private List<string> allLine;
    private int curentLine;

    private float timeBegin = 0f;

    private Color curColor;
    private Color textColor;
    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        timeText.text = "";
        allLine = new List<string>(file.text.Split("/n"));
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBegin >= 2f)
        {
            startTyping();
            if (!isTyping)
            {
                if(curentLine >= allLine.Count)
                {
                    Invoke("startFadein", 2.5f);
                    player.SetActive(true);
                }
                else
                {
                    narText = allLine[curentLine];
                    isTyping = true;
                    wordCount = 0;
                    curentLine++;
                    timeBegin = 0f;
                }
            }
        }
        else
        {
            timeBegin += Time.deltaTime;
        }
    }


    public void startTyping()
    {
        if (isTyping)
        {
            timer += Time.deltaTime;
            if(timer >= timeType)
            {
                timer = 0;
                wordCount++;
                timeText.text = narText.Substring(0, wordCount);
                if(wordCount >= narText.Length)
                {
                    isTyping = false;
                }
            }
        }
    }

    IEnumerator FadeIn()
    {
        targetAlpha = 0.0f;
        curColor = image.color;
        textColor = timeText.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            Debug.Log(image.material.color.a);
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            textColor.a = Mathf.Lerp(textColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.color = curColor;
            timeText.color = textColor;
            yield return null;
        }
    }

    void startFadein()
    {

        StartCoroutine(FadeIn());
    }



}
