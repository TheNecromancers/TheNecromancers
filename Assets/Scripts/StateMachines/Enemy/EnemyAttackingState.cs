using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.WeaponLogic.GetComponent<CapsuleCollider>().enabled = true;
        stateMachine.WeaponLogic.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) { return; }

        FaceToPlayer(deltaTime);

        if (IsPlayingAnimation(stateMachine.Animator)) { return; }


        if (GetNormalizedTime(stateMachine.Animator, "Attack") >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        //FaceToPlayer(deltaTime);
    }

    public override void Exit()
    {
        stateMachine.WeaponLogic.GetComponent<CapsuleCollider>().enabled = false;
    }
}
