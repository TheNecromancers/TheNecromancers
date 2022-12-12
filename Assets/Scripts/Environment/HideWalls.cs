using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HideWalls : MonoBehaviour
{
    [Tooltip("Walls or Environment")]
    [SerializeField] LayerMask LayersToInteract;

    public List<MeshRenderer> renderers;
    MeshRenderer CurrentHit = null;


    void Update()
    {
        if (CurrentHit != null)
            Debug.Log("Sto colpendo: " + CurrentHit.gameObject.name);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.magenta);

        if (Physics.Raycast(ray, out hit, 100, LayersToInteract))
        {
            if (hit.collider.TryGetComponent(out MeshRenderer renderer))
            {
                if (renderer == CurrentHit)
                {
                    CurrentHit.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    return;
                }
                else if (CurrentHit != null)
                {
                    CurrentHit.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    CurrentHit = renderer;
                    CurrentHit.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    return;
                }
                else
                {
                    CurrentHit = renderer;
                    CurrentHit.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
            }
            else
            {
                if (CurrentHit != null)
                {
                    CurrentHit.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    CurrentHit = null;
                }
            }
        }
        else
        {
            if (CurrentHit != null)
            {
                CurrentHit.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                CurrentHit = null;
            }
        }
    }
}
