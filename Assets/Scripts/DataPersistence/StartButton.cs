using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnStart()
    {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath); 
        
        foreach (string filePath in filePaths) File.Delete(filePath);
    }
}
