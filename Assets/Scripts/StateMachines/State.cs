using UnityEngine;

namespace TheNecromancers.StateMachine
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Tick(float deltaTime);
        public abstract void Exit();

        protected float GetNormalizedTime(Animator animator, string tag)
        {
            AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

            if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
            {
                return nextInfo.normalizedTime;
            }
            else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
            {
                return currentInfo.normalizedTime;
            }
            else
            {
                return 0f;
            }
        }

        protected bool IsPlayingAnimation(Animator animator)
        {
            return animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
        }

        protected bool CheckDistanceSqr(Vector3 A, Vector3 B, float accuracy)
        {
            float distanceSqr = (A - B).sqrMagnitude;
            return distanceSqr <= accuracy * accuracy;
        }
    }
}