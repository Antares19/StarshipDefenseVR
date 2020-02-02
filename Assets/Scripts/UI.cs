using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI : MonoBehaviour
{
    public TextMesh Score;
    public TextMesh TimeLeft;

    public Transform GameOverContainer;
    public Transform RestartButtonContainer;

    public Transform TimeLeftLabelContainer;
    public Transform TimeLeftValueContainer;

    public Transform PlayingTransform;
    public Transform PauseTransform;

    public float TransitionDuration = 1;

    [SerializeField]
    private bool isPlaying = true;
    [SerializeField]
    private bool isOver = false;
    [SerializeField]
    private float kPlaying = 1;
    [SerializeField]
    private float kOver = 0;

    public void SetState(string scoreString, float secondsLeft)
    {
        Score.text = scoreString;

        int iSecondsLeft = (int)secondsLeft;
        TimeLeft.text = (iSecondsLeft / 60) + ":" + (iSecondsLeft % 60).ToString("00");
    }
    public void SetPlaying(bool isPlaying, bool gameOver)
    {
        this.isPlaying = isPlaying;
        this.isOver = gameOver;
    }

    void Update()
    {
        float transitionSpeed = 1 / TransitionDuration;
        float maxDelta = Time.unscaledDeltaTime * transitionSpeed;

        // Play/Pause Transition
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

        // GameOver or not transition
        float targetKOver = isOver ? 1 : 0;

        if (Mathf.Abs(targetKOver - kOver) <= maxDelta)
        {
            kOver = targetKOver;
        }
        else
        {
            kOver += Mathf.Sign(targetKOver - kOver) * maxDelta;
        }

        GameOverContainer.transform.localScale = new Vector3(1, kOver, 1);
        RestartButtonContainer.transform.localScale = new Vector3(1, kOver, 1);
        RestartButtonContainer.gameObject.SetActive(kOver != 0);

        TimeLeftLabelContainer.transform.localScale = new Vector3(1, 1 - kOver, 1);
        TimeLeftValueContainer.transform.localScale = new Vector3(1, 1 - kOver, 1);
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
