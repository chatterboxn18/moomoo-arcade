// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/AmongUs/controller/CharacterMovement.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CharacterMovement : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterMovement()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterMovement"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""306ae96f-7fd9-4cc9-b584-8cf2674808cb"",
            ""actions"": [
                {
                    ""name"": ""Movement-1"",
                    ""type"": ""Value"",
                    ""id"": ""7bcf5123-e638-4b58-9c65-6bc7aea178d9"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press""
                },
                {
                    ""name"": ""Movement-2"",
                    ""type"": ""Value"",
                    ""id"": ""52f83e80-2782-412d-b4c2-d0d5d0a53d7e"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press""
                },
                {
                    ""name"": ""GamePadMovement"",
                    ""type"": ""Value"",
                    ""id"": ""b8e93d1b-70fb-4e32-90a6-dc47f8317941"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b1f3b41a-e1cc-4418-b005-64ea19431211"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3759a83d-f9ba-481a-ae10-16ae2f492050"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""127e6a07-1125-460a-87bd-87302cb81533"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""15c7cf98-05fd-46d4-9333-46fbdae13fe9"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a7ca15a1-61bb-4d1e-9f5f-b4bbd89cb82b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8c42dfee-67db-442f-8207-21ad6c242f74"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f3d6c388-f7df-406f-8709-98a14f1ff479"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4dfda6ec-2688-47cf-bdae-b649538bf8f1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7a29978f-62c9-4be1-80f7-8aaef1c1afda"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1ed8e127-0ae9-42f1-a1d1-dee3834ca1e9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement-2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""55bf8f62-b1ca-4cb7-bf1d-86583d475283"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamePadMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d1a0693d-8d3b-4874-8f38-32e2477dae8d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamePadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a7a69f05-8af4-4be2-95fb-77b911da7702"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamePadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""22828e2a-2654-47de-a3c5-4f6580056c1a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamePadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ef0a4e21-049e-48b4-9023-6eb72d38dbe8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamePadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Movement1 = m_Movement.FindAction("Movement-1", throwIfNotFound: true);
        m_Movement_Movement2 = m_Movement.FindAction("Movement-2", throwIfNotFound: true);
        m_Movement_GamePadMovement = m_Movement.FindAction("GamePadMovement", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Movement1;
    private readonly InputAction m_Movement_Movement2;
    private readonly InputAction m_Movement_GamePadMovement;
    public struct MovementActions
    {
        private @CharacterMovement m_Wrapper;
        public MovementActions(@CharacterMovement wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement1 => m_Wrapper.m_Movement_Movement1;
        public InputAction @Movement2 => m_Wrapper.m_Movement_Movement2;
        public InputAction @GamePadMovement => m_Wrapper.m_Movement_GamePadMovement;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Movement1.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement1;
                @Movement1.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement1;
                @Movement1.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement1;
                @Movement2.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement2;
                @Movement2.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement2;
                @Movement2.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement2;
                @GamePadMovement.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnGamePadMovement;
                @GamePadMovement.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnGamePadMovement;
                @GamePadMovement.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnGamePadMovement;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement1.started += instance.OnMovement1;
                @Movement1.performed += instance.OnMovement1;
                @Movement1.canceled += instance.OnMovement1;
                @Movement2.started += instance.OnMovement2;
                @Movement2.performed += instance.OnMovement2;
                @Movement2.canceled += instance.OnMovement2;
                @GamePadMovement.started += instance.OnGamePadMovement;
                @GamePadMovement.performed += instance.OnGamePadMovement;
                @GamePadMovement.canceled += instance.OnGamePadMovement;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnMovement1(InputAction.CallbackContext context);
        void OnMovement2(InputAction.CallbackContext context);
        void OnGamePadMovement(InputAction.CallbackContext context);
    }
}
