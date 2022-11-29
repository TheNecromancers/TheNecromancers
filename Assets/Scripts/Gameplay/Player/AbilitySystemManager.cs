using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemManager : MonoBehaviour
{
    [field: Header("Exploration Ability")]

    [SerializeField] float MinHeight;
    [SerializeField] float MaxHeight;

    [SerializeField] float MaxRange;
    [SerializeField] float MinRange;

    [SerializeField] float MaxIntensity;
    [SerializeField] float MinIntensity;

    [SerializeField] float ModifierSpeed;
    [SerializeField] float TimerAfterReachingMax;
    [SerializeField] float ExplorationCooldown;

    private bool explorationRunning;
    private float intensityWeight;
    private float heightWeight;
    private float rangeWeight;
    Light Light;

    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    private void Start()
    {
        Light.range = MinRange;
        Light.intensity = MinIntensity;
        Light.transform.localPosition = new Vector3(Light.transform.localPosition.x, MinHeight, Light.transform.localPosition.z);
        explorationRunning = false;
    }
    public void OnCombactAbility()
    {

    }

    private void CalculateWeights()
    {
        float diffHeight = MaxHeight - MinHeight;
        float diffRange = MaxRange - MinRange;
        float diffIntensity = MaxIntensity - MinIntensity;
        float minValue = Mathf.Min(diffHeight, diffRange, diffIntensity);
        if(minValue == diffHeight)
        {
            heightWeight = 1;
            intensityWeight = diffIntensity / diffHeight;
            rangeWeight = diffRange / diffHeight;
        }
        else if (minValue == diffRange)
        {
            rangeWeight = 1;
            intensityWeight = diffIntensity / diffRange;
            heightWeight = diffHeight / diffRange;
        }
        else
        {
            intensityWeight = 1;
            rangeWeight = diffRange / diffIntensity;
            heightWeight = diffHeight / diffIntensity;
        }

    }

    public void OnExplorationAbility()
    {
        if (!explorationRunning)
        {
            CalculateWeights();
            explorationRunning = true;
            StartCoroutine(ExpandLight());
        }  
    }

    private IEnumerator CooldownExplorationAbility()
    {
        Debug.Log("Started Cooldown of " + ExplorationCooldown + " Seconds");
        yield return new WaitForSeconds(ExplorationCooldown);
        explorationRunning = false;
        Debug.Log("Ended Cooldown. Exploration Ability Available.");
    }

    public IEnumerator ExpandLight()
    {
        Vector3 targetLightPosition = new Vector3(Light.transform.localPosition.x, MaxHeight, Light.transform.localPosition.z);
        while(Light.range <= MaxRange || Light.intensity <= MaxIntensity){
            Light.range += ModifierSpeed * rangeWeight * Time.deltaTime;
            Light.intensity += ModifierSpeed * intensityWeight * Time.deltaTime;
            if (Light.transform.localPosition.y < targetLightPosition.y)
                Light.transform.localPosition = Vector3.MoveTowards(Light.transform.localPosition, targetLightPosition, ModifierSpeed * heightWeight * Time.deltaTime);
            else
                Light.transform.localPosition = targetLightPosition;
            yield return null;
        }
        yield return new WaitForSeconds(TimerAfterReachingMax);
        targetLightPosition = new Vector3(Light.transform.localPosition.x, MinHeight, Light.transform.localPosition.z);
        while (Light.range >= MinRange && Light.intensity >= MinIntensity){
            Light.range -= ModifierSpeed * rangeWeight * Time.deltaTime;
            Light.intensity -= ModifierSpeed * intensityWeight * Time.deltaTime;
            if (Light.transform.localPosition.y > targetLightPosition.y)
                Light.transform.localPosition = Vector3.MoveTowards(Light.transform.localPosition, targetLightPosition, ModifierSpeed * heightWeight * Time.deltaTime);
            else
                Light.transform.localPosition = targetLightPosition;
            yield return null;
        }
        Light.range = Mathf.Clamp(Light.range, MinRange, MaxRange);
        Light.intensity = Mathf.Clamp(Light.intensity, MinIntensity, MaxIntensity);
        yield return StartCoroutine(CooldownExplorationAbility()); 

    }
}
