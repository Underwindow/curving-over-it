// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""8cf26820-8c75-4bab-8482-f5024042bb5c"",
            ""actions"": [
                {
                    ""name"": ""ExitToMenu"",
                    ""type"": ""Button"",
                    ""id"": ""e0f6edc1-cce0-4710-8caa-f8a2c58d6f26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""dc7609a5-d8a1-4b54-8015-9222ebae58fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""9a4a1fc8-b666-4836-b1fe-d05789582c22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6b587c53-4030-4a45-a440-e79ba5662814"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartPos"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7fd438d4-8a0c-4f98-b1ca-5959284b0ff1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""ScaleVector2(x=0.25,y=0.25)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimButton"",
                    ""type"": ""Button"",
                    ""id"": ""55e5b041-92d4-43f7-9362-c4cd468f0a36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CancelAim"",
                    ""type"": ""Button"",
                    ""id"": ""2516901d-80c5-4222-97b1-a39aaffb51bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d6c75d5c-cfd6-4f7c-8819-d1d9d08e2e4d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitToMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2301ef69-c89e-4b35-9c80-0379e393cafb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitToMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98e8d2ff-1ed0-4ad1-b475-b546cebef847"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ada9604b-71af-4db9-bc59-5a278b7c5453"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2fa751ef-8bf4-4a0c-8e66-c92911e8db56"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9eed935d-6397-4015-9370-08ed016d7f7c"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fd3ba89-b97e-4fb7-a31e-b9b990f89043"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e405a46-d17d-4234-8e0a-25a29a838f6c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72b88c2b-e622-484d-8c46-e2b706f026cd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d778effe-fda5-4727-8557-f866f7716d17"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65c5f48e-c745-4fd0-a119-8e206945d4bf"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2"",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebcc2f3b-18c3-4693-92ee-5e64484de3cb"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""b5ad4040-1f7a-4418-bfc9-0385b155fd9e"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""9984aec6-724e-495e-b77b-0a3f871fb9fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2ff3821d-280a-4691-a546-5ad6e0a8588b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e014d873-502d-45b7-90c0-eca745c5ae3c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_ExitToMenu = m_Gameplay.FindAction("ExitToMenu", throwIfNotFound: true);
        m_Gameplay_Restart = m_Gameplay.FindAction("Restart", throwIfNotFound: true);
        m_Gameplay_Zoom = m_Gameplay.FindAction("Zoom", throwIfNotFound: true);
        m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
        m_Gameplay_StartPos = m_Gameplay.FindAction("StartPos", throwIfNotFound: true);
        m_Gameplay_AimButton = m_Gameplay.FindAction("AimButton", throwIfNotFound: true);
        m_Gameplay_CancelAim = m_Gameplay.FindAction("CancelAim", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Back = m_Menu.FindAction("Back", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_ExitToMenu;
    private readonly InputAction m_Gameplay_Restart;
    private readonly InputAction m_Gameplay_Zoom;
    private readonly InputAction m_Gameplay_Aim;
    private readonly InputAction m_Gameplay_StartPos;
    private readonly InputAction m_Gameplay_AimButton;
    private readonly InputAction m_Gameplay_CancelAim;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ExitToMenu => m_Wrapper.m_Gameplay_ExitToMenu;
        public InputAction @Restart => m_Wrapper.m_Gameplay_Restart;
        public InputAction @Zoom => m_Wrapper.m_Gameplay_Zoom;
        public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
        public InputAction @StartPos => m_Wrapper.m_Gameplay_StartPos;
        public InputAction @AimButton => m_Wrapper.m_Gameplay_AimButton;
        public InputAction @CancelAim => m_Wrapper.m_Gameplay_CancelAim;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @ExitToMenu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnExitToMenu;
                @ExitToMenu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnExitToMenu;
                @ExitToMenu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnExitToMenu;
                @Restart.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRestart;
                @Zoom.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                @Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @StartPos.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartPos;
                @StartPos.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartPos;
                @StartPos.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartPos;
                @AimButton.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimButton;
                @AimButton.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimButton;
                @AimButton.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimButton;
                @CancelAim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelAim;
                @CancelAim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelAim;
                @CancelAim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelAim;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ExitToMenu.started += instance.OnExitToMenu;
                @ExitToMenu.performed += instance.OnExitToMenu;
                @ExitToMenu.canceled += instance.OnExitToMenu;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @StartPos.started += instance.OnStartPos;
                @StartPos.performed += instance.OnStartPos;
                @StartPos.canceled += instance.OnStartPos;
                @AimButton.started += instance.OnAimButton;
                @AimButton.performed += instance.OnAimButton;
                @AimButton.canceled += instance.OnAimButton;
                @CancelAim.started += instance.OnCancelAim;
                @CancelAim.performed += instance.OnCancelAim;
                @CancelAim.canceled += instance.OnCancelAim;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Back;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Back => m_Wrapper.m_Menu_Back;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Back.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IGameplayActions
    {
        void OnExitToMenu(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnStartPos(InputAction.CallbackContext context);
        void OnAimButton(InputAction.CallbackContext context);
        void OnCancelAim(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnBack(InputAction.CallbackContext context);
    }
}
