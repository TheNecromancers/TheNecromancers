using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    // This code need a refactoring
    [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
    [SerializeField] float radius;
    [SerializeField] float actionRange;


    [field: SerializeField] public IInteractable currentTarget;

    float minSqrDistance = Mathf.Infinity;

    private void Update()
    {
        DetectInteractable();
    }

    private void DetectInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerToInteract);
        
        minSqrDistance = radius * radius;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null)
            {
                IInteractable interactable = colliders[i].GetComponent<IInteractable>();

                float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;

                if (interactable != null)
                {
                    if (sqrDistanceToCenter <= minSqrDistance)
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
                        if (currentTarget != null)
                        {
                            currentTarget.OnEndHover();
                            currentTarget = null;
                            return;
                        }
                    } 
                }
                else
                {
                    if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                } 
            }
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
