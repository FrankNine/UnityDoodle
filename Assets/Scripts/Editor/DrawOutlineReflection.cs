using System.Reflection;

using UnityEngine;
using UnityEditor;

public class DrawOutlineReflection : EditorWindow
{
    [MenuItem("Window/Doodle/DrawOutline Reflection")]
    private static void Init()
    {
        // Window Set-Up
        var window = GetWindow<DrawOutlineReflection>(false, "DrawOutline Reflection", true);
        window.position = new Rect(Vector2.zero, new Vector2(300, 300));
        window.Show();
    }

    public void OnGUI() 
        => _DrawOutline(new Rect(100, 100, 100, 100), 1, Color.green);

    private static void _DrawOutline(Rect rect, float size, Color color)
        => typeof(EditorGUI).GetMethod("DrawOutline", BindingFlags.NonPublic | BindingFlags.Static)
                            .Invoke(null, new object[]
                            {
                                rect, 
                                size, 
                                color
                            });
}