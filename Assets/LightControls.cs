using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControls : MonoBehaviour
{
    [Range(0, 179)]
    [SerializeField] float MaxOuterSpotAngle;
    [Range(0, 179)]
    [SerializeField] float MaxInnerSpotAngle;

    [Range(0, 179)]
    [SerializeField] float MinOuterSpotAngle;
    [Range(0, 179)]
    [SerializeField] float MinInnerSpotAngle;

    [SerializeField] float ModifierSpeed;
    [SerializeField] float ExpandendLightTimer;

    Light Light;

    float InnerSpotAngleDefault;
    float OuterSpotAngleDefault;

    bool active;


    void Awake()
    {
        Light = GetComponent<Light>();
    }

    private void Start()
    {
        InnerSpotAngleDefault = Light.innerSpotAngle;
        OuterSpotAngleDefault = Light.spotAngle;

        MinInnerSpotAngle = InnerSpotAngleDefault;
        MinOuterSpotAngle = OuterSpotAngleDefault;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
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

        Light.innerSpotAngle = Mathf.Clamp(Light.innerSpotAngle, MinInnerSpotAngle, MaxOuterSpotAngle);
        Light.spotAngle = Mathf.Clamp(Light.spotAngle, MinOuterSpotAngle, MaxOuterSpotAngle);
    }

    public IEnumerator ExpandLight()
    {
        if (Mathf.Approximately(Light.innerSpotAngle, MaxInnerSpotAngle) && Mathf.Approximately(Light.spotAngle, MaxOuterSpotAngle))
        {
            active = false;
        }

        Light.innerSpotAngle += ModifierSpeed * Time.deltaTime;
        Light.spotAngle += ModifierSpeed * Time.deltaTime;

        yield return new WaitForSeconds(ExpandendLightTimer);
            Light.innerSpotAngle -= ModifierSpeed * Time.deltaTime;
            Light.spotAngle -= ModifierSpeed * Time.deltaTime;
    }
}
