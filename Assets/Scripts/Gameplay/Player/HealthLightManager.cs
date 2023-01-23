using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[System.Serializable]
public class ColorHealthLevel
{
    public int Percentage;
    public Color Color;
    public float Range;
    public float Intensity;
    public float PlayerIntensity;
}

public class HealthLightManager : MonoBehaviour
{
    [SerializeField] ColorHealthLevel[] colorHealthLevels;
    [SerializeField] float TransitionSpeed;
    [SerializeField] Light DirectionalLightOnPlayer;
    [SerializeField] float PlayerIntensityWhenDeath;
    [SerializeField] Light Light;

    private void Awake()
    {
        //Light = GetComponent<Light>();
    }

    public void ChangeLightAccordingToHealth(int currHealth, int maxHealth)
    {
        int currPercentage = currHealth * 100 / maxHealth;
        Debug.Log("Curr Percentage: "+currPercentage);
        foreach (ColorHealthLevel chl in colorHealthLevels)
        {
            if (currPercentage > chl.Percentage)
            {
                StartCoroutine(ChangeRangeOverTime(chl.Range, false));
                StartCoroutine(ChangeIntensityOverTime(Light,chl.Intensity, false));
                StartCoroutine(ChangeColorOverTime(chl.Color));
                StartCoroutine(ChangeIntensityOverTime(DirectionalLightOnPlayer, chl.PlayerIntensity, false));
                break;
            }
        }
    }


    private IEnumerator ChangeColorOverTime(Color targetColor)
    {
        float timeLeft = 1.0f;
        while (timeLeft >= Time.deltaTime)
        {
            Light.color = Color.Lerp(Light.color, targetColor, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ChangeIntensityOverTime(Light Light,float targetIntensity, bool increment)
    {
        if (increment)
        {
            while (Light.intensity <= targetIntensity)
            {
                Light.intensity += TransitionSpeed * Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (Light.intensity >= targetIntensity)
            {
                Light.intensity -= TransitionSpeed * Time.deltaTime;
                yield return null;
            }
        }
        Light.intensity = targetIntensity;

    }

    private IEnumerator ChangeRangeOverTime(float targetRange, bool increment)
    {
        if (increment)
        {
            while (Light.range <= targetRange)
            {
                Light.range += TransitionSpeed * Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (Light.range >= targetRange)
            {
                Light.range -= TransitionSpeed * Time.deltaTime;
                yield return null;
            }
        }
        Light.range = targetRange;
    }
    public void ChangePlayerIlluminationToDeath()
    {
        StartCoroutine(ChangeIntensityOverTime(DirectionalLightOnPlayer, PlayerIntensityWhenDeath, false));
    }

    public void RestoreLifeColors()
    {
        StartCoroutine(ChangeRangeOverTime(colorHealthLevels[0].Range, true));
        StartCoroutine(ChangeIntensityOverTime(Light,colorHealthLevels[0].Intensity, true));
        StartCoroutine(ChangeColorOverTime(colorHealthLevels[0].Color));
        StartCoroutine(ChangeIntensityOverTime(DirectionalLightOnPlayer, colorHealthLevels[0].PlayerIntensity, true));
    }
}