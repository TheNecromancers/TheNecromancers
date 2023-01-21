using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine;
using System;

public class BossStateMachine : StateMachine
{
    [field: SerializeField] public List<Transform> SpawnPoints;
    [field: SerializeField] public Transform MeleeEnemy;
    [field: SerializeField] public Transform RangedEnemy;

    public List<Transform> CurrentEnemies;
    public int CurrentWave = 1;

    private void Start()
    {
        SwitchState(new BossSpawnEnemiesState(this));
    }
}
