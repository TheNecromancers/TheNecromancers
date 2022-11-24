using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
    [SerializeField] float radius;

    public IInteractable currentTarget;

    float minDistanceSqr;

    private void Update()
    {
        DetectInteractable();
    }

    private void DetectInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerToInteract);

        minDistanceSqr = radius * radius;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null)
            {
                IInteractable interactable = colliders[i].GetComponent<IInteractable>();

                float DistanceToCenterSqr = (transform.position - colliders[i].transform.position).sqrMagnitude;

                if (interactable != null)
                {
                    if (DistanceToCenterSqr <= minDistanceSqr)
                    {
                        Debug.Log("Nearest object " + colliders[i].name);

                        if (interactable == currentTarget) return;
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
                    // Maybe useless check
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
