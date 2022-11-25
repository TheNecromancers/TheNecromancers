using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CrossFadeduration = 0.1f;

    private float duration = 1f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        Debug.Log("Player Impact");
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeduration);
    }

    public override void Enter() { }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0)
        {
            stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
        }
    }

    public override void Exit()
    {
    }

   
}
