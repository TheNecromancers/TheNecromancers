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
        float forwardAmount;
        float rightAmount;

        float nextStep;
        float stepRate = 0.7f;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputManager.BlockEvent += OnBlock;
            stateMachine.InputManager.RollEvent += OnRoll;

            stateMachine.InputManager.TargetEvent += OnTarget;
            stateMachine.InputManager.NextTargetEvent += OnNextTarget;
            stateMachine.InputManager.PrevTargetEvent += OnPrevTarget;

            stateMachine.Health.OnTakeDamage += HandleTakeDamage;

            stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            movement = CalculateMovement();

            if (stateMachine.InputManager.IsAttacking && CanAttack())
            {
                stateMachine.SwitchState(new PlayerMeleeAttackState(stateMachine, 0, movement));
                return;
            }

            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
                return;
            }

            movement.Normalize();

            ConvertDirection(movement);

            Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

            PlayFootSteps();
            UpdateAnimator();
            FaceOnTarget(deltaTime);
        }

        public override void Exit()
        {
            stateMachine.InputManager.BlockEvent -= OnBlock;
            stateMachine.InputManager.RollEvent -= OnRoll;

            stateMachine.InputManager.TargetEvent -= OnTarget;
            stateMachine.InputManager.NextTargetEvent -= OnNextTarget;
            stateMachine.InputManager.PrevTargetEvent -= OnPrevTarget;

            stateMachine.Health.OnTakeDamage -= HandleTakeDamage;
        }

        void ConvertDirection(Vector3 direction)
        {
            if (direction.magnitude > 1)
            {
                direction.Normalize();
            }

            Vector3 localMove = stateMachine.transform.InverseTransformDirection(direction);

            rightAmount = localMove.x;
            forwardAmount = localMove.z;
        }

        private void OnTarget()
        {
            stateMachine.Targeter.Cancel();
            stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
        }

        private void UpdateAnimator()
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, forwardAmount);
            stateMachine.Animator.SetFloat(TargetingRightHash, rightAmount);
        }

        void PlayFootSteps()
        {
            if (Time.fixedTime > nextStep)
            {
                nextStep = Time.fixedTime + stepRate;
                AudioManager.Instance.PlayRandomClip(stateMachine.AudioClips.Footsteps);
            }
        }

        void OnRoll()
        {
            stateMachine.SwitchState(new PlayerRollState(stateMachine, movement));
            return;
        }

        void OnNextTarget()
        {
            stateMachine.Targeter.NextTarget();
        }
        
        void OnPrevTarget()
        {
            stateMachine.Targeter.PrevTarget();
        }
    }
}
