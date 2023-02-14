using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
public class RunGame : Editor
{

    [MenuItem("Game/Run Game")]
    static void StartGame()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Levels/MainMenu.unity");
            EditorApplication.isPlaying = true;
        }
    }

    [MenuItem("Game/Delete Saved Files")]
    static void DeleteSavedFiles()
    {
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            string[] filePaths = Directory.GetFiles(string.Concat(Application.persistentDataPath, "/Data"));
            foreach (string filePath in filePaths) File.Delete(filePath);
            Debug.Log("All saved files deleted correctly!");
        }
    }

}
#endif