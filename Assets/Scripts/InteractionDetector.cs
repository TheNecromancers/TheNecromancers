using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] float radius;
    [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
    public GameObject NearestObject { get; private set; }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerToInteract);

        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;

            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = colliders[i];
                var iteractableObj = nearestCollider.GetComponent<IInteractable>();
                if (nearestCollider != null)
                {
                    if (iteractableObj != null)
                    {
                        NearestObject = nearestCollider.gameObject;
                        NearestObject.transform.SendMessage("InteractionDetected", true);
                    }
                }
            }
        }

        if(nearestCollider == null || NearestObject == null)
        {
            NearestObject?.transform.SendMessage("InteractionDetected", false);
            NearestObject = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
