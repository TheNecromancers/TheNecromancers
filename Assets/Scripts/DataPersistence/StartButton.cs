using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStart()
    {
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            string[] filePaths = Directory.GetFiles(string.Concat(Application.persistentDataPath, "/Data"));
            foreach (string filePath in filePaths) File.Delete(filePath);
        }
    }
}
