using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ForcedGamepadNavigation : MonoBehaviour
{

    public GameObject ButtonToForceSelection;

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            PreventDeselectionForced(ButtonToForceSelection);
        }
    }

    public void PreventDeselectionForced(GameObject _Go)
    {
        if(EventSystem.current.currentSelectedGameObject != _Go)
        {
            EventSystem.current.SetSelectedGameObject(_Go);
        }
    }
}
