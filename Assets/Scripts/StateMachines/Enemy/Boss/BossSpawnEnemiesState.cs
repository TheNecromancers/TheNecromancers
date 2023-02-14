using TheNecromancers.StateMachine;
using TheNecromancers.StateMachine.Enemy;
using UnityEngine;
//using static UnityEditor.Progress;

public class BossSpawnEnemiesState : BossBaseState
{
    public BossSpawnEnemiesState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        switch (stateMachine.CurrentWave)
        {
            case 1:
                for (int i = 0; i < stateMachine.SpawnPoints.Count - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        Transform enemyMelee = GameObject.Instantiate(stateMachine.MeleeEnemy, stateMachine.SpawnPoints[i].position, Quaternion.Euler(0, -180, 0));
                        enemyMelee.SetParent(stateMachine.transform);
                        stateMachine.CurrentEnemies.Add(enemyMelee);
                    }
                    else
                    {
                        Transform rangedEnemy = GameObject.Instantiate(stateMachine.RangedEnemy, stateMachine.SpawnPoints[i].position, Quaternion.Euler(0, -180, 0));
                        rangedEnemy.SetParent(stateMachine.transform);
                        stateMachine.CurrentEnemies.Add(rangedEnemy);
                    }
                }
                break;

            case 2:
                for (int i = 0; i < stateMachine.SpawnPoints.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        Transform enemyMelee = GameObject.Instantiate(stateMachine.MeleeEnemy, stateMachine.SpawnPoints[i].position, Quaternion.Euler(0, -180, 0));
                        enemyMelee.SetParent(stateMachine.transform);
                        stateMachine.CurrentEnemies.Add(enemyMelee);
                    }
                    else
                    {
                        Transform rangedEnemy = GameObject.Instantiate(stateMachine.RangedEnemy, stateMachine.SpawnPoints[i].position, Quaternion.Euler(0, -180, 0));
                        rangedEnemy.SetParent(stateMachine.transform);
                        stateMachine.CurrentEnemies.Add(rangedEnemy);
                    }
                }

                break;
            default:
                break;
        }
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.transform.GetComponentsInChildren<EnemyStateMachine>().Length <= 0 &&
            !stateMachine.WaitForNextWave)
        {
            stateMachine.EndFirstWaveDialogue.StartDialogue();

            stateMachine.Collider.enabled = true;
            stateMachine.WaitForNextWave = true;
        }
    }

    public override void Exit()
    {
    }
}
