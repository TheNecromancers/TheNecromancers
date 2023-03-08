using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityVisualController : MonoBehaviour
{
    [SerializeField] GameObject ExplorationAbilityParent;
    [SerializeField] GameObject RepulsionAbilityParent;


    public void UseExplorationAbility(float _abilityCooldown)
    {
        if (ExplorationAbilityParent.activeInHierarchy)
        {
            Image[] ExplorationImages = ExplorationAbilityParent.GetComponentsInChildren<Image>();
            if(ExplorationImages.Length > 1)
            {
                ExplorationImages[1].fillAmount = 0;
                StartCoroutine(RechargeGraphically(ExplorationImages[1], _abilityCooldown));
            }
        }
        
        
    }

    public void UseRepulsionAbility(float _abilityCooldown)
    {
        if (ExplorationAbilityParent.activeInHierarchy)
        {
            Image[] RepulsionImages = RepulsionAbilityParent.GetComponentsInChildren<Image>();
            if (RepulsionImages.Length > 1)
            {
                RepulsionImages[1].fillAmount = 0;
                StartCoroutine(RechargeGraphically(RepulsionImages[1], _abilityCooldown));
            }
        }
    }

    IEnumerator RechargeGraphically(Image RechargeImage, float _abilityCooldown)
    {
        while(RechargeImage.fillAmount < 1)
        {
            RechargeImage.fillAmount += 1 / _abilityCooldown * Time.deltaTime;
            if (RechargeImage.fillAmount >= 1) RechargeImage.fillAmount = 1;
            yield return null;
        }
    }

    public void ToggleOffImages()
    {
        ExplorationAbilityParent.SetActive(false);
        RepulsionAbilityParent.SetActive(false);
    }

    public void ToggleOnImages()
    {
        ExplorationAbilityParent.SetActive(true);
        RepulsionAbilityParent.SetActive(true);
    }
}
