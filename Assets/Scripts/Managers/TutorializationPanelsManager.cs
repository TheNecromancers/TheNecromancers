using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
        if(tutorialPanels != null)
        {
            if (tutorialPanels[0] != null)
            {
                TutorialInfoSaver tis = tutorialPanels[0].GetComponent<TutorialInfoSaver>();
                if(tis != null && !tis.isTriggered) {
                    tis.PanelOpened();
                    tutorialPanels[0].gameObject.SetActive(true);
                }
            }
        }
    }
}
