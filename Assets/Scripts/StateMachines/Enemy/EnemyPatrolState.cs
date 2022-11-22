using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeduration = 0.1f;
    private const float AnimatorDumpTime = 0.1f;

    Vector3 LastWaypoint;
    Vector3 NextWaypoint;
    int pathIndex = 0;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeduration);
        NextWaypoint = stateMachine.PathWaypoints[0].transform.position;
       // LastWaypoint = stateMachine.PathWaypoints[^1].transform.position;
    }

    public override void Tick(float deltaTime)
    {
        if(IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        MoveTo(stateMachine.PathWaypoints[pathIndex].transform.position, false, deltaTime);

        if (Vector3.Distance(stateMachine.transform.position, NextWaypoint) < 1f)
        {
            pathIndex++;

            if (NextWaypoint == stateMachine.PathWaypoints[^1].transform.position)
            {
                pathIndex = 0;
            }
        }

        NextWaypoint = stateMachine.PathWaypoints[pathIndex].transform.position;
        FaceTo(NextWaypoint);
        stateMachine.Animator.SetFloat(SpeedHash, 0.3f, AnimatorDumpTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.LastWaypoint = LastWaypoint;
    }

   

}
