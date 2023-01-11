using TheNecromancers.StateMachine.Enemy;
using UnityEngine;

public class EnemyRangedAttackState : EnemyBaseState
{
    private readonly int ShootHash = Animator.StringToHash("Shoot");
    private const float TransitionDuration = 0.1f;

    private float nextFire;

    public EnemyRangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.WeaponLogic.SetAttack(stateMachine.CurrentWeapon.Damage, stateMachine.CurrentWeapon.Knockbacks[0], true);
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        FaceToPlayer(deltaTime);

        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
        else if (nextFire <= 0)
        {
            ShootProjectile();
            nextFire = stateMachine.AttackRate;
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
        Transform projectile = GameObject.Instantiate(stateMachine.ProjectileObj);

        //Set projectile 
        projectile.transform.SetPositionAndRotation(stateMachine.RightHandHolder.transform.GetChild(0).transform.position, stateMachine.transform.rotation);
        projectile.SetParent(stateMachine.transform);
    }

}
