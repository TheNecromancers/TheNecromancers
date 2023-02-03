using UnityEngine;

namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyDeadState : EnemyBaseState
    {
        private readonly int DieHash = Animator.StringToHash("Die");
        private const float CrossFadeduration = 0.1f;

        float disableEnemyDelay = 5f;
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Nemico Morto");
            disableEnemyDelay += Time.time;
            stateMachine.Animator.CrossFadeInFixedTime(DieHash, CrossFadeduration);
            stateMachine.WeaponLogic.gameObject.SetActive(false);
            stateMachine.Controller.enabled = false;
            GameObject.Destroy(stateMachine.Target);

            AudioManager.Instance.PlayRandomClip(stateMachine.AudioClips.Death);
        }

        public override void Tick(float deltaTime) 
        { 
            if(Time.time > disableEnemyDelay)
            {
                stateMachine.gameObject.SetActive(false);
                //GameObject.Destroy(stateMachine.gameObject, 5f);
            }
        }

        public override void Exit() { }
    }
}