using UnityEngine;

namespace TheNecromancers.StateMachine.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TargetingBlendTreeHash = Animator.StringToHash("Targeting");
        private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
        private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

        private const float CrossFadeDuration = 0.1f;

        Vector3 movement;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
            
            stateMachine.InputManager.RollEvent += OnRoll;
            stateMachine.InputManager.BlockEvent += OnBlock;

            stateMachine.InputManager.TargetEvent += OnTarget;
            stateMachine.InputManager.NextTargetEvent += OnNextTarget;
            stateMachine.InputManager.PrevTargetEvent += OnPrevTarget;

            stateMachine.Health.OnTakeDamage += HandleTakeDamage;
        }

        public override void Tick(float deltaTime)
        {
            movement = CalculateMovement();

            if (stateMachine.InputManager.IsAttacking)
            {
                stateMachine.SwitchState(new PlayerMeleeAttackState(stateMachine, 0, movement));
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
            stateMachine.InputManager.RollEvent -= OnRoll;
            stateMachine.InputManager.BlockEvent -= OnBlock;

            stateMachine.InputManager.TargetEvent -= OnTarget;
            stateMachine.InputManager.NextTargetEvent -= OnNextTarget;
            stateMachine.InputManager.PrevTargetEvent -= OnPrevTarget;

            stateMachine.Health.OnTakeDamage -= HandleTakeDamage;
        }

        private void OnTarget()
        {
            stateMachine.Targeter.Cancel();

            stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
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
        void OnBlock()
        {
            Debug.Log("Blocco!");
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        void OnNextTarget()
        {
            stateMachine.Targeter.GetNextTarget();
        }
        
        void OnPrevTarget()
        {
            stateMachine.Targeter.GetPrevTarget();
        }
    }
}
