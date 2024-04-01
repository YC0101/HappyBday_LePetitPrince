using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCounter : MonoBehaviour
{
    [SerializeField]
    private float timeValue = 30.0f;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private GameObject Manager;

    private int textValue = 0;

    void SetTimeText()
    {
        textValue = (int)timeValue;
        timeText.text = "Timer: " + textValue.ToString() + " sec";
    }

    // Update is called once per frame
    void Update()
    {
        timeValue -= Time.deltaTime;
        if(timeValue >= 0f)
        {
            SetTimeText();
        }
        else
        {
            timeValue = 0;
            Manager.GetComponent<showPictures>().isEnd = true;
            SetTimeText();
        }
    }

}
