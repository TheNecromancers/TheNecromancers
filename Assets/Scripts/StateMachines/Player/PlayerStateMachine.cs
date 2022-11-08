using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("Controllers")]
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: Header("Movement Settings")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }

    [field: SerializeField, Header("Attack Settings")] public Attack[] Attacks { get; private set; }

    public Transform MainCameraTransform { get; private set; }


    private void Start()
    {
 
        Debug.Log("Use WASD for move the character");

        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerLocomotionState(this));
    }
}
