using UnityEngine;
using TMPro;

public class GUISubtitles : MonoBehaviour
{
    public TextMeshProUGUI subs;

    private void Awake()
    {
        Clear();
    }

    public void Clear()
    {
        subs.text = string.Empty;
    }

    public void SetText(string text)
    {
        subs.text = text;
    }
}
