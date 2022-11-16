using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("Controllers")]
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: Header("Movement Settings")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float RollForce { get; private set; }
    [field: SerializeField] public float RollDuration { get; private set; }
    [field: SerializeField] public LayerMask LayerToInteract { get; private set; }

    [field: SerializeField, Header("Attack Settings")] public Attack[] Attacks { get; private set; }

    public Transform MainCameraTransform { get; private set; }


    private void Start()
    {
        InputManager.InteractEvent += OnInteract;

        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerLocomotionState(this));
    }

    private void OnDestroy()
    {
        InputManager.InteractEvent -= OnInteract;
    }

    void OnInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerToInteract))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit " + hit.collider.name);
            if(hit.collider.GetComponent<Door>())
            {
                hit.collider.GetComponent<Door>().Interact();
            }
        }
    }
}
