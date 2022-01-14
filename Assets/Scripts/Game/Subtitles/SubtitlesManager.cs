using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SubtitlesResource))]
public class SubtitlesManager : MonoBehaviour, IPreferenceLoader, IFinishEventListener
{
    private SubtitlesResource subtitlesResource;
    private GUISubtitles subtitleGui;
    private Coroutine textCoroutine;
    private SubtitleTrigger currSubtitle;
    private List<SubtitleTrigger> subtitles;

    public void OnSubtitleTriggerEnter(object sender, SubtitleEnterEventArgs args)
    {
        var subtitle = sender as SubtitleTrigger;

        if (textCoroutine != null)
        {
            if (currSubtitle.AudioName == subtitle.AudioName)
                return;

            if (currSubtitle.audioSubtitle.interruptible)
                StopSubtitle(currSubtitle);
            else
                return;
        }

        currSubtitle = subtitle;

        textCoroutine = StartCoroutine(PlaySub(currSubtitle));
    }

    private IEnumerator PlaySub(SubtitleTrigger subtitle)
    {
        Debug.Log($"PlayingSub:{subtitle.AudioName}");
        yield return new WaitForSeconds(subtitle.delayInSeconds);

        subtitle.OnTriggered();
        subtitle.audioSubtitle.source.Play();

        var textToPLay = subtitlesResource.GetText(subtitle.AudioName);
        var textLength = textToPLay.Select(line => line.Length).Sum();
        var audioTime = subtitle.GetAudioTime();

        foreach (var line in textToPLay)
        {
            subtitleGui.SetText(line);

            var delay = audioTime * line.Length / textLength;

            yield return new WaitForSeconds(delay);
        }

        StopSubtitle(subtitle);
    }

    private void StopSubtitle(SubtitleTrigger subtitle)
    {
        subtitle.SubtitleTriggerEnter -= OnSubtitleTriggerEnter;

        var audioSub = subtitle.audioSubtitle;
            audioSub.source.Stop();

        Destroy(audioSub.source);

        if (textCoroutine != null)
            StopCoroutine(textCoroutine);
        textCoroutine = null;

        subtitleGui.Clear();
    }

    public void InitData()
    {
        subtitlesResource = GetComponent<SubtitlesResource>();
        subtitleGui = FindObjectOfType<GUISubtitles>();
        subtitles = GetComponentsInChildren<SubtitleTrigger>(includeInactive: false)
            .Where(s => s.language == GameLocalization.Instance.Language)
            .ToList();

        foreach (var s in subtitles)
        {
            var subtitleId = s.GetId();
            bool subTriggered = false;

            if (MyPlayerPrefs.Exists<SubtitleData>(subtitleId))
            {
                subTriggered = MyPlayerPrefs.GetData<SubtitleData>(subtitleId).played;
                s.triggered = subTriggered;
            }

            if (!subTriggered)
                s.SubtitleTriggerEnter += OnSubtitleTriggerEnter;
        }
    }

    public void SaveData()
    {
        subtitles.ForEach(s => MyPlayerPrefs.Save(new SubtitleData(s), s.GetId()));
    }

    public void RemoveData()
    {
        subtitles.ForEach(s => MyPlayerPrefs.Remove<SubtitleData>(s.GetId()));
    }

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        if (textCoroutine != null)
            StopSubtitle(currSubtitle);
    }
}