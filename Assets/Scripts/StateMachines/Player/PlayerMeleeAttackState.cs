using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack attack;
    private Vector3 direction;

    public PlayerMeleeAttackState(PlayerStateMachine stateMachine, int attackIndex, Vector3 direction) : base(stateMachine)
    {
        this.attack = stateMachine.Attacks[attackIndex];
        this.direction = direction;
    }

    public override void Enter()
    {
        direction = CalculateMovement();

        stateMachine.WeaponLogic.GetComponent<CapsuleCollider>().enabled = true;
        stateMachine.WeaponLogic.SetAttack(stateMachine.WeaponRight.Damage, stateMachine.WeaponRight.Knockback);

        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputManager.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
        }

        previousFrameTime = normalizedTime;

        if (direction != Vector3.zero)
            FaceMovementDirection(direction, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.WeaponLogic.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        if(direction == Vector3.zero)
        {
            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        }

        stateMachine.ForceReceiver.AddForce(direction * attack.Force);

        alreadyAppliedForce = true;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState
            (
            new PlayerMeleeAttackState(
                stateMachine,
                attack.ComboStateIndex,
                direction
                )
            );
    }

}