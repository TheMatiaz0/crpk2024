#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public abstract class ResettableScriptableObject : ScriptableObject
{
#if UNITY_EDITOR
    private string _initialJson = string.Empty;
#endif

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        _initialJson = EditorJsonUtility.ToJson(this);
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR

        EditorJsonUtility.FromJsonOverwrite(_initialJson, this);

#endif
    }

#if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        switch (obj)
        {
            case PlayModeStateChange.ExitingPlayMode:
                EditorJsonUtility.FromJsonOverwrite(_initialJson, this);
                break;
        }
    }
#endif
}
