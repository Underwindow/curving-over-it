using System;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SubtitlesResource : MonoBehaviour
{
    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

    public string[] GetText(string subtitleTextKey)
    {
        string[] tmp = new string[] { };

        if (lines.TryGetValue(subtitleTextKey, out tmp))
            return tmp;

        print($"Текст по ключу: \"{subtitleTextKey}\" не найден");
        return new string[] { };
    }

    private void Awake()
    {
        var textAsset = Resources.Load<TextAsset>(GameLocalization.Instance.Language + "_subtitles");
        var subtitleText = JsonUtility.FromJson<SubtitleText>(textAsset.text);

        foreach (var text in subtitleText.lines)
        {
            lines[text.key] = text.line;
        }
    }
}
