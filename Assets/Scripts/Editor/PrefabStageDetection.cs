using UnityEditor;
using UnityEditor.SceneManagement;

public class PrefabStageDetection 
{
    [MenuItem("Window/Doodle/Prefab Stage Detection", false, 5)]
    private static void Init()
    {
        EditorUtility.DisplayDialog
        (
            "Prefab Stage",
            PrefabStageUtility.GetCurrentPrefabStage() == null ? "Not prefab editing" : "Prefab editing",
            "Ok"
        );
    }
}
