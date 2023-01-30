using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenInitialScene : MonoBehaviour
{
    public void OpenInitial()
    {
        SceneManager.LoadScene("Introduction");
    }
}
