using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("Controllers")]
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public InteractionDetector InteractionDetector { get; private set; }

    [field: Header("Movement Settings")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float RollForce { get; private set; }
    [field: SerializeField] public float RollDuration { get; private set; }
    [field: Header("Attack Settings")]
    [field: SerializeField] public WeaponSO WeaponRight { get; private set; } = null;
    [field: SerializeField] public WeaponSO WeaponLeft { get; private set; } = null;
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public GameObject RightHandHolder { get; private set; }
    [field: SerializeField] public GameObject LeftHandHolder { get; private set; }
    [field: SerializeField] public WeaponLogic WeaponLogic { get; private set; } = null;

    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        InputManager.InteractEvent += OnInteract;

        WeaponRight?.Equip(RightHandHolder.transform);
        WeaponLeft?.Equip(LeftHandHolder.transform);

        WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();

        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerLocomotionState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
        InputManager.InteractEvent -= OnInteract;

    }

    private void HandleTakeDamage()
    {
      //  SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
     //   SwitchState(new PlayerDeadState(this));
    }
    
    void OnInteract()
    {
        if (InteractionDetector.currentTarget != null)
        {
            InteractionDetector.currentTarget.OnInteract();
        }
    } 
}
