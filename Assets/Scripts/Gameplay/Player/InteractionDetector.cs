using UnityEngine;

namespace TheNecromancers.Gameplay
{
    public class InteractionDetector : MonoBehaviour
    {
        [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
        [SerializeField] float InteractionRange;

        public IInteractable currentTarget;

        private void Update()
        {
            DetectInteractable();
        }

        private void DetectInteractable()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, InteractionRange, LayerToInteract);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null)
                {
                    if (colliders[i].TryGetComponent<IInteractable>(out IInteractable interactable))
                    {
                        if (CheckDistanceSqr(transform.position, colliders[i].transform.position, InteractionRange))
                        {
                            Debug.Log("Nearest object to interact " + colliders[i].name);

                            if (interactable == currentTarget) { return; }
                            else if (currentTarget != null)
                            {
                                currentTarget.OnEndHover();
                                currentTarget = interactable;
                                currentTarget.OnStartHover();
                                return;
                            }
                            else
                            {
                                currentTarget = interactable;
                                currentTarget.OnStartHover();
                            }
                        }
                        else
                        {
                            OnCurrentTargetExit(ref currentTarget);
                        }
                    }
                    else
                    {
                        // Maybe useless check, should can be removed
                        OnCurrentTargetExit(ref currentTarget);
                    }
                }
                else
                {
                    OnCurrentTargetExit(ref currentTarget);
                }
            }
        }

        void OnCurrentTargetExit(ref IInteractable currentTarget)
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }

        private bool CheckDistanceSqr(Vector3 A, Vector3 B, float accuracy)
        {
            float distanceSqr = (A - B).sqrMagnitude;
            return distanceSqr <= accuracy * accuracy;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, InteractionRange);
        }
    }
}

