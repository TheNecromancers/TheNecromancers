using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheNecromancers.StateMachine.Player
{
    public class PlayerInteractingState : PlayerBaseState
    {
        private readonly int InteractHash = Animator.StringToHash("Interact");
        private const float CrossFadeDuration = 0.1f;

        public PlayerInteractingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(InteractHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Interact");
            if(normalizedTime > 1f)
            {
                ReturnToLocomotion();
                return;
            }
        }

        public override void Exit()
        {
            stateMachine.InteractionDetector.CurrentTarget = null;
        }
    }
}
