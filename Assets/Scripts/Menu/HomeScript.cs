using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HomeScript : MonoBehaviour
{
    private Button button;
    private PlayerControls controls;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        controls = controls ?? new PlayerControls();
        controls.Menu.Back.performed += ctx => button.onClick.Invoke();
        controls.Menu.Enable();
    }

    private void OnDisable()
    {
        controls.Menu.Disable();
    }
}
