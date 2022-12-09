using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SandBox : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("testSceneBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Vado alla Scena di Prova");
            Loader.Load(Loader.Scene.TestScene);
        };
    }
}
