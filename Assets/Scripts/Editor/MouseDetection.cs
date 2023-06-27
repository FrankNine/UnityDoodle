using UnityEngine;
using UnityEditor;

public class MouseDetection : EditorWindow
{
    [MenuItem("Window/Doodle/Mouse Detection", false, 4)]
    private static void Init()
    {
        // Window Set-Up
        var window = GetWindow<MouseDetection>(false, "Mouse Detection", true);
        window.position = new Rect(Vector2.zero, new Vector2(300, 300));
        window.Show();
    }

    public void OnGUI()
    {
        EditorGUILayout.LabelField("There's delay in detection");
        var isInRange = new Rect(100, 100, 100, 100).Contains(Event.current.mousePosition);
        DrawOutlineReflection.DrawOutline(new Rect(100, 100, 100, 100), 1, isInRange ? Color.red : Color.green);
    }
}