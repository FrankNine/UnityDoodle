using UnityEngine;
using UnityEditor;

public class CreateAnimationAsset : EditorWindow
{
    [MenuItem("Window/Doodle/Create Animation Asset", false, 3)]
    private static void Init()
    {
        // Window Set-Up
        var window = GetWindow<CreateAnimationAsset>(false, "Create Animation Asset", true);
        window.position = new Rect(Vector2.zero, new Vector2(300, 300));
        window.Show();
    }

    private static readonly EditorCurveBinding _rootSpriteBinding = new EditorCurveBinding
    {
        type = typeof(SpriteRenderer),
        path = "",
        propertyName = "m_Sprite"
    };

    private int _index;
    private Object[] _objects;

    private float _saveFrameRate;

    public void OnGUI()
    {
        _index = EditorGUILayout.IntSlider(_index, 1, 12);

        if (_objects == null)
        {
            // Sprite from: https://opengameart.org/content/green-cap-character-16x18
            var guid = AssetDatabase.FindAssets("Green-Cap-Character-16x18 t:texture")[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            _objects = AssetDatabase.LoadAllAssetsAtPath(path);
        }

        DisplaySpriteInAtlas.DrawSprite(new Rect(100, 100, 100, 100), _objects[_index] as Sprite);


        EditorGUILayout.LabelField("Save Frame Rate");
        _saveFrameRate = EditorGUILayout.Slider(_saveFrameRate, 12, 60);
        if (GUILayout.Button("Save Animation"))
        {
            _SaveSpriteAnimation(_saveFrameRate, _objects[_index] as Sprite);
        }
    }

    private static void _SaveSpriteAnimation(float saveFrameRate, Sprite sprite)
    {
        var savePath = EditorUtility.SaveFilePanelInProject("Save Animation", "Walk", "anim", string.Empty);
        if (string.IsNullOrEmpty(savePath))
        {
            return;
        }

        var animationClip = new AnimationClip
        {
            frameRate = saveFrameRate
        };

        ObjectReferenceKeyframe[] frames =
        {
            new ObjectReferenceKeyframe
            {
                time = 0.1f,
                value = sprite
            }
        };

        AnimationUtility.SetObjectReferenceCurve(animationClip, _rootSpriteBinding, frames);

        AssetDatabase.CreateAsset(animationClip, savePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}