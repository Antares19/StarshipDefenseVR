using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI : MonoBehaviour
{
    public TextMesh Score;
    public TextMesh TimeLeft;

    public void SetState(string scoreString, float secondsLeft)
    {
        Score.text = scoreString;

        int iSecondsLeft = (int)secondsLeft;
        TimeLeft.text = (iSecondsLeft / 60) + ":" + (iSecondsLeft % 60).ToString("00");
    }

    void Start()
    {
        //StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        yield return new WaitForSeconds(2);
        SetState("Works", 127.4f);
    }
}
