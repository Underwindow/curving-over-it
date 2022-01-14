using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Finish : MonoBehaviour
{
    public UnityEvent UnityPlayerFinished;
    private List<IFinishEventListener> finishEventListeners;
    private AudioManager audioManager;
    private SoundEffect snd;
    private Timer timer;
    private HitsCounter hitsCounter;

    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        hitsCounter = FindObjectOfType<HitsCounter>();

        GetComponent<Collider2D>().isTrigger = true;
        finishEventListeners = FindObjectsOfType<MonoBehaviour>().OfType<IFinishEventListener>().ToList();
        finishEventListeners.ForEach(el => PlayerFinished += el.OnPlayerFinished);

        audioManager = FindObjectOfType<AudioManager>();
        snd = audioManager.Play(SoundEffectType.Finish);
        snd.source.Pause();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.attachedRigidbody.CompareTag("Player")) {
            if (PlayerFinished != null) 
            {
                UnityPlayerFinished?.Invoke();
                PlayerFinished?.Invoke(this, new PlayerFinishedEventArgs((float)timer.Value, hitsCounter.HitsCount));
                
                snd.source.UnPause();
                StartCoroutine(AudioFade.FadeOut(snd, 1f, Mathf.Lerp, .5f));
                finishEventListeners.ForEach(el => PlayerFinished -= el.OnPlayerFinished);
            }

            PlayerFinished = null;
        }
    }

    public event EventHandler<PlayerFinishedEventArgs> PlayerFinished;
}

public class PlayerFinishedEventArgs : EventArgs
{
    public SafeFloat TotalHits { get; }
    public float FinishTime { get; }
    public int PlayerScore => (int)Math.Max(0, TimeSpan.FromSeconds(FinishTime).TotalMilliseconds);

    public PlayerFinishedEventArgs(float finishTime, SafeFloat totalHits)
    {
        FinishTime = finishTime;
        TotalHits = totalHits;
    }
}
