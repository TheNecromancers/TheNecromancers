using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheNecromancers.StateMachine.Player
{
    public class PlayerDeadState : PlayerBaseState
    {
        private readonly int DieHash = Animator.StringToHash("Die");
        private const float CrossFadeduration = 0.1f;

        public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(DieHash, CrossFadeduration);
            Debug.Log("Player è morto");
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void Exit()
        {
        }

    }
}