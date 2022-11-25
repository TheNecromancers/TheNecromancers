using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeduration = 0.1f;
    private const float AnimatorDumpTime = 0.1f;

    int currentWaypointIndex = 0;
    Vector3 nextPosition;
    float dwellTimeElapsed = 0;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeduration);
        currentWaypointIndex = stateMachine.LastWaypointIndex;
    }

    public override void Tick(float deltaTime)
    {
        if (IsInChaseRange())
        {
            if (IsInViewRange() || IsTooNearRange())
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }
        }

        dwellTimeElapsed += deltaTime;

        if (AtWaypoint())
        {
            dwellTimeElapsed = 0;
            CycleWaypoint();
        }

        if (dwellTimeElapsed < stateMachine.DwellTime)
        {
            Move(deltaTime);
            ResetAgentPath();
            stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDumpTime, deltaTime);
            return;
        }

        nextPosition = GetCurrentWaypoint();

        FaceTo(nextPosition, deltaTime);
        MoveTo(nextPosition, deltaTime);
       
        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDumpTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.LastWaypointIndex = currentWaypointIndex;
        ResetAgentPath();
    }

    private bool AtWaypoint()
    {
        return CheckDistanceSqr(stateMachine.transform.position, GetCurrentWaypoint(), 1f);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return stateMachine.PatrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = stateMachine.PatrolPath.GetNextIndex(currentWaypointIndex);
    }
}
