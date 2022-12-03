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

        [field: Header("Movement Settings")]
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float RollForce { get; private set; }
        [field: SerializeField] public float RollDuration { get; private set; }

        [field: Header("Attack Settings")]
        [field: SerializeField] public WeaponSO WeaponRightHand { get; private set; } = null;
        [field: SerializeField] public WeaponSO WeaponLeftHand { get; private set; } = null;
        [field: SerializeField] public Attack[] Attacks { get; private set; }
        [field: SerializeField] public GameObject RightHandHolder { get; private set; }
        [field: SerializeField] public GameObject LeftHandHolder { get; private set; }
        public WeaponLogic WeaponLogic { get; private set; } = null;
        public WeaponLogic ShieldLogic { get; private set; } = null;
        public Transform MainCameraTransform { get; private set; }

        private void Start()
        {
            WeaponRightHand?.Equip(RightHandHolder.transform);
            WeaponLeftHand?.Equip(LeftHandHolder.transform);
            WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();

            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerLocomotionState(this));
        }

        private void OnEnable()
        {
            Health.OnDie += HandleDie;
            InputManager.InteractEvent += HandleInteract;
        }

        private void OnDisable()
        {
            Health.OnDie -= HandleDie;
            InputManager.InteractEvent -= HandleInteract;
        }

        private void HandleDie()
        {
            SwitchState(new PlayerDeadState(this));
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
            Debug.Log("Parry");
            Health.SetInvulnerable(true);
        }

        void OnEndParry()
        {
            Debug.Log("End Parry");
            Health.SetInvulnerable(false);
        }
    }
}
