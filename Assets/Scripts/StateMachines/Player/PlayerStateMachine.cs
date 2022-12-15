using UnityEngine;
using TheNecromancers.Gameplay.Player;
using TheNecromancers.Combat;
using System.Collections;
using UnityEngine.SceneManagement;

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
        [field: SerializeField] public WeaponSO WeaponRightHand { get; private set; } = null;
        [field: SerializeField] public WeaponSO WeaponLeftHand { get; private set; } = null;
        [field: SerializeField] public Attack[] Attacks { get; private set; }
        [field: SerializeField] public GameObject RightHandHolder { get; private set; }
        [field: SerializeField] public GameObject LeftHandHolder { get; private set; }
        public WeaponLogic WeaponLogic { get; private set; } = null;

        public Transform MainCameraTransform { get; private set; }

        private void Start()
        {
            WeaponRightHand?.Equip(RightHandHolder.transform);
            WeaponLeftHand?.Equip(LeftHandHolder.transform);
            WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();
            InventoryManager.inventoryObject = inventoryObject;
            InventoryManager.displayInventory = InventoryUIManager;
            InventoryManager.playerStateMachine = this;

            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerLocomotionState(this));
            inventoryObject.playerStateMachine = gameObject.GetComponent<PlayerStateMachine>();
        }

        private void OnEnable()
        {

            Health.OnDie += HandleDie;
            InputManager.InteractEvent += HandleInteract;
            InputManager.InventoryEvent += InventoryUIManager.HandleInventoryInteraction;
        }

        private void OnDisable()
        {
            Health.OnDie -= HandleDie;
            InputManager.InteractEvent -= HandleInteract;
        }

        private void HandleDie()
        {
            SwitchState(new PlayerDeadState(this));
            StartCoroutine(Respawn());
        }

        void HandleInteract()
        {
            if (InteractionDetector.CurrentTarget != null)
            {
                InteractionDetector.CurrentTarget.OnInteract();
            }
        }
        private void OnApplicationQuit()
        {
            inventoryObject.Container.Clear();
        }

        public void OnWeaponChanged(WeaponSO _weapon)
        {
            WeaponLogic = _weapon.itemPrefab.GetComponent<WeaponLogic>();
            if (_weapon.WeaponType == WeaponType.LeftHand)
            {
                WeaponLeftHand = _weapon;
            }
            else if (_weapon.WeaponType == WeaponType.RightHand)
            {
                WeaponRightHand = _weapon;
            }
        }

        IEnumerator Respawn()
        {
            yield return new WaitForSeconds(2f);

            Health.Heal(10);

            SwitchState(new PlayerLocomotionState(this));

            Controller.enabled = false;
            transform.position = GameManager.Instance.LastCheckPointPos;
            Controller.enabled = true;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
