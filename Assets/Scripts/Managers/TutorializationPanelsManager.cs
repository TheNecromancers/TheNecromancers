using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class TutorializationPanelsManager : MonoBehaviour
{
    private static TutorializationPanelsManager _instance;
    Image[] tutorialPanels;

    public static TutorializationPanelsManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Tutorialization Panels Manager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        tutorialPanels = GetComponentsInChildren<Image>(true);
        tutorialPanels = (from item in tutorialPanels
                          where item.gameObject.tag == "TutorialPanel"
                          select item).ToArray<Image>();
        foreach(Image item in tutorialPanels)
        {
            item.gameObject.SetActive(false);
        }
    }
    
    public void StartPanelWASDMove()
    {
        StartPanelWithIndex(0);
    }
    public void StartPanelInteractions()
    {
        StartPanelWithIndex(1);
    }

    public void StartPanelEquipWeapon()
    {
        StartPanelWithIndex(2);
    }

    public void StartPanelCombact()
    {
        StartPanelWithIndex(3);
    }

    public void StartPanelHealth()
    {
        StartPanelWithIndex(4);
    }

    public void StartPanelExploration()
    {
        StartPanelWithIndex(5);
    }

    public void StartPanelExplosion()
    {
        StartPanelWithIndex(6);
    }
    private void StartPanelWithIndex(int index)
    {
        if(tutorialPanels != null)
        {
            if (tutorialPanels[index] != null)
            {
                TutorialInfoSaver tis = tutorialPanels[index].GetComponent<TutorialInfoSaver>();
                if(tis != null && !tis.isTriggered) {
                    tis.PanelOpened();
                    tutorialPanels[index].gameObject.SetActive(true);
                }
            }
        }
    }
}
