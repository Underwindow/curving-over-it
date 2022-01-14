using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPreferenceLoader, IFinishEventListener
{
    public UnityEvent PlayerStopped, PlayerMoved, PlayerAsleep;

    public float BallRadius { get; private set; } 
    private Rigidbody2D rb;
    private Coroutine StayingCoroutine = null;
    private bool isAwaked = true;
    private AudioManager audioManager;
    private bool IsPlayerSLow => rb.velocity.magnitude < 1.5f;

    private void Start()
    {
        BallRadius = transform.localScale.x * .5f;
        audioManager = FindObjectOfType<AudioManager>();

        StartCoroutine(RenderTrail(.1f));

        var actions = Controls.Instance.GameActions;
            actions.ExitToMenu.performed += ctx => { Debug.Log("Escape key was pressed"); GameManager.Instance.LoadMenu(); };
            actions.Restart.performed    += ctx => { Debug.Log("R key was pressed"); GameManager.Instance.Restart(); };
    }

    private void FixedUpdate()
    {
        if (rb.IsSleeping())
        {
            if (isAwaked)
            {
                PlayerAsleep?.Invoke();
                isAwaked = false;
            }
        }
        else
        {
            isAwaked = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsPlayerSLow)
        {
            if (StayingCoroutine == null)
            {
                StayingCoroutine = StartCoroutine(Staying(.5f));
            }
        }
        else 
            ResetPlayerStaying();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        int layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(5)); // All layers excepts UI
        if (!rb.IsTouchingLayers(layerMask) || !IsPlayerSLow)         // If player dont touching any surface or still touching surface but moving fast
        { 
            ResetPlayerStaying();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impulse = 0f;

        collision.contacts.ToList().ForEach(c => impulse += c.normalImpulse);

        var snd = audioManager.GetSound(SoundEffectType.BouncingOnWoodenTable);
            snd.source.volume = snd.volume * impulse;
            snd.source.Play();

        if (Gamepad.all.Any())
        {
            var frequency = Mathf.Clamp(impulse / 100, .08f, 1);

            StartCoroutine(impulse > 17 ? 
                Gamepad.current.Vibrate(0f, frequency, frequency) : 
                Gamepad.current.Vibrate(frequency, 0f, frequency)
            );
        }
    }

    public bool IsGrounded()
    {
        if (rb.velocity == Vector2.zero)
            return true;

        RaycastHit2D cCast = Physics2D.CircleCast(
            rb.transform.position + Vector3.down * .05f,
            transform.localScale.x * .498f, Vector2.zero, 0,
            ~LayerMask.GetMask("Player", "UI", "Ignore Colliders")
        );

        return cCast.collider != null;
    }

    private IEnumerator RenderTrail(float delay)
    {
        var particleSystem = GetComponent<ParticleSystem>();

        yield return new WaitForSeconds(delay);

        var emission = particleSystem.emission;
            emission.enabled = true;

        var renderer = GetComponent<ParticleSystemRenderer>();
            renderer.enabled = true;
    }

    public void OnAimShot()
    {
        ResetPlayerStaying();
    }

    public IEnumerator Staying(float stayingTime)
    {
        yield return new WaitForSeconds(stayingTime);
        StayingCoroutine = null;

        PlayerStopped?.Invoke();
    }

    private void ResetPlayerStaying()
    {
        if (StayingCoroutine != null)
        {
            StopCoroutine(StayingCoroutine);
            StayingCoroutine = null;

            PlayerMoved?.Invoke();
        }
    }

    public void InitData()
    {
        rb = GetComponent<Rigidbody2D>();

        if (MyPlayerPrefs.Exists<PlayerRbData>())
            MyPlayerPrefs.GetData<PlayerRbData>().SetTo(rb);
    }

    public void SaveData()
    {
        MyPlayerPrefs.Save(new PlayerRbData(rb));
    }

    public void RemoveData()
    {
        MyPlayerPrefs.Remove<PlayerRbData>();
    }

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        PlayerStopped?.RemoveAllListeners();
        PlayerMoved?.RemoveAllListeners();
        PlayerAsleep?.RemoveAllListeners();

        PlayerStopped = PlayerMoved = PlayerAsleep = null;
        
        enabled = false;
    }
}