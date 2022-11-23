using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: Header("Components")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public CooldownManager CooldownManager { get; private set; }

    //  [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    //  [field: SerializeField] public Target Target { get; private set; }
    //  [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: Header("Movement")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: Header("Chasing And Patrolling")]
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float SuspicionTime { get; private set; }
    [field: SerializeField] public float DwellingTime { get; private set; }
    [field: SerializeField] public PatrolPath PatrolPath { get; private set; }

    [field: Header("Attack")]
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }


    public GameObject Player { get; private set; }
    public int LastWaypointIndex { get; set; }
    public Vector3 InitialPosition { get; set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InitialPosition = transform.position;

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        if (PatrolPath != null)
            SwitchState(new EnemyPatrolState(this));
        else
            SwitchState(new EnemyIdleState(this));
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
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

}
