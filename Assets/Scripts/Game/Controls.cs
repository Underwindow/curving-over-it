using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls Instance { get; private set; }
    public PlayerControls Actions { get; private set; }
    public PlayerControls.GameplayActions GameActions { get; private set; }
    public bool IsAimHold { get; private set; } = false;
    public bool IsZoomHold { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Actions = Actions ?? new PlayerControls();

        GameActions = Actions.Gameplay;
        GameActions.AimButton.started += ctx => IsAimHold = true;
        GameActions.AimButton.canceled += ctx => IsAimHold = false;
        GameActions.Zoom.started += ctx => IsZoomHold = true;
        GameActions.Zoom.canceled += ctx => IsZoomHold = false;
    }

    private void OnEnable() => GameActions.Enable();

    private void OnDisable() => GameActions.Disable();
}