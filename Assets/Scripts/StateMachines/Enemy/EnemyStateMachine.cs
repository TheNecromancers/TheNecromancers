using System;
using UnityEngine;
using UnityEngine.AI;
using TheNecromancers.Gameplay.AI;
using TheNecromancers.Combat;
using UnityEditorInternal;


namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: Header("Components")]
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public CooldownManager CooldownManager { get; private set; }
        [field: SerializeField] public ParticleFXManager ParticleFXManager { get; private set; }
        [field: SerializeField] public EnemyPresenter EnemyPresenter { get; private set; }
        [field: SerializeField] public Target Target { get; private set; }
        [field: Header("Data")]
        [field: SerializeField] public AudioClips AudioClips { get; private set; }

        [field: Header("Movement")]
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }

        [field: Header("Chasing And Patrolling")]
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        [field: SerializeField] public float PlayerTooNearChasingRange { get; private set; }
        [field: SerializeField] public float ViewAngle { get; private set; }
        [field: SerializeField] public float SuspicionTime { get; private set; }
        [field: SerializeField] public float DwellTime { get; private set; }
        [field: SerializeField] public PatrolPath PatrolPath { get; private set; }
        [field: SerializeField] public LayerMask PlayerLayerMask { get; private set; }

        [field: Header("Attack")]
        [field: SerializeField] public bool IsRanged { get; private set; }
        [field: SerializeField] public WeaponSO RangedWeapon { get; private set; }
        [field: SerializeField] public WeaponSO MeleeWeapon { get; private set; }
        [field: SerializeField] public float MeleeAttackRange { get; private set; }
        [field: SerializeField] public float RangedAttackRange { get; private set; }
        [field: SerializeField] public float AttackRate { get; private set; }
        [field: SerializeField] public float AttackForce { get; private set; }
        [field: SerializeField] public Transform ProjectileObj { get; private set; }
        public float AttackRange { get; set; }
        [field: SerializeField] public WeaponSO CurrentWeapon { get; set; }
        [field: SerializeField] public WeaponLogic WeaponLogic { get; private set; }

        // [field: SerializeField] public int AttackDamage { get; private set; }
        // [field: SerializeField] public float AttackKnockback { get; private set; }
        [field: SerializeField] public GameObject RightHandHolder { get; private set; }
        [field: SerializeField] public float StunDuration { get; private set; }

        public GameObject Player { get; private set; }
        public int HitsDamageTaked { get; set; }
        public int LastWaypointIndex { get; set; }
        public Vector3 InitialPosition { get; set; }

        private void Awake() 
        {
            SetStartingWeapon();
        }

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            InitialPosition = transform.position;

            Agent.updatePosition = false;
            Agent.updateRotation = false;


            GoToGuardPosition();
        }

        private void OnEnable()
        {
            Health.OnDie += HandleDie;
            Health.OnTakeDamage += HandleTakeDamage;

            WeaponLogic.OnTakeParry += HandleTakeParry;
        }

        private void OnDisable()
        {
            Health.OnDie -= HandleDie;
            Health.OnTakeDamage -= HandleTakeDamage;

            WeaponLogic.OnTakeParry -= HandleTakeParry;
        }

        private void HandleDie()
        {
            SwitchState(new EnemyDeadState(this));
        }

        public void GoToGuardPosition()
        {
            if (PatrolPath != null)
            {
                SwitchState(new EnemyPatrolState(this));
                return;
            }
            else
            {
                SwitchState(new EnemyIdleState(this));
                return;
            }
        }

        public void SetStartingWeapon()
        {
            CurrentWeapon = IsRanged ? RangedWeapon : MeleeWeapon;
            AttackRange = IsRanged ? RangedAttackRange : MeleeAttackRange;

            CurrentWeapon?.Equip(RightHandHolder.transform);
            WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();
        }

        public void UpdateEquippedWeapon(bool playerIsTooNear)
        {
            CurrentWeapon = playerIsTooNear ? MeleeWeapon : RangedWeapon;
            AttackRange = playerIsTooNear ? MeleeAttackRange : RangedAttackRange;

            CurrentWeapon?.Equip(RightHandHolder.transform);
            WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();
        }

        private void HandleTakeDamage()
        {
            HitsDamageTaked++;

            if (HitsDamageTaked >= 3)
            {
                SwitchState(new EnemyStunState(this));
                HitsDamageTaked = 0;
            }
        }

        private void HandleTakeParry()
        {
            AudioManager.Instance.PlayRandomClip(AudioClips.Parry);
            ParticleFXManager.PlayParticleFX(transform.position + Vector3.up * 2, ParticleFXManager.ParryParticleFX);
            WeaponLogic.GetComponent<CapsuleCollider>().enabled = false;

            SwitchState(new EnemyStunState(this));
        }

        // Animations Events
        private void OnStartShootAnim()
        {
            ParticleFXManager.PlayParticleFX(transform.position + Vector3.up, ParticleFXManager.AttackParticleFX);
        }

        private void OnShootAnim()
        {
            Transform projectile = GameObject.Instantiate(ProjectileObj);

            //Set projectile 
            projectile.transform.SetPositionAndRotation(RightHandHolder.transform.GetChild(0).transform.position, transform.rotation);
            projectile.SetParent(transform);
            CooldownManager.BeginCooldown("ProjectileShoot", AttackRate);
            // ParticleFXManager.PlayParticleFX(RightHandHolder.transform.position, ParticleFXManager.AttackParticleFX);
        }

        private void OnStartAttackAnim()
        {
            ParticleFXManager.PlayParticleFX(RightHandHolder.transform.position, ParticleFXManager.AttackParticleFX);
            AudioManager.Instance.PlayRandomClip(AudioClips.Attacks);
        }

        private void OnHitAnim()
        {
            WeaponLogic.GetComponent<CapsuleCollider>().enabled = true;
           // ParticleFXManager.PlayParticleFX(RightHandHolder.transform.position, ParticleFXManager.HitParticleFX);
        }

        private void OnEndAttackAnim()
        {
            WeaponLogic.GetComponent<CapsuleCollider>().enabled = false;
        }

        private void OnDrawGizmosSelected()
        {
            AttackRange = IsRanged ? RangedAttackRange : MeleeAttackRange;

            // Draw Chasing Range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);

            // Draw Too Near Range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerTooNearChasingRange);

            // Draw MeleeAttack Range
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, AttackRange);

        }
    }
}
