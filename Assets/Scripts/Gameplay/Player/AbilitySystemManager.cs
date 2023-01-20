using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemManager : MonoBehaviour
{
    [SerializeField] AbilityVisualController AbilityController;

    [field: Header("Exploration Ability")]

    [SerializeField] float MinHeight;
    [SerializeField] float MaxHeight;

    [SerializeField] float MaxRange;
    private float MinRange;

    [SerializeField] float MaxIntensity;
    private float MinIntensity;

    [SerializeField] float ModifierSpeed;
    [SerializeField] float TimerAfterReachingMax;
    [SerializeField] float ExplorationCooldown;

    [field: Header("Repulsion Ability")]
    [SerializeField] float MaxRepulsionIntensity;
    [SerializeField] float MinRepulsionIntensity;

    [SerializeField] float RepulsionSpeed;
    [SerializeField] float RepulsionCooldown;

    [field: SerializeField] public LayerMask LayerToInteract { get; private set; }
    [SerializeField] float Radius;
    [SerializeField] float KnockbackForce;

    private bool repulsionRunning;
    private bool explorationRunning;
    private bool repulsionOnCD;
    private bool explorationOnCD;
    private float intensityWeight;
    private float heightWeight;
    private float rangeWeight;
    [SerializeField] Light Light;

    private void Awake()
    {
        //Light = GetComponent<Light>();
    }

    private void Start()
    {
        //Light.range = MinRange;
        //Light.intensity = MinIntensity;
        Light.transform.localPosition = new Vector3(Light.transform.localPosition.x, MinHeight, Light.transform.localPosition.z);
        explorationRunning = false;
        repulsionRunning = false;
        repulsionOnCD = false;
        explorationOnCD = false;
    }
    public void OnCombactAbility(Vector3 playerPosition)
    {
        if (!repulsionRunning && !repulsionOnCD && !explorationRunning)
        {
            MinIntensity = Light.intensity;
            repulsionRunning = true;
            StartCoroutine(RepulseEnemies(playerPosition));
        }
    }

    private void CalculateWeights()
    {
        float diffHeight = MaxHeight - MinHeight;
        float diffRange = MaxRange - MinRange;
        float diffIntensity = MaxIntensity - MinIntensity;
        float minValue = Mathf.Min(diffHeight, diffRange, diffIntensity);
        if (minValue == diffHeight)
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

    private IEnumerator RepulseEnemies(Vector3 playerPosition)
    {
        while ( Light.intensity >= MinRepulsionIntensity)
        {
            Light.intensity -= RepulsionSpeed/3 * Time.deltaTime;
        }
        yield return new WaitForSeconds(0.5f);
        while (Light.intensity <= MaxRepulsionIntensity)
        {
            Light.intensity += RepulsionSpeed * Time.deltaTime;
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, LayerToInteract);
        foreach(Collider other in colliders)
        {
            if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
            {
                Vector3 direction = (other.transform.position - playerPosition).normalized;
                forceReceiver.AddForce(direction * KnockbackForce);
            }
        }
        yield return new WaitForSeconds(0.5f);
        Light.intensity = MinIntensity;
        repulsionRunning = false;
        repulsionOnCD = true;
        yield return StartCoroutine(CooldownRepulsionAbility());

    }

    public void OnExplorationAbility()
    {
        if (!explorationRunning && !explorationOnCD && !repulsionRunning)
        {
            MinRange = Light.range;
            MinIntensity = Light.intensity;
            CalculateWeights();
            explorationRunning = true;
            StartCoroutine(ExpandLight());
        }  
    }

    private IEnumerator CooldownRepulsionAbility()
    {
        AbilityController.UseRepulsionAbility(RepulsionCooldown);
        Debug.Log("Started Cooldown of " + RepulsionCooldown + " Seconds");
        yield return new WaitForSeconds(RepulsionCooldown);
        repulsionOnCD = false;
        Debug.Log("Ended Cooldown. Repulsion Ability Available.");
    }

    private IEnumerator CooldownExplorationAbility()
    {
        AbilityController.UseExplorationAbility(ExplorationCooldown);
        Debug.Log("Started Cooldown of " + ExplorationCooldown + " Seconds");
        yield return new WaitForSeconds(ExplorationCooldown);
        explorationOnCD = false;
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
        explorationRunning = false;
        explorationOnCD = true;
        yield return StartCoroutine(CooldownExplorationAbility()); 

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
