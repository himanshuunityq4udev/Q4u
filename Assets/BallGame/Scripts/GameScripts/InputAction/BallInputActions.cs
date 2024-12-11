//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/BallGame/Scripts/GameScripts/InputAction/BallInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @BallInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @BallInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BallInputActions"",
    ""maps"": [
        {
            ""name"": ""BallControls"",
            ""id"": ""01efb486-5d47-4c94-985a-bc80c1057a55"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3b0cd742-1b01-4f5b-91e2-bea72f116484"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Swipe"",
                    ""type"": ""Value"",
                    ""id"": ""02fdbeae-a440-4584-8da8-995b951eb499"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b12b1584-33bc-4fea-a3bb-165837b138f7"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1aa8bef-ba62-4a8f-92c6-7a53c7b832c1"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e645fcb2-b603-45eb-9390-4549c5a922d6"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2da278d8-78ed-4871-8e14-2ead6eeb2ee5"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BallControls
        m_BallControls = asset.FindActionMap("BallControls", throwIfNotFound: true);
        m_BallControls_Move = m_BallControls.FindAction("Move", throwIfNotFound: true);
        m_BallControls_Swipe = m_BallControls.FindAction("Swipe", throwIfNotFound: true);
    }

    ~@BallInputActions()
    {
        UnityEngine.Debug.Assert(!m_BallControls.enabled, "This will cause a leak and performance issues, BallInputActions.BallControls.Disable() has not been called.");
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // BallControls
    private readonly InputActionMap m_BallControls;
    private List<IBallControlsActions> m_BallControlsActionsCallbackInterfaces = new List<IBallControlsActions>();
    private readonly InputAction m_BallControls_Move;
    private readonly InputAction m_BallControls_Swipe;
    public struct BallControlsActions
    {
        private @BallInputActions m_Wrapper;
        public BallControlsActions(@BallInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_BallControls_Move;
        public InputAction @Swipe => m_Wrapper.m_BallControls_Swipe;
        public InputActionMap Get() { return m_Wrapper.m_BallControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BallControlsActions set) { return set.Get(); }
        public void AddCallbacks(IBallControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_BallControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BallControlsActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Swipe.started += instance.OnSwipe;
            @Swipe.performed += instance.OnSwipe;
            @Swipe.canceled += instance.OnSwipe;
        }

        private void UnregisterCallbacks(IBallControlsActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Swipe.started -= instance.OnSwipe;
            @Swipe.performed -= instance.OnSwipe;
            @Swipe.canceled -= instance.OnSwipe;
        }

        public void RemoveCallbacks(IBallControlsActions instance)
        {
            if (m_Wrapper.m_BallControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBallControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_BallControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BallControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BallControlsActions @BallControls => new BallControlsActions(this);
    public interface IBallControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSwipe(InputAction.CallbackContext context);
    }
}