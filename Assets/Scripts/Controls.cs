//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Scripts/Controls.inputactions
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

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""45b6ef49-b89f-4fae-a8f4-19e8f2dd0782"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""cb3f6da2-d4b9-48f6-90e4-cfffe7bb8a4f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""6c98c5bd-97f9-4997-93bb-5b1cae8d98bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""60a6242d-d271-4a2a-a561-9bb5f275b80c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c2b9fd9f-c857-409a-a6d4-ad990ae01f3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""d3aaf622-9699-4c5c-9eec-5ed25b72bd11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CombactAbility"",
                    ""type"": ""Button"",
                    ""id"": ""5e60aa6a-fa88-46eb-9b66-7678b765bbbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ExplorationAbility"",
                    ""type"": ""Button"",
                    ""id"": ""8a4b5faf-9732-46e2-bd79-8ef04b38bc0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectNextTarget"",
                    ""type"": ""Button"",
                    ""id"": ""00672b64-705b-40e6-bb4e-d0ffba688414"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectPrevTarget"",
                    ""type"": ""Button"",
                    ""id"": ""078247b5-5082-4858-b10d-d90a3b0f3cba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectTarget"",
                    ""type"": ""Button"",
                    ""id"": ""70f9ab2b-68fd-4ead-9100-7dd8dff9879b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""6cf3f496-3df6-43f2-9130-1e3f343d2e40"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""561c7234-d4c7-42f5-8eb3-4dc085f44684"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b71f4c8d-3550-4645-aa16-15ca2e392941"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6c35db15-3056-49af-a4cd-ce418d6138fa"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cd87b372-8214-4e56-a420-eb5eb3638990"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""43037a7c-f2c1-4f6b-a9ac-88758d31b65d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb20be32-f3ce-45ff-b26c-953d542eb2f3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64fc2af9-fdac-4401-a9ce-166c1022ca07"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44145238-da88-40b2-8235-b86d78cc716e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bb829ba-effc-4480-87f4-fc0f91f62061"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8835eb6-3fd9-4f9c-925c-6232cee25991"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80279caa-8fea-4b74-b5a0-21fa61226e7c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""064801d9-d601-4849-bf3f-0c400b5bec9d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e0a6ef6-1811-498a-b88b-52df991b4724"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43124dd3-2e1d-44aa-8023-5b141c00bb94"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""CombactAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""917aeb05-e569-46d7-9a74-60b392a7b297"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CombactAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""327819c7-9b35-4487-8ac5-904c39b89158"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""ExplorationAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bada4a84-967c-4ac9-972e-d5e50d77a1e5"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ExplorationAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9edb5f4e-70b0-41ac-92e8-c9e5c3799363"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectPrevTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b4d39f1-222d-490f-8533-37c14bb7c3b4"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectNextTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21ec73c7-ca99-4651-8659-8c0b8833f614"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc5848f9-ea21-4cac-8eb7-2c2469fa75e9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""SelectTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIControls"",
            ""id"": ""7cc67bbf-23a9-44f4-8056-5cbe62984d80"",
            ""actions"": [
                {
                    ""name"": ""UIInventoryInteraction"",
                    ""type"": ""Button"",
                    ""id"": ""ebd23cbf-08c0-4072-9a39-70093948ce21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ResetInvetoryChest"",
                    ""type"": ""Button"",
                    ""id"": ""096a08c4-f421-44eb-9441-b0bc75a93827"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e7cc4d63-d484-4ce6-aead-2830f4ed6ef5"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""UIInventoryInteraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02e4b0e2-2810-409e-8ef3-1f1ea831ef79"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""UIInventoryInteraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cedab102-a64a-4c04-a885-db100b1d68c7"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & Keyboard"",
                    ""action"": ""ResetInvetoryChest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse & Keyboard"",
            ""bindingGroup"": ""Mouse & Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Roll = m_Player.FindAction("Roll", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Block = m_Player.FindAction("Block", throwIfNotFound: true);
        m_Player_CombactAbility = m_Player.FindAction("CombactAbility", throwIfNotFound: true);
        m_Player_ExplorationAbility = m_Player.FindAction("ExplorationAbility", throwIfNotFound: true);
        m_Player_SelectNextTarget = m_Player.FindAction("SelectNextTarget", throwIfNotFound: true);
        m_Player_SelectPrevTarget = m_Player.FindAction("SelectPrevTarget", throwIfNotFound: true);
        m_Player_SelectTarget = m_Player.FindAction("SelectTarget", throwIfNotFound: true);
        // UIControls
        m_UIControls = asset.FindActionMap("UIControls", throwIfNotFound: true);
        m_UIControls_UIInventoryInteraction = m_UIControls.FindAction("UIInventoryInteraction", throwIfNotFound: true);
        m_UIControls_ResetInvetoryChest = m_UIControls.FindAction("ResetInvetoryChest", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Roll;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Block;
    private readonly InputAction m_Player_CombactAbility;
    private readonly InputAction m_Player_ExplorationAbility;
    private readonly InputAction m_Player_SelectNextTarget;
    private readonly InputAction m_Player_SelectPrevTarget;
    private readonly InputAction m_Player_SelectTarget;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Roll => m_Wrapper.m_Player_Roll;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Block => m_Wrapper.m_Player_Block;
        public InputAction @CombactAbility => m_Wrapper.m_Player_CombactAbility;
        public InputAction @ExplorationAbility => m_Wrapper.m_Player_ExplorationAbility;
        public InputAction @SelectNextTarget => m_Wrapper.m_Player_SelectNextTarget;
        public InputAction @SelectPrevTarget => m_Wrapper.m_Player_SelectPrevTarget;
        public InputAction @SelectTarget => m_Wrapper.m_Player_SelectTarget;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Roll.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRoll;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Block.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @CombactAbility.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCombactAbility;
                @CombactAbility.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCombactAbility;
                @CombactAbility.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCombactAbility;
                @ExplorationAbility.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExplorationAbility;
                @ExplorationAbility.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExplorationAbility;
                @ExplorationAbility.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExplorationAbility;
                @SelectNextTarget.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectNextTarget;
                @SelectNextTarget.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectNextTarget;
                @SelectNextTarget.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectNextTarget;
                @SelectPrevTarget.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectPrevTarget;
                @SelectPrevTarget.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectPrevTarget;
                @SelectPrevTarget.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectPrevTarget;
                @SelectTarget.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTarget;
                @SelectTarget.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTarget;
                @SelectTarget.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTarget;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @CombactAbility.started += instance.OnCombactAbility;
                @CombactAbility.performed += instance.OnCombactAbility;
                @CombactAbility.canceled += instance.OnCombactAbility;
                @ExplorationAbility.started += instance.OnExplorationAbility;
                @ExplorationAbility.performed += instance.OnExplorationAbility;
                @ExplorationAbility.canceled += instance.OnExplorationAbility;
                @SelectNextTarget.started += instance.OnSelectNextTarget;
                @SelectNextTarget.performed += instance.OnSelectNextTarget;
                @SelectNextTarget.canceled += instance.OnSelectNextTarget;
                @SelectPrevTarget.started += instance.OnSelectPrevTarget;
                @SelectPrevTarget.performed += instance.OnSelectPrevTarget;
                @SelectPrevTarget.canceled += instance.OnSelectPrevTarget;
                @SelectTarget.started += instance.OnSelectTarget;
                @SelectTarget.performed += instance.OnSelectTarget;
                @SelectTarget.canceled += instance.OnSelectTarget;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UIControls
    private readonly InputActionMap m_UIControls;
    private IUIControlsActions m_UIControlsActionsCallbackInterface;
    private readonly InputAction m_UIControls_UIInventoryInteraction;
    private readonly InputAction m_UIControls_ResetInvetoryChest;
    public struct UIControlsActions
    {
        private @Controls m_Wrapper;
        public UIControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @UIInventoryInteraction => m_Wrapper.m_UIControls_UIInventoryInteraction;
        public InputAction @ResetInvetoryChest => m_Wrapper.m_UIControls_ResetInvetoryChest;
        public InputActionMap Get() { return m_Wrapper.m_UIControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUIControlsActions instance)
        {
            if (m_Wrapper.m_UIControlsActionsCallbackInterface != null)
            {
                @UIInventoryInteraction.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnUIInventoryInteraction;
                @UIInventoryInteraction.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnUIInventoryInteraction;
                @UIInventoryInteraction.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnUIInventoryInteraction;
                @ResetInvetoryChest.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnResetInvetoryChest;
                @ResetInvetoryChest.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnResetInvetoryChest;
                @ResetInvetoryChest.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnResetInvetoryChest;
            }
            m_Wrapper.m_UIControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UIInventoryInteraction.started += instance.OnUIInventoryInteraction;
                @UIInventoryInteraction.performed += instance.OnUIInventoryInteraction;
                @UIInventoryInteraction.canceled += instance.OnUIInventoryInteraction;
                @ResetInvetoryChest.started += instance.OnResetInvetoryChest;
                @ResetInvetoryChest.performed += instance.OnResetInvetoryChest;
                @ResetInvetoryChest.canceled += instance.OnResetInvetoryChest;
            }
        }
    }
    public UIControlsActions @UIControls => new UIControlsActions(this);
    private int m_MouseKeyboardSchemeIndex = -1;
    public InputControlScheme MouseKeyboardScheme
    {
        get
        {
            if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse & Keyboard");
            return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnCombactAbility(InputAction.CallbackContext context);
        void OnExplorationAbility(InputAction.CallbackContext context);
        void OnSelectNextTarget(InputAction.CallbackContext context);
        void OnSelectPrevTarget(InputAction.CallbackContext context);
        void OnSelectTarget(InputAction.CallbackContext context);
    }
    public interface IUIControlsActions
    {
        void OnUIInventoryInteraction(InputAction.CallbackContext context);
        void OnResetInvetoryChest(InputAction.CallbackContext context);
    }
}
