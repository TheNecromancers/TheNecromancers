using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
public class RunGame : Editor
{
    const string CHEATS_ENABLED = "CheatsEnabled";

    [MenuItem("Game/Run Game")]
    static void StartGame()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Levels/Introduction.unity");
            EditorApplication.isPlaying = true;
        }
    }

    /*
    /// <summary>
    /// Validating Editor Menu Items
    /// </summary>
    [MenuItem("Game/Cheats Enabled")]
    static void ToggleCheats()
    {
        EditorPrefs.SetBool(CHEATS_ENABLED, !EditorPrefs.GetBool(CHEATS_ENABLED));
    }

    [MenuItem("Game/Cheats Enabled", true)]
    static bool ValidateToggleCheats()
    {
        // Return false if no transform is selected.
        return EditorPrefs.GetBool(CHEATS_ENABLED);
    }
    */
}
#endif