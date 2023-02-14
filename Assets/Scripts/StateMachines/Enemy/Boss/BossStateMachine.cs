using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine;
using System;
using TheNecromancers.Combat;

public class BossStateMachine : StateMachine
{
    [field: SerializeField] public List<Transform> SpawnPoints;
    [field: SerializeField] public Transform MeleeEnemy;
    [field: SerializeField] public Transform RangedEnemy;
    [field: SerializeField] public DialogueTrigger EndFirstWaveDialogue;
    [field: SerializeField] public Health Health;
    [field: SerializeField] public CharacterController Collider;

    [field: SerializeField] public bool WaitForNextWave = false;

    public List<Transform> CurrentEnemies;
    public int CurrentWave = 1;

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
    }

    private void Start()
    {
        SwitchState(new BossSpawnEnemiesState(this));
        Collider.enabled = false;
        Health.enabled = true;
    }

    public void SpawnNextWave()
    {
        WaitForNextWave = false;
        CurrentWave++;
        CurrentEnemies.Clear();
        Collider.enabled = false;
        SwitchState(new BossSpawnEnemiesState(this));
    }

    void HandleTakeDamage()
    {
        Debug.Log("Ho preso danni " + gameObject.name);
        SpawnNextWave();
    }
}
