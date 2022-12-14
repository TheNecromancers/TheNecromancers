using UnityEngine;
using TheNecromancers.Gameplay.Player;
using TheNecromancers.Combat;

namespace TheNecromancers.StateMachine.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [field: Header("Controllers")]
        [field: SerializeField] public InputManager InputManager { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public InteractionDetector InteractionDetector { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        [field: SerializeField] public AbilitySystemManager AbilitySystemManager { get; private set; }
        [field: SerializeField] public InventoryObject inventoryObject { get; private set; }
        [field: SerializeField] public DisplayInventory InventoryUIManager { get; private set; }
        [field: SerializeField] public InventoryManager InventoryManager { get; private set; }

        [field: Header("Movement Settings")]
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float RollForce { get; private set; }
        [field: SerializeField] public float RollDuration { get; private set; }

        [field: Header("Attack Settings")]
        [field: SerializeField] public WeaponSO WeaponRightHand { get;  set; } = null;
        [field: SerializeField] public WeaponSO WeaponLeftHand { get;  set; } = null;
        [field: SerializeField] public Attack[] Attacks { get; private set; }
        [field: SerializeField] public GameObject RightHandHolder { get; private set; }
        [field: SerializeField] public GameObject LeftHandHolder { get; private set; }
        public WeaponLogic WeaponLogic { get;  set; } = null;
        public WeaponLogic ShieldLogic { get; private set; } = null;
        public Transform MainCameraTransform { get; private set; }


        private void Awake() 
        {
            InventoryUIManager = FindObjectOfType<DisplayInventory>();
    
        }
        private void Start()
        {
            InputManager.CombactAbilityEvent += OnCombactAbility;
            InputManager.ExplorationAbilityEvent += OnExplorationAbility;
            WeaponRightHand?.Equip(RightHandHolder.transform);
            WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();
            WeaponLeftHand?.Equip(LeftHandHolder.transform);
            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerLocomotionState(this));

            InventoryManager.inventoryObject = inventoryObject;
            InventoryManager.displayInventory = InventoryUIManager;
            InventoryManager.playerStateMachine = this;
            inventoryObject.playerStateMachine=gameObject.GetComponent<PlayerStateMachine>();


        }

        private void OnEnable()
        {
            Health.OnDie += HandleDie;
            InputManager.InteractEvent += HandleInteract;
            InputManager.InventoryEvent += InventoryUIManager.HandleInventoryInteraction;

        }


        private void HandleDie()
        {
            SwitchState(new PlayerDeadState(this));
        }

        private void OnDisable()
        {
            Health.OnDie -= HandleDie;
            InputManager.CombactAbilityEvent -= OnCombactAbility;
            InputManager.ExplorationAbilityEvent -= OnExplorationAbility;
            InputManager.InteractEvent -= HandleInteract;
            InputManager.InventoryEvent -= InventoryUIManager.HandleInventoryInteraction;

        }
        private void OnApplicationQuit() 
        {
            inventoryObject.Container.Clear();
        }
        
        void HandleInteract()
        {
            if (InteractionDetector.CurrentTarget != null)
            {
                InteractionDetector.CurrentTarget.OnInteract();
                SwitchState(new PlayerInteractingState(this));
            }
        }

        //Animations Events
        void OnStartAttackAnim()
        {
        }

        void OnHitAnim()
        {
            WeaponLogic.GetComponent<CapsuleCollider>().enabled = true;
        }

        void OnEndAttackAnim()
        {
            WeaponLogic.GetComponent<CapsuleCollider>().enabled = false;
        }

        void OnStartParry()
        {
            Debug.Log("Start Parry");
            LeftHandHolder.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            Health.SetInvulnerable(true);
        }

    void OnCombactAbility()
    {
        if (AbilitySystemManager != null)
        {
            AbilitySystemManager.OnCombactAbility(transform.position);
        }
    }

    void OnExplorationAbility()
    {
            if (AbilitySystemManager != null)
            {
                AbilitySystemManager.OnExplorationAbility();
            }
    }
        void OnEndParry()
        {
            Debug.Log("End Parry");
            LeftHandHolder.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            Health.SetInvulnerable(false);
        }
    }
}
