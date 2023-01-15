using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{



    private Controls controls;

    void Start()
    {
            controls = new Controls();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            controls.Player.Disable();
            controls.UIControls.Disable();
    }

    void Update()
    {
        
    }
}
