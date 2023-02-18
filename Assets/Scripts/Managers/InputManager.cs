using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.IPlayerActions, Controls.IUIControlsActions
{
    public Vector2 MovementValue { get; private set; }
    public bool IsAttacking { get; private set; }
    private bool isInventoryOpened{ get; set; } =false;
    private bool isPauseMenuOpened{ get; set; } =false;

    public event Action RollEvent;
    public event Action InteractEvent;

    public event Action CombactAbilityEvent;
    public event Action ExplorationAbilityEvent;

    public event Action BlockEvent;

    public event Action TargetEvent;
    public event Action NextTargetEvent;
    public event Action PrevTargetEvent;

    public event Action InventoryEvent;
    public event Action ResetInventoryChestEvent;
    public event Action PauseMenuEvent;

    private Controls controls;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
        controls.UIControls.SetCallbacks(this);
        controls.UIControls.Enable();    }

    private void OnDestroy()
    {
        // The player not longer destroyed
        //controls.Player.Disable();
        //controls.UIControls.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(HandleAttackClick());
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        BlockEvent?.Invoke();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        RollEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        InteractEvent?.Invoke();
    }

    public void OnCombactAbility(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        CombactAbilityEvent?.Invoke();
    }

    public void OnExplorationAbility(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        ExplorationAbilityEvent?.Invoke();
    }

    public void OnSelectTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    IEnumerator HandleAttackClick()
    {
        IsAttacking = true;
        yield return new WaitForSeconds(0.1f);
        IsAttacking = false;
    }
    public void OnSelectPrevTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        PrevTargetEvent?.Invoke();
    }

    public void OnSelectNextTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        NextTargetEvent?.Invoke();
    }
    public void OnUIInventoryInteraction(InputAction.CallbackContext context)
    {
        if(!isPauseMenuOpened)
        {
            
            if (!context.performed) { return; }
            {
                InventoryStateChange();
                InventoryEvent?.Invoke();
            }
        }
    }
    
    public void InventoryStateChange()
    {
        isInventoryOpened = !isInventoryOpened;

        if(isInventoryOpened)
        {
            DisablePlayerControls();
        }
        else
        {
            EnablePlayerControls();
        }
    }

    public void PauseMenuStateChange()
    {
        isPauseMenuOpened = !isPauseMenuOpened;
                
        if(controls.Player.enabled)
        {
            DisablePlayerControls();
        }
        else
        {
            EnablePlayerControls();
        }
    }

    public void DisablePlayerControls()
    {
        controls.Player.Disable();
    }

    public void EnablePlayerControls()
    {
        controls.Player.Enable();
    }
    public void DisableUIControls()
    {
        controls.UIControls.Disable();
    }
        public void EnableUIControls()
    {
        controls.UIControls.Enable();
    }

    public void OnResetInvetoryChest(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        ResetInventoryChestEvent?.Invoke();
    }

    public void OnUIPauseMenuInteraction(InputAction.CallbackContext context)
    {
        if(isInventoryOpened)
        {
            OnUIInventoryInteraction(context);
        }
        else if(!isInventoryOpened)
        {
            if (!context.performed) { return; }
            {
                PauseMenuEvent?.Invoke();
            }
        }

    }
}
