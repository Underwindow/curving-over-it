using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class FinalCaptions : MonoBehaviour, IFinishEventListener
{
    [SerializeField]
    private TextMeshProUGUI title, shotsUI, timeUI;
    private TimeSpan elapsedTime;
    private List<string> fmtStrings;

    [SerializeField]
    private GameObject blackScreen, theEndblackScreen, finalBlackScreen;

    [SerializeField]
    private float delay;

    [SerializeField]
    private Animator captionsAnim;

    private AudioManager audioManager;
    private SoundEffect textSound;

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        Debug.Log("FinalCaptions OnPlayerFinished");
        ShowTotalHits((int)args.TotalHits);
        ShowTotalTime(args.FinishTime);

        StartCoroutine(ShowCaptions(delay));
    }

    private void Awake()
    {
        CaptionsPlayed += GameManager.Instance.OnCaptionsPlayed;
        audioManager = FindObjectOfType<AudioManager>();

        title.transform.parent.gameObject.SetActive(true);
        title.enabled = false;
        timeUI.enabled = false;
        shotsUI.enabled = false;
    }
    private void ShowTotalHits(int hits)
    {
        switch (GameLocalization.Instance.Language) 
        {
            case UserLanguage.eng:
                shotsUI.text = $"with {hits} shots";
                break;
            case UserLanguage.rus:
                shotsUI.text = $"совершив {hits} ударов";
                break;
        }
    }

    private void ShowTotalTime(float time)
    {
        elapsedTime = TimeSpan.FromSeconds(time);

        string days = "", hours = "", minutes = "", seconds = "";
        switch (GameLocalization.Instance.Language)   
        {
            case UserLanguage.eng:
                days = "days";
                hours = "hours";
                minutes = "minutes";
                seconds = "seconds";
                break;
            case UserLanguage.rus:
                days = "дней";
                hours = "часов";
                minutes = "минут";
                seconds = "секунд";
                break;
        }

        var fmtStr = $"s'.'f' {seconds}'";
        fmtStrings = new List<string>() { fmtStr };
        if (elapsedTime.Minutes > 0 || elapsedTime.Hours > 0)
            fmtStrings.Add(fmtStr = fmtStr.Insert(0, $"m' {minutes} \n'"));
        if (elapsedTime.Hours > 0 || elapsedTime.Days > 0)
            fmtStrings.Add(fmtStr = fmtStr.Insert(0, $"h' {hours} \n'"));
        if (elapsedTime.Days > 0)
            fmtStrings.Add(fmtStr.Insert(0, $"d' {days}\n'"));
    }

    private IEnumerator ShowCaptions(float delay)
    {
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(delay);
        title.enabled = true;

        yield return new WaitForSeconds(1);

        timeUI.enabled = true;

        foreach (var fmtStr in fmtStrings)
        {
            timeUI.text = elapsedTime.ToString(fmtStr);

            audioManager.Play(SoundEffectType.TextHit);

            yield return new WaitForSeconds(1f);
        }
        
        yield return new WaitForSeconds(.5f);
        
        audioManager.Play(SoundEffectType.TextHit);
        shotsUI.enabled = true;
        theEndblackScreen.SetActive(true);

        yield return new WaitForSeconds(1f);

        if (!PlayerPrefs.HasKey("FINISHED_ONCE"))
        {
            var snd = audioManager.GetSound(SoundEffectType.TheEnd);
            StartCoroutine(AudioFade.FadeIn(snd, 7, Mathf.Lerp));

            captionsAnim.enabled = true;
            CreditsStarted?.Invoke();
            PlayerPrefs.SetInt("FINISHED_ONCE", 1);

            yield return new WaitForSeconds(40f);
            StartCoroutine(AudioFade.FadeOut(snd, 7, Mathf.Lerp));

            yield return new WaitForSeconds(2.6f);

            finalBlackScreen.SetActive(true);

            yield return new WaitForSeconds(1f);
        }

        CaptionsPlayed?.Invoke(this, null);
        CaptionsPlayed -= GameManager.Instance.OnCaptionsPlayed;
    }

    public event EventHandler<EventArgs> CaptionsPlayed;
    public UnityEvent CreditsStarted;
}