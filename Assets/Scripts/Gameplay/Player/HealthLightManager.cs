using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class ColorHealthLevel
{
    public int Percentage;
    public Color Color;
    public float Range;
    public float Intensity;
}

public class HealthLightManager : MonoBehaviour
{
    [SerializeField] ColorHealthLevel[] colorHealthLevels;
    Light Light;
    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    public void ChangeLightAccordingToHealth(int currHealth, int maxHealth)
    {
        int currPercentage = currHealth * 100 / maxHealth;
        Debug.Log("Curr Percentage: "+currPercentage);
        foreach (ColorHealthLevel chl in colorHealthLevels)
        {
            if (currPercentage > chl.Percentage)
            {
                Debug.Log("Entering coroutine");
                StartCoroutine(ChangeLightValuesOverTime(chl));
                break;
            }
        }
    }

    private IEnumerator ChangeLightValuesOverTime(ColorHealthLevel chl)
    {
        /*Debug.Log("Changing Stuffs!");
        Mathf.Lerp(Light.range, chl.Range, Time.deltaTime);
        Mathf.Lerp(Light.intensity, chl.Intensity, Time.deltaTime);
        Color.Lerp(Light.color, chl.Color, Mathf.PingPong(Time.deltaTime, 1));*/
        Light.range = chl.Range;
        Light.intensity = chl.Intensity;
        Light.color = chl.Color;
        yield return null;
    }

    public void RestoreLifeColors()
    {
        Light.range = colorHealthLevels[0].Range;
        Light.intensity = colorHealthLevels[0].Intensity;
        Light.color = colorHealthLevels[0].Color;
    }
}