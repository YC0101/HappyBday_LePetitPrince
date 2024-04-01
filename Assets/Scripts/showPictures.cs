using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showPictures : MonoBehaviour
{
    public bool isEnd = false;
    public int score = 0;

    [SerializeField]
    private Image[] imageDisplays;

    [SerializeField]
    private Sprite[] ZhuPics;

    [SerializeField]
    private Sprite[] PigPics;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject WinBoard;
    [SerializeField]
    private GameObject LoseBoard;
    [SerializeField]
    private GameObject GameContent;
    

    private int isZhu, imgRand, ZhuRand, PigRand;

    private float timeCount = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        //hide all display
        foreach (Image i in imageDisplays)
        {
            i.enabled = false;
        }
        isZhu = -1;
        score = 0;
        //SetActive
        WinBoard.SetActive(false);
        LoseBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            timeDecline();
            checkInputScore();
            setScore();
        }
        else
        {
            imageDisplays[imgRand].enabled = false;
            GameContent.SetActive(false);
            if(score >= 120)
            {
                WinBoard.SetActive(true);
            }
            else
            {
                LoseBoard.SetActive(true);
            }
        }
    }

    void getRand()
    {
        //Random all the number needed
        isZhu = Random.Range(0, 2);
        imgRand = Random.Range(0,3);
        ZhuRand = Random.Range(0, ZhuPics.Length);
        PigRand = Random.Range(0, PigPics.Length);
    }

    void timeDecline()
    {
        if(timeCount > 0f)
        {
            timeCount -= Time.deltaTime;
            if(isZhu == -1)
            {
                imageDisplays[imgRand].enabled = false;
            }
        }
        else
        {
            timeCount = 2.0f;
            imageDisplays[imgRand].enabled = false;
            isZhu = -1;
            getRand();
            imageDisplays[imgRand].enabled = true;
            if (isZhu == 1)
            {
                imageDisplays[imgRand].sprite = ZhuPics[ZhuRand];
            }
            else if(isZhu == 0)
            {
                imageDisplays[imgRand].sprite = PigPics[PigRand];
            }
        }
    }

    void show()
    {
        timeCount = 2.0f;
        imageDisplays[imgRand].enabled = false;
        isZhu = -1;
        getRand();
        imageDisplays[imgRand].enabled = true;
        if (isZhu == 1)
        {
            imageDisplays[imgRand].sprite = ZhuPics[ZhuRand];
        }
        else if (isZhu == 0)
        {
            imageDisplays[imgRand].sprite = PigPics[PigRand];
        }
    }

    void checkInputScore()
    {
        if (isZhu == 0 && Input.GetKeyDown(KeyCode.D))
        {
            score += 10;
            isZhu = -1;
        }
        else if ( isZhu == 1 && Input.GetKeyDown(KeyCode.J))
        {
            score += 10;
            isZhu = -1;
        }
        else if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.D))
        {
            isZhu = -1;
        }
    }

    void setScore()
    {
        scoreText.text = "Score: " + score.ToString() + " ";
    }
}
