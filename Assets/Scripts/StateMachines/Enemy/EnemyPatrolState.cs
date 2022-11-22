using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeduration = 0.1f;
    private const float AnimatorDumpTime = 0.1f;

    Vector3 lastWaypoint;
    Vector3 nextWaypoint;
    float nextWaypointDistanceSqr;
    int pathIndex = 0;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeduration);
        nextWaypoint = stateMachine.PathWaypoints[0].transform.position;
    }

    public override void Tick(float deltaTime)
    {
        if(IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        MoveTo(stateMachine.PathWaypoints[pathIndex].transform.position, false, deltaTime);

        nextWaypointDistanceSqr = (nextWaypoint - stateMachine.transform.position).sqrMagnitude;
            
        if (nextWaypointDistanceSqr <= 1 )
        {
            pathIndex++;

            if (nextWaypoint == stateMachine.PathWaypoints[^1].transform.position)
            {
                pathIndex = 0;
            }
        }

        nextWaypoint = stateMachine.PathWaypoints[pathIndex].transform.position;

        FaceTo(nextWaypoint);
        stateMachine.Animator.SetFloat(SpeedHash, 0.3f, AnimatorDumpTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.LastWaypoint = lastWaypoint;
    }
}
