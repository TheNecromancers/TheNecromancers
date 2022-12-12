using System;
using UnityEngine;

namespace TheNecromancers.Gameplay.Player
{
    public class InteractionDetector : MonoBehaviour
    {
        [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public Collider[] Colliders { get; private set; }

        [SerializeField] GameObject InteractionText;

        public event Action<bool> OnCurrentInteraction;

        public IInteractable CurrentTarget;

        private void Update()
        {
            DetectInteractable();

            if(CurrentTarget != null)
            {
                InteractionText.SetActive(CurrentTarget.IsInteractable);
            }
            else
            {
                InteractionText.SetActive(false);
            }
        }

        private void DetectInteractable()
        {
            Colliders = Physics.OverlapSphere(transform.position, InteractionRange, LayerToInteract);

            if(Colliders.Length <= 0) { OnCurrentTargetExit(ref CurrentTarget); return; }

            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i] != null)
                {
                    if (Colliders[i].TryGetComponent(out IInteractable interactable))
                    {
                        if (CheckDistanceSqr(transform.position, Colliders[i].transform.position, InteractionRange))
                        {
                            if(!interactable.IsInteractable) { return; }

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

