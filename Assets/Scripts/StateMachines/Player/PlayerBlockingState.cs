using UnityEngine;

namespace TheNecromancers.StateMachine.Player
{
    public class PlayerBlockingState : PlayerBaseState
    {
        private readonly int BlockHash = Animator.StringToHash("Block");

        private const float CrossFadeDuration = 0.1f;
        private float remainingBlockTime = 0.65f;
        public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            remainingBlockTime -= deltaTime;

            if (remainingBlockTime <= 0f)
            {
                ReturnToLocomotion();
                return;
            }
        }

        public override void Exit() { }
    }
}
