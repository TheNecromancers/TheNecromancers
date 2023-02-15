using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine;
using System;
using TheNecromancers.Combat;
using UnityEngine.InputSystem.XR;
using System.Collections;

public class BossStateMachine : StateMachine
{
    [field: SerializeField] public List<Transform> SpawnPoints;
    [field: SerializeField] public Transform MeleeEnemy;
    [field: SerializeField] public Transform RangedEnemy;
    [field: SerializeField] public DialogueTrigger EndFirstWaveDialogue;
    [field: SerializeField] public DialogueTrigger EndSecondWaveDialogue;
    [field: SerializeField] public Health Health;
    [field: SerializeField] public CharacterController Collider;

    [field: SerializeField] public bool WaitForNextWave = false;

    public List<Transform> CurrentEnemies;
    public int CurrentWave = 1;

    [SerializeField] Transform PlayerInitialPosition;
    [SerializeField] Transform PlayerTeleportVFX;
    public GameObject Player { get; private set; }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        SwitchState(new BossSpawnEnemiesState(this));
        Collider.enabled = false;
        Health.enabled = true;
    }

    public IEnumerator SpawnNextWave()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        Transform teleportFX = Instantiate(PlayerTeleportVFX, PlayerInitialPosition.position, Quaternion.identity);
        Player.transform.position = PlayerInitialPosition.position;
        WaitForNextWave = false;
        CurrentEnemies.Clear();
        Collider.enabled = false;
        SwitchState(new BossSpawnEnemiesState(this));
        yield return new WaitForSeconds(2.5f);
        Destroy(teleportFX.gameObject);
        Player.GetComponent<CharacterController>().enabled = true;
    }

    void HandleTakeDamage()
    {
        CurrentWave++;

        if (CurrentWave <= 2)
            StartCoroutine(SpawnNextWave());
        else
            Debug.Log("End Game");
    }
}
