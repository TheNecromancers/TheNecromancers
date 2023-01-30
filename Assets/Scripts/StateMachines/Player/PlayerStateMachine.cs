using UnityEngine;
using TheNecromancers.Gameplay.Player;
using TheNecromancers.Combat;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
//using UnityEditorInternal;

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
        [field: SerializeField] public DisplayInventory DisplayInventory { get; private set; }
        [field: SerializeField] public InventoryManager InventoryManager { get; private set; }
        [field: Header("Data")]
        [field: SerializeField] public AudioClips AudioClips { get; private set; }

        [field: Header("Movement Settings")]
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float RollForce { get; private set; }
        [field: SerializeField] public float RollDuration { get; private set; }

        [field: Header("Attack Settings")]
        [field: SerializeField] public WeaponSO WeaponRightHand { get; set; } = null;
        [field: SerializeField] public WeaponSO WeaponLeftHand { get; set; } = null;
        [field: SerializeField] public GameObject RightHandHolder { get; private set; }
        [field: SerializeField] public GameObject LeftHandHolder { get; private set; }
        [field: SerializeField] public GameObject SlashVFX { get; private set; }
        public WeaponLogic WeaponLogic { get; set; } = null;
        public Attack[] Attacks { get; set; }

        public WeaponLogic ShieldLogic { get; private set; } = null;
        public Transform MainCameraTransform { get; private set; }

        [field: Header("Persistence")]

        public string savePath;
        public Vector3 LastSpawnPosition { get => lastSpawnPosition; set { lastSpawnPosition = value; } }
        private Vector3 lastSpawnPosition;

        public List<Chest> chests;

        private void Awake()
        {
            //For persistence 
            Load();

            DisplayInventory = FindObjectOfType<DisplayInventory>();

            InputManager = GetComponent<InputManager>();
            Controller = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            ForceReceiver= GetComponent<ForceReceiver>();
            Health = GetComponent<Health>();

            AbilitySystemManager = GetComponentInChildren<AbilitySystemManager>();
            InteractionDetector = GetComponentInChildren<InteractionDetector>();
            Targeter = GetComponentInChildren<Targeter>();
            InventoryManager = GetComponentInChildren<InventoryManager>();

            RightHandHolder = GameObject.FindGameObjectWithTag("RightSlot");
            LeftHandHolder = GameObject.FindGameObjectWithTag("LeftSlot");

            #if UNITY_EDITOR
            inventoryObject = (InventoryObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/SO_Scriptable Objects/InventorySO/Resources/Empty Inventory.asset",
                typeof(InventoryObject));
            AudioClips = (AudioClips)AssetDatabase.LoadAssetAtPath("Assets/Scripts/SO_Scriptable Objects/AudioSO/Resources/PlayerAudioData.asset",
                typeof(AudioClips));
            SlashVFX = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/slash5-HungNguyen/prefab/slash/white-yellow bolder.prefab",
                typeof(GameObject));
            #else
                inventoryObject = Resources.Load<InventoryObject>("Empty Inventory");
                AudioClips = Resources.Load<AudioClips>("PlayerAudioData");
                SlashVFX = Resources.Load<GameObject>("slash5-HungNguyen/prefab/slash/white-yellow bolder");
            #endif
        }


        private void Start()
        {
            MainCameraTransform = Camera.main.transform;

            if (WeaponRightHand != null || WeaponLeftHand != null)
            {
                WeaponRightHand?.Equip(RightHandHolder.transform);
                Attacks = WeaponRightHand.Attacks;
                WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();
                WeaponLeftHand?.Equip(LeftHandHolder.transform);
            }

            InventoryManager.inventoryObject = inventoryObject;
            InventoryManager.displayInventory = DisplayInventory;
            InventoryManager.playerStateMachine = this;
            inventoryObject.playerStateMachine = gameObject.GetComponent<PlayerStateMachine>();

            SwitchState(new PlayerLocomotionState(this));
        }

        private void OnEnable()
        {
            Health.OnDie += HandleDie;
            InputManager.InventoryEvent += DisplayInventory.HandleInventoryInteraction;
            InputManager.CombactAbilityEvent += OnCombactAbility;
            InputManager.ExplorationAbilityEvent += OnExplorationAbility;
            InputManager.ResetInventoryChestEvent += OnResetInventoryChest;
        }

        void SearchChest()
        {
            var Chests = FindObjectsOfType<Chest>();
            for (int f = 0; f < Chests.Length; f++)
            {
                chests.Add(Chests[f]);
            }
        }

        void OnResetInventoryChest()
        {
            Debug.Log("Reset");
            inventoryObject.Container.Clear();

            SearchChest();

            for (int i = 0; i < chests.Count; i++)
            {
                chests[i].ResetChest();
            }

        }

        private void HandleDie()
        {
            SwitchState(new PlayerDeadState(this));
            StartCoroutine(Respawn());
        }

        private void OnDisable()
        {
            Health.OnDie -= HandleDie;
            InputManager.CombactAbilityEvent -= OnCombactAbility;
            InputManager.ExplorationAbilityEvent -= OnExplorationAbility;
            InputManager.InventoryEvent -= DisplayInventory.HandleInventoryInteraction;
            //InputManager.ResetInventoryChestEvent -= OnResetInventoryChest;
        }

        void OnCombactAbility()
        {
            if (AbilitySystemManager != null)
            {
                AudioManager.Instance.PlayRandomClip(AudioClips.PowerUp2);
                AbilitySystemManager.OnCombactAbility(transform.position);
            }
        }

        void OnExplorationAbility()
        {
            if (AbilitySystemManager != null)
            {
                AudioManager.Instance.PlayRandomClip(AudioClips.PowerUp1);
                AbilitySystemManager.OnExplorationAbility();
            }
        }

        public void SetPlayerPosition(Vector3 position)
        {
            Controller.enabled= false;
            transform.position = position;
            Controller.enabled = true;
        }

        IEnumerator Respawn()
        {
            inventoryObject.Save();

            yield return new WaitForSeconds(2f);

            Health.RestoreLife();

            SwitchState(new PlayerLocomotionState(this));

            Controller.enabled = false;
            transform.position = lastSpawnPosition;
            Controller.enabled = true;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            inventoryObject.Load();
        }

        public bool HasShield()
        {
            return LeftHandHolder != null && WeaponLeftHand != null;
        }

        //Animations Events
        void OnStartAttackAnim() 
        {
            AudioManager.Instance.PlayRandomClip(AudioClips.Attacks);
            Vector3 spawnPos = transform.position + (transform.forward * 1.5f) + Vector3.up;
            var slashvfx = Instantiate(SlashVFX, spawnPos, RightHandHolder.transform.rotation);
            slashvfx.transform.SetParent(transform);
            Destroy(slashvfx.gameObject, 1);
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
            if (HasShield())
            {
                LeftHandHolder.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                Health.SetInvulnerable(true);
            }
        }

        void OnEndParry()
        {
            if (HasShield())
            {
                LeftHandHolder.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                Health.SetInvulnerable(false);
            }
        }

        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new();
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
            }
            FileStream file = File.Create(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath));
            bf.Serialize(file, saveData);
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath)))
            {
                BinaryFormatter bf = new();
                FileStream file = File.Open(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
            }
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        /* 
        public void EquipmentChange(WeaponSO _weapon)
        {
            Debug.Log("weapon changed");
            if(_weapon.WeaponType == WeaponType.LeftHand)
            {
                
                WeaponLeftHand = _weapon;
            }
            else if(_weapon.WeaponType == WeaponType.RightHand)
            {
                WeaponRightHand = _weapon;
                WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();
            }
        } */
    }
}
