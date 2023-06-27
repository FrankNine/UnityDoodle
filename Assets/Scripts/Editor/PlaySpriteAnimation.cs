using UnityEngine;
using UnityEditor;

public class PlaySpriteAnimation : EditorWindow
{
    [MenuItem("Window/Doodle/Play Animation", false, 2)]
    private static void Init()
    {
        // Window Set-Up
        var window = GetWindow<PlaySpriteAnimation>(false, "Play Animation", true);
        window.position = new Rect(Vector2.zero, new Vector2(300, 300));
        window.Show();
    }
    
    private static readonly EditorCurveBinding _rootSpriteBinding = new EditorCurveBinding
    {
        type = typeof(SpriteRenderer),
        path = "",
        propertyName = "m_Sprite"
    };

    private AnimationClip _animationClip;
    private ObjectReferenceKeyframe[] _frames;
    private float _time;

    public void OnGUI()
    {
        if (_frames == null)
        {
            // Sprite from: https://opengameart.org/content/green-cap-character-16x18
            var guid = AssetDatabase.FindAssets("Green-Cap-Character-16x18-Walk")[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            _animationClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
            _frames = AnimationUtility.GetObjectReferenceCurve(_animationClip, _rootSpriteBinding);
        }

        EditorGUILayout.LabelField($"Framerate: {_animationClip.frameRate}");
        _time = EditorGUILayout.Slider(_time, 0, _animationClip.length);
        
        Sprite sprite = _GetFrameSprite(_frames, _time);
        DisplaySpriteInAtlas.DrawSprite(new Rect(100, 100, 100, 100), sprite);
    }
    
    private static Sprite _GetFrameSprite(ObjectReferenceKeyframe[] frames, float time)
    {
        if (frames == null || frames.Length == 0) 
        {
            return null;
        }

        if (frames.Length == 1)
        {
            return frames[0].value as Sprite; 
        }

        Sprite sprite = null;
        bool hasMatchFrame = false;
        foreach (var f in frames)
        {
            if (f.time < time)
            {
                continue;
            }
            
            sprite = f.value as Sprite;
            hasMatchFrame = true;
            break;
        }

        if (!hasMatchFrame)
        {
            sprite = frames[frames.Length -1].value as Sprite;
        }

        return sprite;
    }
}