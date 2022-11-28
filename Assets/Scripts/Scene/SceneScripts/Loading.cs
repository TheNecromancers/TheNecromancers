using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Loading : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Parte la scena che vuoi");
            Loader.Load(Loader.Scene.Loading);
        };
    }
}
