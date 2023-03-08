using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightControls : MonoBehaviour
{
    [SerializeField] float MaxRange;
    [SerializeField] float MinRange;

    [SerializeField] float MaxIntensity;
    [SerializeField] float MinIntensity;

    [SerializeField] float ModifierSpeed;
    [SerializeField] float Timer;

    bool active;

    Light Light;

    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            active = true;
        }

        if (active)
        {
            StartCoroutine(ExpandLight());
        }
        else
        {
            StopCoroutine(ExpandLight());
        }

        Light.range = Mathf.Clamp(Light.range, MinRange, MaxRange);
        Light.intensity = Mathf.Clamp(Light.intensity, MinIntensity, MaxIntensity);

    }

    public IEnumerator ExpandLight()
    {
        if (Mathf.Approximately(Light.range, MaxRange) && Mathf.Approximately(Light.intensity, MaxIntensity))
        {
            active = false;
        }

        Light.range += ModifierSpeed * Time.deltaTime;
        Light.intensity += ModifierSpeed * Time.deltaTime;

        yield return new WaitForSeconds(Timer);
        Light.range -= ModifierSpeed * Time.deltaTime;
        Light.intensity -= ModifierSpeed * Time.deltaTime;
    }
}
