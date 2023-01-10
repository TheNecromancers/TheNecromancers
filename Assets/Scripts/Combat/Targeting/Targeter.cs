using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheNecromancers.Combat
{
    public class Targeter : MonoBehaviour
    {
        private Camera mainCamera;
        private List<Target> targets = new List<Target>();
        [field: SerializeField] public Target CurrentTarget { get; private set; }
        [SerializeField] Transform TargetVFX;
        Transform TargetObj;
        Transform CurrentTargetTransform;

        private void Start()
        {
            mainCamera = Camera.main;
            TargetObj = Instantiate(TargetVFX, transform.position, Quaternion.identity);
            TargetObj.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (CurrentTarget != null)
            {
                TargetObj.gameObject.SetActive(true);

                if (CurrentTargetTransform != null)
                    TargetObj.position = CurrentTargetTransform.position;
            }
            else
            {
                TargetObj.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Target>(out Target target)) { return; }

            targets.Add(target);
            target.OnDestroyed += RemoveTarget;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Target>(out Target target)) { return; }

            RemoveTarget(target);
        }

        public bool SelectTarget()
        {
            if (targets.Count == 0) { return false; }

            Target closestTarget = null;
            float closestTargetDistance = Mathf.Infinity;

            foreach (Target target in targets)
            {
                Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

                if (!target.GetComponentInChildren<Renderer>().isVisible)
                {
                    continue;
                }

                Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
                if (toCenter.sqrMagnitude < closestTargetDistance)
                {
                    closestTarget = target;
                    closestTargetDistance = toCenter.sqrMagnitude;
                }
            }

            if (closestTarget == null) { return false; }

            CurrentTarget = closestTarget;
            UpdateTargetTransform(CurrentTarget);

            return true;
        }

        public void Cancel()
        {
            if (CurrentTarget == null) { return; }
            CurrentTarget = null;
        }

        public void RemoveTarget(Target target)
        {
            if (CurrentTarget == target)
            {
                CurrentTarget = null;
                UpdateTargetTransform(CurrentTarget);
            }

            target.OnDestroyed -= RemoveTarget;
            targets.Remove(target);

            if (targets.Count > 0)
            {
                CurrentTarget = targets[0];
            }
        }

        public void NextTarget()
        {
            if (targets.IndexOf(CurrentTarget) + 1 == targets.Count) 
            {
                CurrentTarget = targets[0];
                UpdateTargetTransform(CurrentTarget);
                return; 
            }

            CurrentTarget = targets[targets.IndexOf(CurrentTarget) + 1];
            UpdateTargetTransform(CurrentTarget);

        }

        public void PrevTarget()
        {
            if (targets.IndexOf(CurrentTarget) == 0) 
            {
                CurrentTarget = targets[targets.Count - 1];
                UpdateTargetTransform(CurrentTarget);
                return; 
            };

            CurrentTarget = targets[targets.IndexOf(CurrentTarget) - 1];
            UpdateTargetTransform(CurrentTarget);
        }

        private void UpdateTargetTransform(Target CurrentTarget)
        {
            CurrentTargetTransform = CurrentTarget?.GetComponent<Transform>();
        }
    }
}
