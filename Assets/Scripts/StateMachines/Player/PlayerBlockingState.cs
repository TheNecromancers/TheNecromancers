using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");

    private const float CrossFadeDuration = 0.1f;
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (!stateMachine.InputManager.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }

  
}