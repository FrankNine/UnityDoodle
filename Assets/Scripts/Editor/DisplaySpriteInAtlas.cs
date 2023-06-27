using UnityEngine;
using UnityEditor;

public class DisplaySpriteInAtlas : EditorWindow
{
    [MenuItem("Window/Doodle/Display Sprite in Atlas", false, 1)]
    private static void Init()
    {
        // Window Set-Up
        var window = GetWindow<DisplaySpriteInAtlas>(false, "Display Sprite in Atlas", true);
        window.position = new Rect(Vector2.zero, new Vector2(300, 300));
        window.Show();
    }

    private int _index;
    private Object[] _objects;
    
    public void OnGUI()
    {
        // Index 0 is Texture2D
        _index = EditorGUILayout.IntSlider(_index, 1, 12);
        
        if (_objects == null)
        {
            // Sprite from: https://opengameart.org/content/green-cap-character-16x18
            var guid = AssetDatabase.FindAssets("Green-Cap-Character-16x18 t:texture")[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            _objects = AssetDatabase.LoadAllAssetsAtPath(path);
        }

        DrawOutlineReflection.DrawOutline(new Rect(100, 100, 100, 100), 1, Color.green);
        DrawSprite(new Rect(100, 100, 100, 100), _objects[_index] as Sprite);
    }

    // Code from: https://forum.unity.com/threads/drawing-a-sprite-in-editor-window.419199/#post-3059891
    public static void DrawSprite(Rect position, Sprite sprite)
    {
        if (sprite == null) { return; }
        
        var textureSize = new Vector2(sprite.texture.width, sprite.texture.height);

        var uv = new Rect
        (
            sprite.textureRect.x / textureSize.x,
            sprite.textureRect.y / textureSize.y,
            sprite.textureRect.width / textureSize.x,
            sprite.textureRect.height / textureSize.y
        );

        var minScale = Mathf.Min
        (
            position.width / sprite.textureRect.width,
            position.height / sprite.textureRect.height
        );

        var pos = new Rect
        {
            x = position.x,
            y = position.y,
            width = sprite.textureRect.width * minScale,
            height = sprite.textureRect.height * minScale
        };
        
        GUI.DrawTextureWithTexCoords(pos, sprite.texture, uv);
    }
}