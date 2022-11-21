using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    // This code need a refactor

    [SerializeField] float radius;
    [SerializeField] List<Collider> Colliders;
    [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
    [field: SerializeField] public GameObject NearestObject { get; private set; }

    private void Update()
    {
        if(NearestObject != null)
        {
            NearestObject.GetComponent<IInteractable>().InteractionDetected(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerToInteract == (LayerToInteract | (1 << other.gameObject.layer)))
        {
            if(other.GetComponent<IInteractable>().IsInteractable())
            {
                Colliders.Add(other);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (Collider collider in Colliders)
        {
            float sqrDistanceToCenter = (transform.position - collider.transform.position).sqrMagnitude;
            if (sqrDistanceToCenter <= radius * radius)
            {
                var interactableObj = collider.GetComponent<IInteractable>();
              
                if (interactableObj == null) continue;

                if (interactableObj.IsInteractable())
                {
                    NearestObject = collider.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other?.GetComponent<IInteractable>().InteractionDetected(false);
        Colliders.Remove(other);
        NearestObject = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
