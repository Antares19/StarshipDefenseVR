using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI : MonoBehaviour
{
    public TextMesh Score;
    public TextMesh TimeLeft;

    public Transform PlayingTransform;
    public Transform PauseTransform;

    public float TransitionDuration = 1;

    [SerializeField]
    private bool isPlaying = true;
    [SerializeField]
    private float kPlaying = 1;

    public void SetState(string scoreString, float secondsLeft)
    {
        Score.text = scoreString;

        int iSecondsLeft = (int)secondsLeft;
        TimeLeft.text = (iSecondsLeft / 60) + ":" + (iSecondsLeft % 60).ToString("00");
    }
    public void SetPlaying(bool value)
    {
        isPlaying = value;
    }

    void Update()
    {
        float transitionSpeed = 1 / TransitionDuration;
        float maxDelta = Time.unscaledDeltaTime * transitionSpeed;

        float targetK = isPlaying ? 1 : 0;
        if (Mathf.Abs(targetK - kPlaying) <= maxDelta)
        {
            kPlaying = targetK;
        }
        else
        {
            kPlaying += Mathf.Sign(targetK - kPlaying) * maxDelta;
        }

        float k = kPlaying;

        transform.localScale = Vector3.Lerp(PauseTransform.lossyScale, PlayingTransform.lossyScale, k);
        transform.rotation = Quaternion.LookRotation(
            Vector3.Lerp(PauseTransform.forward, PlayingTransform.forward, k),
            Vector3.Lerp(PauseTransform.up, PlayingTransform.up, k)
        );
        transform.position = Vector3.Lerp(PauseTransform.position, PlayingTransform.position, k);
        
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
