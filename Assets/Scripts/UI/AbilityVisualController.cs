using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityVisualController : MonoBehaviour
{
    [SerializeField] List<Image> ExplorationImages;
    [SerializeField] List<Image> RepulsionImages;
    [SerializeField] Color ExplorationBrightColor;
    [SerializeField] Color ExplorationDarkColor;
    [SerializeField] Color RepulsionBrightColor;
    [SerializeField] Color RepulsionDarkColor;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Image im in ExplorationImages)
        {
            im.color = ExplorationBrightColor;
        }
        foreach (Image im in RepulsionImages)
        {
            im.color = RepulsionBrightColor;
        }
    }

    public void UseExplorationAbility(float _abilityCooldown)
    {
        foreach (Image im in ExplorationImages)
        {
            im.color = new Color(ExplorationDarkColor.r, ExplorationDarkColor.g, ExplorationDarkColor.b, 0);
        }
        StartCoroutine(RechargeGraphically(ExplorationImages, ExplorationDarkColor, ExplorationBrightColor, _abilityCooldown));
    }

    public void UseRepulsionAbility(float _abilityCooldown)
    {
        foreach (Image im in RepulsionImages)
        {
            im.color = new Color(RepulsionDarkColor.r, RepulsionDarkColor.g, RepulsionDarkColor.b, 0);
        }
        StartCoroutine(RechargeGraphically(RepulsionImages, RepulsionDarkColor, RepulsionBrightColor, _abilityCooldown));
    }

    IEnumerator RechargeGraphically(List<Image> RechargeImages, Color RechargeColor, Color BrightColor, float _abilityCooldown)
    {
        float elapsedTime = 0;
        while(elapsedTime < _abilityCooldown/2)
        {
            RechargeImages[0].color = new Color(RechargeColor.r, RechargeColor.g, RechargeColor.b, RechargeColor.a / 2 + (elapsedTime / (_abilityCooldown / 2) * RechargeColor.a / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RechargeImages[0].color = BrightColor;
        while (elapsedTime >= _abilityCooldown / 2 && elapsedTime < _abilityCooldown)
        {
            RechargeImages[1].color = new Color(RechargeColor.r, RechargeColor.g, RechargeColor.b, RechargeColor.a / 2 + (elapsedTime / (_abilityCooldown ) * RechargeColor.a / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RechargeImages[1].color = BrightColor;
    }
}
