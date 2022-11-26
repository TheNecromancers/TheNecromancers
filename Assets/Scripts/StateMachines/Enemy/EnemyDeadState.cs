using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private readonly int DieHash = Animator.StringToHash("Die");
    private const float CrossFadeduration = 0.1f;
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Nemico Morto");
        stateMachine.Animator.CrossFadeInFixedTime(DieHash, CrossFadeduration);
        stateMachine.WeaponLogic.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
    }
}
