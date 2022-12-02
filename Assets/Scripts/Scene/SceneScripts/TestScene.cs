using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestScene : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("sandBoxSceneBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Vado a SandBox");
            Loader.Load(Loader.Scene.SandBox);
        };
    }
}
