using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SubtitleTrigger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This trigger will active from value in seconds. By default is set zero")]
    private float activeFromSeconds = 0;
    public AudioSubtitle audioSubtitle;
    public UserLanguage language;
    public float delayInSeconds = 0;

    [TextArea(1, 25), SerializeField] private string rawText;

    internal bool triggered = false;

    public int GetId() => gameObject.name.GetHashCode();
    public string AudioName => audioSubtitle.clip.name;
    public float GetAudioTime() => audioSubtitle.GetTime();

    private void Awake()
    {
        audioSubtitle?.InitByAudioSource(gameObject.AddComponent<AudioSource>());
    }

    private void Start()
    {
        if (audioSubtitle.playOnAwake)
            StartCoroutine(AwakeSubtitle(activeFromSeconds + 1));
    }

    private IEnumerator AwakeSubtitle(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (!triggered)
        {
            SubtitleTriggerEnter?.Invoke(this, new SubtitleEnterEventArgs(audioSubtitle, null));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && Timer.Instance.Value > activeFromSeconds)
            SubtitleTriggerEnter?.Invoke(this, new SubtitleEnterEventArgs(audioSubtitle, other));
    }

    public void OnTriggered()
    {
        triggered = true;
    }

    public event EventHandler<SubtitleEnterEventArgs> SubtitleTriggerEnter;
}