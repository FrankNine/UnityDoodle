using UnityEngine;
using UnityEditor;

public class DrawDelimiterLineInternalAPIEngineBridge : EditorWindow 
{
    [MenuItem("Window/Doodle/Draw Delimiter Line InternalAPIEngineBridge", false, 6)]
    private static void Init()
    {
        // Window Set-Up
        var window = GetWindow<DrawDelimiterLineInternalAPIEngineBridge>(false, "Draw Delimiter Line InternalAPIEngineBridge", true);
        window.position = new Rect(Vector2.zero, new Vector2(300, 300));
        window.Show();
    }
    
    // Unity has predefined InternalsVisibleTo in AssemblyInfo.cs
    // https://github.com/Unity-Technologies/UnityCsReference/blob/a935c76939be457b82c70f54fe2cc7dd40fb9090/Runtime/Export/AssemblyInfo.cs#L111
    // You can test internal API by naming asmdef accordingly
    
    // InternalsVisibleTo are removed at Unity 2023.2.0a13 
    // https://github.com/Unity-Technologies/UnityCsReference/blob/0355e09029fa1212b7f2e821f41565df8e8814c7/Runtime/Export/AssemblyInfo.cs
    // Might not be available in later version
#if !UNITY_2023_2_OR_NEWER
    private void OnGUI() 
        => EditorGUI.DrawDelimiterLine(new Rect(100, 100, 100, 100));
#endif
}
