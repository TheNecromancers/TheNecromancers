using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    private readonly int DashBlendTreeHash = Animator.StringToHash("Roll");
    private readonly int DirXHash = Animator.StringToHash("DirX");
    private readonly int DirYHash = Animator.StringToHash("DirY");

    private const float CrossFadeDuration = 0.3f;

    Vector3 direction;
    private float remainingRollTime;

    public PlayerRollState(PlayerStateMachine stateMachine, Vector3 direction) : base(stateMachine) 
    {
        this.direction = direction;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DashBlendTreeHash, CrossFadeDuration);

        remainingRollTime = stateMachine.RollDuration;
    }


    public override void Tick(float deltaTime)
    {
        remainingRollTime -= deltaTime;

        if (remainingRollTime <= 0f)
        {
            ReturnToLocomotion();
            return;
        }

        if (direction == Vector3.zero)
        {
            Move(stateMachine.transform.forward * stateMachine.RollForce, deltaTime);
            return;
        }

        Move(direction.normalized * stateMachine.RollForce, deltaTime);

        FaceMovementDirection(direction, deltaTime);
    }

    public override void Exit()
    {

    }
}
