using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine;
using System;
using TheNecromancers.Combat;
using UnityEngine.InputSystem.XR;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossStateMachine : StateMachine
{
    [field: Header("Minions Spawn")]
    [field: SerializeField] public List<Transform> SpawnPoints;
    [field: SerializeField] public Transform MeleeEnemy;
    [field: SerializeField] public Transform RangedEnemy;
    public List<Transform> CurrentEnemies;
    [field: SerializeField] public bool WaitForNextWave = false;
    public int CurrentWave = 1;

    [field: Header("Dialagoues")]
    [field: SerializeField] public DialogueTrigger EndFirstWaveDialogue;
    [field: SerializeField] public DialogueTrigger EndSecondWaveDialogue;

    [field: Header("Components")]
    [field: SerializeField] public Health Health;
    [field: SerializeField] public CharacterController Collider;
    [field: SerializeField] public Animator Animator;

    [field: Header("Player Settings")]
    [SerializeField] Transform PlayerInitialPosition;
    [SerializeField] Transform PlayerTeleportVFX;
    public GameObject Player { get; private set; }

    [field: Header("Slow Motion")]
    [SerializeField] float SlowMotionTimeScale = 0.1f;
    [SerializeField] float SlowMotionDuration = 0.3f;

    float startTimeScale;
    float startFixedDeltaTime;

    readonly int dance = Animator.StringToHash("Dance");
    readonly int idle = Animator.StringToHash("Idle");
    readonly int dead = Animator.StringToHash("Die");

    [SerializeField] GameObject TransitionOutIn;

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

        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;

    }

    public IEnumerator SpawnNextWave()
    {
        Animator.CrossFadeInFixedTime(dance, 0.1f);

        // Player controller need is disable for change its position
        Player.GetComponent<CharacterController>().enabled = false;

        Transform teleportFX = Instantiate(PlayerTeleportVFX, PlayerInitialPosition.position, Quaternion.identity);
        Player.transform.position = PlayerInitialPosition.position;
        WaitForNextWave = false;
        Collider.enabled = false;

        CurrentEnemies.Clear();
        SwitchState(new BossSpawnEnemiesState(this));
       

        yield return new WaitForSeconds(2.5f);
        Destroy(teleportFX.gameObject);

        Player.GetComponent<CharacterController>().enabled = true;
        Animator.CrossFadeInFixedTime(idle, 0.1f);

    }

    void HandleTakeDamage()
    {
        CurrentWave++;

        if (CurrentWave <= 2)
        {
            StartCoroutine(SpawnNextWave());
        }
        else
        {
            Debug.Log("End Game, Necromancer death");
            Animator.CrossFadeInFixedTime(dead, 0.1f);
            StartCoroutine(SlowMotion());
        }
    }

    IEnumerator SlowMotion()
    {
        Player.GetComponent<CharacterController>().enabled = false;

        Time.timeScale = SlowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * SlowMotionTimeScale;

        yield return new WaitForSeconds(SlowMotionDuration);
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
        
        TransitionOutIn.SetActive(true);
        yield return new WaitForSeconds(3.3f);
        if (PlayerInstance.Instance != null)
        {
            Destroy(PlayerInstance.Instance.gameObject);
        }
        // Load Final Scene
        SceneManager.LoadScene("EndScene");

    }
}
