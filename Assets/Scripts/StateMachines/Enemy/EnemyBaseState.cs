using ClipperLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    protected void MoveTo(Vector3 position, float deltaTime)
    {
        AgentMoveTo(position, deltaTime);
    }

    private void AgentMoveTo(Vector3 position, float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    protected void FaceToPlayer(float deltaTime)
    {
        if (stateMachine.Player == null) { return; }

        SmoothRotation(stateMachine.Player.transform.position, deltaTime);
    }
  
    protected void FaceTo(Vector3 position, float deltaTime)
    {
        SmoothRotation(position, deltaTime);
    }

    private void SmoothRotation(Vector3 target, float deltaTime)
    {
        Vector3 lookPos = target - stateMachine.transform.position;
        lookPos.y = 0f;

        var targetRotation = Quaternion.LookRotation(lookPos);

        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.RotationSpeed * deltaTime);
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.Player.GetComponent<Health>().IsDead) { return false; }

        return CheckDistanceSqr(stateMachine.Player.transform.position, stateMachine.transform.position, stateMachine.PlayerChasingRange);
    }


    protected void ResetAgentPath()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

}
