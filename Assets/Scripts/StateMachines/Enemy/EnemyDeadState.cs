using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Nemico Morto");
        //   stateMachine.Ragdoll.ToggleRagdoll(true);
    //        stateMachine.Weapon.gameObject.SetActive(false);
    //      GameObject.Destroy(stateMachine.Target);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}
