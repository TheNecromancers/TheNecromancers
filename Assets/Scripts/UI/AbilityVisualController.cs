using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityVisualController : MonoBehaviour
{
    [SerializeField] List<Image> ExplorationImages;
    [SerializeField] List<Image> RepulsionImages;
    [SerializeField] Color ExplorationColor;
    [SerializeField] Color RepulsionColor;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Image im in ExplorationImages)
        {
            im.color = ExplorationColor;
        }
        foreach (Image im in RepulsionImages)
        {
            im.color = RepulsionColor;
        }
    }

    public void UseExplorationAbility(float _abilityCooldown)
    {
        foreach (Image im in ExplorationImages)
        {
            im.color = new Color(ExplorationColor.r, ExplorationColor.g, ExplorationColor.b, 0);
        }
        StartCoroutine(RechargeGraphically(ExplorationImages, ExplorationColor, _abilityCooldown));
    }

    public void UseRepulsionAbility(float _abilityCooldown)
    {
        foreach (Image im in RepulsionImages)
        {
            im.color = new Color(RepulsionColor.r, RepulsionColor.g, RepulsionColor.b, 0);
        }
        StartCoroutine(RechargeGraphically(RepulsionImages, RepulsionColor, _abilityCooldown));
    }

    IEnumerator RechargeGraphically(List<Image> RechargeImages, Color RechargeColor, float _abilityCooldown)
    {
        float elapsedTime = 0;
        while(elapsedTime < _abilityCooldown/2)
        {
            RechargeImages[0].color = new Color(RechargeColor.r, RechargeColor.g, RechargeColor.b, RechargeColor.a / 2 + (elapsedTime / (_abilityCooldown / 2) * RechargeColor.a / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (elapsedTime >= _abilityCooldown / 2 && elapsedTime < _abilityCooldown)
        {
            RechargeImages[1].color = new Color(RechargeColor.r, RechargeColor.g, RechargeColor.b, RechargeColor.a / 2 + (elapsedTime / (_abilityCooldown ) * RechargeColor.a / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
