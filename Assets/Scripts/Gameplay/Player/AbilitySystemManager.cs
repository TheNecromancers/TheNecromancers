using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemManager : MonoBehaviour
{
    [SerializeField] float MaxRange;
    [SerializeField] float MinRange;

    [SerializeField] float MaxIntensity;
    [SerializeField] float MinIntensity;

    [SerializeField] float ModifierSpeed;
    [SerializeField] float Timer;

    private bool explorationRunning;

    Light Light;

    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    private void Start()
    {
        Light.range = Mathf.Clamp(Light.range, MinRange, MaxRange);
        Light.intensity = Mathf.Clamp(Light.intensity, MinIntensity, MaxIntensity);
        explorationRunning = false;
    }
    public void OnCombactAbility()
    {

    }

    public void OnExplorationAbility()
    {
        if (!explorationRunning)
        {
            explorationRunning = true;
            StartCoroutine(ExpandLight());
        }  
    }

    public IEnumerator ExpandLight()
    {
        while(Light.range <= MaxRange || Light.intensity <= MaxIntensity){
            Light.range += ModifierSpeed * Time.deltaTime;
            Light.intensity += ModifierSpeed * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(Timer);
        while (Light.range >= MinRange || Light.intensity >= MinIntensity){
            Light.range -= ModifierSpeed * Time.deltaTime;
            Light.intensity -= ModifierSpeed * Time.deltaTime;
            yield return null;
        }
        Light.range = Mathf.Clamp(Light.range, MinRange, MaxRange);
        Light.intensity = Mathf.Clamp(Light.intensity, MinIntensity, MaxIntensity);
        explorationRunning = false;

    }
}
