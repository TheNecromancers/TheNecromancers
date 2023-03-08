using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionController : MonoBehaviour
{
    [SerializeField] List<PickuppableItem> pickuppableItems;
    int countPickuppedItems = 0;
    void Start()
    {
        foreach(PickuppableItem item in pickuppableItems)
        {
            item.onPickupAction += ReactToItemPickup;
        }
    }

    private void ReactToItemPickup()
    {
        countPickuppedItems++;
        if(pickuppableItems.Count == countPickuppedItems)
        {
            StartCoroutine(StartTutorializationPanelAfterWaiting());
        }
    }

    private IEnumerator StartTutorializationPanelAfterWaiting()
    {
        yield return new WaitForSeconds(1f);
        TutorializationPanelsManager.Instance.StartPanelEquipWeapon();
    }
}
