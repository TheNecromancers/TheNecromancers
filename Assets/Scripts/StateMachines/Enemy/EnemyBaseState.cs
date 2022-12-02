using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        Move(deltaTime);
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
        Move(deltaTime);
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

    protected bool IsInViewRange()
    {
        Vector3 toTarget = stateMachine.Player.transform.position - stateMachine.transform.position;

        Vector3 localDirection = stateMachine.transform.InverseTransformDirection(toTarget);

        RaycastHit hit;

        Debug.DrawRay(stateMachine.transform.position + Vector3.up, toTarget + Vector3.up, Color.red);
      
        float angle = Mathf.Atan2(localDirection.z, localDirection.x) * Mathf.Rad2Deg - 90;

        if (angle < stateMachine.ViewAngle && angle > -stateMachine.ViewAngle)
        {
            if (Physics.Raycast(stateMachine.transform.position + Vector3.up, toTarget + Vector3.up, out hit, Mathf.Infinity))
            {
               // Debug.Log(hit.collider.name);
                if (!hit.collider.CompareTag("Player")) return false;
                else return true;
            }
            return true;
        }
        return false;
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.Player.GetComponent<Health>().IsDead) { return false; }

        return CheckDistanceSqr(stateMachine.Player.transform.position, stateMachine.transform.position, stateMachine.PlayerChasingRange);
    }

    protected bool IsTooNearRange()
    {
        if (stateMachine.Player.GetComponent<Health>().IsDead) { return false; }

        return CheckDistanceSqr(stateMachine.Player.transform.position, stateMachine.transform.position, stateMachine.PlayerToNearChasingRange);
    }

    protected void ResetAgentPath()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }
}
