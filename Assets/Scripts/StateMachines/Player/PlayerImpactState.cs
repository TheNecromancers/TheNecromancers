using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CrossFadeduration = 0.1f;

    private float duration = 0.5f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeduration);
    }

    public override void Enter() { }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputManager.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerMeleeAttackState(stateMachine, 0, Vector3.zero));
            return;
        }

        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0)
        {
            ReturnToLocomotion();
            return;
        }
    }

    public override void Exit() { }
   
}
