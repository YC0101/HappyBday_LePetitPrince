using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointStoryTrigger : MonoBehaviour
{
    public float FadeRate = 0f;

    private bool ischecked = false;
    [SerializeField]
    private GameObject triggerPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text narText;

    private string tempText;

    private bool isTyping = false;
    private float timer;
    [SerializeField]
    private float timeType = 0.1f;
    private int wordCount;

    [SerializeField]
    private TextAsset file;


    private List<string> allLine;
    private int curentLine;


    private Color textColor;
    private float targetAlpha = 0f;

    private float timeBegin = 0f;

    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        originalColor = narText.color;
        allLine = new List<string>(file.text.Split("/n"));
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMovement>().isAbleMove == false)
        {
            if (player.GetComponent<PlayerMovement>().isAbleMove == false && timeBegin > 1f)
            {
                narText.color = originalColor;
                startTyping();
                if (!isTyping)
                {
                    if (curentLine >= allLine.Count)
                    {
                        Invoke("startFadein", 1.5f);
                        triggerPoint.GetComponent<BoxCollider2D>().enabled = false;
                        player.GetComponent<PlayerMovement>().isAbleMove = true;
                    }
                    else
                    {
                        Debug.Log(curentLine);
                        tempText = allLine[curentLine];
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
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable") && ischecked ==false)
        {
            narText.text = "";
            Debug.Log("Player Enters");
            if (GetComponent<PlayerMovement>().isAbleMove)
            {
                GetComponent<PlayerMovement>().isAbleMove = false;

            }
            ischecked = true;

        }
    }

    public void startTyping()
    {
        if (isTyping)
        {
            timer += Time.deltaTime;
            if (timer >= timeType)
            {
                timer = 0;
                wordCount++;
                narText.text = tempText.Substring(0, wordCount);
                if (wordCount >= tempText.Length)
                {
                    isTyping = false;
                }
            }
        }
    }

    IEnumerator FadeIn()
    {
        targetAlpha = 0.0f;
        textColor = narText.color;
        while (Mathf.Abs(textColor.a - targetAlpha) > 0.0001f)
        {
            Debug.Log(narText.material.color.a);
            textColor.a = Mathf.Lerp(textColor.a, targetAlpha, FadeRate * Time.deltaTime);
            narText.color = textColor;
            yield return null;
        }
        textColor.a = targetAlpha;
        narText.color = textColor;
    }

    void startFadein()
    {

        StartCoroutine(FadeIn());
    }

}
