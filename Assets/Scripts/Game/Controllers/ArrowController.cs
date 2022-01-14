using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    
    private SpriteRenderer arrowSprite;
    private AimController aimController;
    private PlayerControls.GameplayActions actions;

    private void Awake()
    {
        arrowSprite = GetComponentInChildren<SpriteRenderer>();
        aimController = FindObjectOfType<AimController>();
    }

    private void Start()
    {
        SetActions();
        Show(false);
    }

    public void SetActions()
    {
        actions = Controls.Instance.GameActions;
        actions.CancelAim.started += cts => Show(false);
        actions.AimButton.canceled += cts => Show(false);
    }

    private void Update()
    {
        if (aimController.Visible && Controls.Instance.IsAimHold)
        {
            UpdateArrowTransform();
            Show(aimController.DirectionChosen);
        }
    }

    public void OnVisibilityChanged()
    {
        if (!aimController.Visible)
            Show(false);
    }

    public void Show(bool value)
        => arrowSprite.enabled = value;

    private void UpdateArrowTransform()
    {
        transform.localPosition = line.GetPosition(0);

        Vector2 dir = line.GetPosition(1) - line.GetPosition(0);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
