using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheNecromancers.StateMachine.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
        private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
        private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

        private const float CrossFadeDuration = 0.1f;

        Vector3 movement;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputManager.TargetEvent += OnTarget;
            stateMachine.InputManager.RollEvent += OnRoll;

            stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTree, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            movement = CalculateMovement();

            if (stateMachine.InputManager.IsAttacking)
            {
                stateMachine.SwitchState(new PlayerMeleeAttackState(stateMachine, 0, movement));
                return;
            }

            if (stateMachine.InputManager.IsBlocking)
            {
                stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
                return;
            }

            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
                return;
            }

            Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

            UpdateAnimator(deltaTime);

            FaceOnTarget(deltaTime);
        }

        public override void Exit()
        {
            stateMachine.InputManager.TargetEvent -= OnTarget;
            stateMachine.InputManager.RollEvent -= OnRoll;
        }

        private void OnTarget()
        {
            stateMachine.Targeter.Cancel();

            stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
        }


        private Vector3 CalculateMovement(float deltaTime)
        {
            Vector3 movement = new Vector3();

            movement += -stateMachine.transform.right * stateMachine.InputManager.MovementValue.x;
            movement += -stateMachine.transform.forward * stateMachine.InputManager.MovementValue.y;

            return movement;
        }

        private void UpdateAnimator(float deltaTime)
        {
            if (stateMachine.InputManager.MovementValue.y == 0)
            {
                stateMachine.Animator.SetFloat(TargetingForwardHash, 0f, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputManager.MovementValue.y > 0 ? 1f : -1f;
                stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
            }

            if (stateMachine.InputManager.MovementValue.x == 0)
            {
                stateMachine.Animator.SetFloat(TargetingRightHash, 0f, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputManager.MovementValue.x > 0 ? 1f : -1f;
                stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
            }
        }

        void OnRoll()
        {
            stateMachine.SwitchState(new PlayerRollState(stateMachine, movement));
            return;
        }
    }
}
