using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour, IFinishEventListener
{
    [SerializeField] private UnityEvent AimShot;
    [SerializeField] private UnityEvent VisibilityChanged;
    [SerializeField] private AnimationCurve punchCurve;
    [SerializeField] private Material coolDownMaterial;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private SpriteRenderer uiBallSprite;
    [SerializeField] private Transform cursor;
    [SerializeField] private Material readyMaterial;

    private bool IsReady { get; set; } = false;
    private bool IsCLickedOnUI => EventSystem.current.IsPointerOverGameObject();
    private Vector2 ScreenCenterToWorld => uiCamera.ScreenToWorldPoint(screenCenter);
    private Vector2 screenCenter;
    private Vector2 startPos, currPos, leftStickPos, direction, deltaPos, shotForce = Vector2.zero;
    private SafeFloat defOffset;
    private PlayerController player;
    private Rigidbody2D playerRb, uiBallRb;
    private LineRenderer line;
    private SpriteRenderer playerSprite;
    private AudioManager audioManager;
    private bool visible;
    private InputDevice device;

    public bool DirectionChosen { get; private set; }

    public bool Visible {
        get => visible;
        private set {
            Show(visible = value);
            VisibilityChanged?.Invoke();
        }
    }

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.widthCurve = punchCurve;

        uiBallSprite.material = coolDownMaterial;
        defOffset = .5f;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerRb = player.GetComponent<Rigidbody2D>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        uiBallRb = uiBallSprite.GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        cursor.position = Vector2.zero;
        screenCenter = new Vector2(uiCamera.scaledPixelWidth, uiCamera.scaledPixelHeight) * .5f;

        UpdateMaterial(IsReady);
        SetActions();
        Visible = false;
    }

    private void OnGUI()
    {
        UpdateStartPos();
    }

    private void FixedUpdate()
    {
        if (!player.IsGrounded())
            UpdateMaterial(false);

        if (Controls.Instance.IsAimHold && DirectionChosen)
            RotateUIBall();
    }

    void Update()
    {
        if (!Controls.Instance.IsAimHold || !Visible)
            return;

        deltaPos = (currPos = GetCurrPos()) - startPos;

        if (DirectionChosen = deltaPos.magnitude > defOffset)
        {
            UpdateDirection();
        }
        else
        {
            direction = deltaPos.normalized;
            new List<int> { 0, 1 }.ForEach(i => line.SetPosition(i, Vector2.zero));
        }
    }

    private void LateUpdate()
    {
        cursor.position = startPos + (DirectionChosen ? (Vector2)line.GetPosition(1) : deltaPos);
    }

    private void UpdateDirection()
    {
        Vector2 circlePoint = direction * (float)defOffset;
        Vector2 deltaPointsPos = currPos - (startPos + circlePoint);
        Vector2 endPoint = deltaPointsPos.magnitude > 1
            ? circlePoint + deltaPointsPos.normalized
            : circlePoint + deltaPointsPos;

        shotForce = circlePoint - endPoint;

        line.SetPosition(0, circlePoint);
        line.SetPosition(1, endPoint);

        if (Vector2.Angle(direction, -shotForce) > 89)
        {
            int layerMask = LayerMask.GetMask(LayerMask.LayerToName(5)); // Only UI layer
            RaycastHit2D hit2D = Physics2D.Raycast((Vector2)transform.position + endPoint, shotForce.normalized, Mathf.Infinity, layerMask);
            if (hit2D.collider != null)
                direction = (hit2D.point - (Vector2)transform.position).normalized;
        }
    }

    private void SetActions()
    {
        var actions = Controls.Instance.GameActions;
            actions.AimButton.started  += ctx => { OnAimBtnDown(ctx); Debug.Log("AimButton.started"); };
        actions.AimButton.canceled += ctx => { OnAimBtnUp(); Debug.Log("AimButton.canceled"); };
            actions.CancelAim.started  += ctx => Visible = false;
    }


    private Vector2 GetCurrPos()
    {
        Vector2 pointerPos = device is Gamepad ? (Vector2)uiCamera.WorldToScreenPoint(startPos) : Mouse.current.position.ReadValue();
        Vector2 result = uiCamera.ScreenToWorldPoint(pointerPos);
        
        if (device is Gamepad)
        {
            leftStickPos += Gamepad.current.leftStick.ReadValue() * Time.deltaTime * 3f;
            leftStickPos = Vector2.ClampMagnitude(leftStickPos, 1.5f);
            result += leftStickPos;
        }
        return result;
    }

    private void UpdateStartPos()
    {
        if (device is Gamepad)
        {
            var tempPos = startPos + Gamepad.current.rightStick.ReadValue() * Time.deltaTime * 2;
            Vector2 newPos = uiCamera.ScreenToWorldPoint(new Vector2(
                Mathf.Clamp(uiCamera.WorldToScreenPoint(tempPos).x, 0, uiCamera.scaledPixelWidth),
                Mathf.Clamp(uiCamera.WorldToScreenPoint(tempPos).y, 0, uiCamera.scaledPixelHeight)
            ));

            transform.position = startPos = newPos;
        }
    }

    private void RotateUIBall()
    {
        var punchAngle = -Vector2.SignedAngle(direction, -shotForce);
        uiBallRb.MoveRotation(uiBallRb.rotation + punchAngle * shotForce.magnitude * .4f);
    }

    private void OnAimBtnDown(InputAction.CallbackContext ctx)
    {
        if (IsCLickedOnUI) return;
        
        uiBallRb.rotation = 0;
        switch (device = ctx.control.device)
        {
            case InputDevice a when a is Mouse:
                Cursor.visible = true;
                Vector2 mousePos = Mouse.current.position.ReadValue();
                transform.position = startPos = uiCamera.ScreenToWorldPoint(mousePos);
                break;

            case InputDevice b when b is Gamepad:
                Cursor.visible = false;
                break;
        }

        Visible = true;
    }

    private void OnAimBtnUp()
    {
        if (IsReady && DirectionChosen && Visible && player.IsGrounded())
            AimShot?.Invoke();

        for (int i = 0; i < line.positionCount; i++)
            line.SetPosition(i, Vector3.zero);

        currPos = startPos;
        leftStickPos = Vector2.zero;
        Visible = false;
    }

    private void Show(bool value)
    {
        uiBallSprite.enabled = value;
        cursor.gameObject.SetActive(value);
        line.enabled = value;
    }

    private void UpdateMaterial(bool isPlayerReady)
    {
        var material = isPlayerReady ? readyMaterial : coolDownMaterial;
        uiBallSprite.material = material;
        playerSprite.material = material;
    }

    public void Ready(bool value)
    {
        IsReady = value;
        UpdateMaterial(value);
    }

    public void Shot()
    {
        playerRb.AddForceAtPosition(
            shotForce * playerRb.mass * 18.75f,
            playerRb.position + direction * player.BallRadius,
            ForceMode2D.Impulse);
    }

    public void PlayShotSound()
    {
        var snd = audioManager.GetSound(SoundEffectType.BallHitting);
            snd.source.volume = snd.volume * shotForce.magnitude;
            snd.source.Play();
    }

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        gameObject.SetActive(false);
        Destroy(this);
    }
}