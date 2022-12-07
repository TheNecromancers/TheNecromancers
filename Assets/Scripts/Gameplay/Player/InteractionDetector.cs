using UnityEngine;

namespace TheNecromancers.Gameplay.Player
{
    public class InteractionDetector : MonoBehaviour
    {
        [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public Collider[] colliders { get; private set; }

        public IInteractable CurrentTarget;

        private void Update()
        {
            DetectInteractable();
        }

        private void DetectInteractable()
        {
            colliders = Physics.OverlapSphere(transform.position, InteractionRange, LayerToInteract);

            if(colliders.Length <= 0) { OnCurrentTargetExit(ref CurrentTarget); return; }

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null)
                {
                    if (colliders[i].TryGetComponent(out IInteractable interactable))
                    {
                        if (CheckDistanceSqr(transform.position, colliders[i].transform.position, InteractionRange))
                        {
                            Debug.Log("Nearest object to interact " + colliders[i].name);
                            if (interactable == CurrentTarget) { return; }
                            else if (CurrentTarget != null)
                            {
                                CurrentTarget.OnEndHover();
                                CurrentTarget = interactable;
                                CurrentTarget.OnStartHover();
                                return;
                            }
                            else
                            {
                                CurrentTarget = interactable;
                                CurrentTarget.OnStartHover();
                            }
                        }
                        else
                        {
                            OnCurrentTargetExit(ref CurrentTarget);
                        }
                    }
                    else
                    {
                        OnCurrentTargetExit(ref CurrentTarget);
                    }
                }
            }
        }

        void OnCurrentTargetExit(ref IInteractable currentTarget)
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                Debug.Log("Current target OnTargetExit: " + currentTarget);
            }
        }

        private bool CheckDistanceSqr(Vector3 A, Vector3 B, float accuracy)
        {
            float distanceSqr = (A - B).sqrMagnitude;
            return distanceSqr <= accuracy * accuracy;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, InteractionRange);
        }
    }
}

