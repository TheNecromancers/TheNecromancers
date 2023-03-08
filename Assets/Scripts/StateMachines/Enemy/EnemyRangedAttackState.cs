using TheNecromancers.StateMachine.Enemy;
using UnityEngine;

public class EnemyRangedAttackState : EnemyBaseState
{
    private readonly int AimHash = Animator.StringToHash("CrossbowAim");
    private readonly int ShootHash = Animator.StringToHash("Shoot");
    private const float TransitionDuration = 0.1f;

    private float nextFire;

    public EnemyRangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.WeaponLogic.SetAttack(stateMachine.CurrentWeapon.Damage, stateMachine.CurrentWeapon.Knockbacks[0], true);
    }

    public override void Enter()
    {
        nextFire = stateMachine.AttackRate;
        stateMachine.Animator.CrossFadeInFixedTime(AimHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (IsPlayingAnimation(stateMachine.Animator, "Attack")) return;

        FaceToPlayer(deltaTime);

        if (!stateMachine.CooldownManager.CooldownActive("ProjectileShoot"))
        {
            ShootProjectile();
        }

        if (!IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }

    private void ShootProjectile()
    {
        stateMachine.Animator.PlayInFixedTime(ShootHash, 0, TransitionDuration);
    }
}
