using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TableRow : MonoBehaviour
{
    public TextMeshProUGUI rank, playerName, time, hits;
    public Image playerAvatar, background;
    public User User { get; private set; } = null;
    public LeaderboardEntry Entry { get; private set; } = null;

    public void SetData(LeaderboardEntry entry)
    {
        Entry = entry;

        rank.text = Entry.Rank.ToString();

        User = new User(Entry.SteamID,
            (username) => playerName.text = username,
            (avatar) => playerAvatar.sprite = avatar.CreateSprite()
        );

        time.text = GetTimeStrFromScore(Entry.Score);
        hits.text = Entry.Details[0].ToString();
    }

    public string GetTimeStrFromScore(int score)
    {
        var fmtString = @"ss\.fff";
        var timeSpan = TimeSpan.FromMilliseconds(score);

        if (timeSpan.Minutes > 0 || timeSpan.Hours > 0)
            fmtString = fmtString.Insert(0, @"mm\:");
        if (timeSpan.Hours > 0 || timeSpan.Days > 0)
            fmtString = fmtString.Insert(0, @"hh\:");
        if (timeSpan.Days > 0)
            fmtString = fmtString.Insert(0, @"d\:");

        return timeSpan.ToString(fmtString);
    }

    public void Highlight()
    {
        background.color = new Color(.8f, .8f, .8f, .5f);
    }
}
