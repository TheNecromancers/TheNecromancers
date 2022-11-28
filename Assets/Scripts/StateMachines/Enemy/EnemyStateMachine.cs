using UnityEngine;
using UnityEngine.AI;
using TheNecromancers.Gameplay.AI;
using TheNecromancers.Combat;

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
        [field: SerializeField] public Target Target { get; private set; }

        [field: Header("Movement")]
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }

        [field: Header("Chasing And Patrolling")]
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        [field: SerializeField] public float PlayerToNearChasingRange { get; private set; }
        [field: SerializeField] public float ViewAngle { get; private set; }
        [field: SerializeField] public float SuspicionTime { get; private set; }
        [field: SerializeField] public float DwellTime { get; private set; }
        [field: SerializeField] public PatrolPath PatrolPath { get; private set; }

        [field: Header("Attack")]
        [field: SerializeField] public WeaponSO CurrentWeapon { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackRate { get; private set; }
        public WeaponLogic WeaponLogic { get; private set; }
       // [field: SerializeField] public int AttackDamage { get; private set; }
       // [field: SerializeField] public float AttackKnockback { get; private set; }
        [field: SerializeField] public GameObject RightHandHolder { get; private set; }

        public GameObject Player { get; private set; }
        public int LastWaypointIndex { get; set; }
        public Vector3 InitialPosition { get; set; }

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            InitialPosition = transform.position;

            CurrentWeapon?.Equip(RightHandHolder.transform);
            WeaponLogic = RightHandHolder.transform.GetComponentInChildren<WeaponLogic>();

            Agent.updatePosition = false;
            Agent.updateRotation = false;

            GoToGuardPosition();
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
        }

        private void HandleTakeDamage()
        {
           /* int balanceAccuracy = Random.Range(1, 7);

            Debug.Log("have lost balance " + balanceAccuracy);
            if(balanceAccuracy > 3)*/
            SwitchState(new EnemyImpactState(this));
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

        private void OnDrawGizmosSelected()
        {
            // Draw Chasing Range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);

            // Draw Too Near Range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerToNearChasingRange);
        }
    }
}
