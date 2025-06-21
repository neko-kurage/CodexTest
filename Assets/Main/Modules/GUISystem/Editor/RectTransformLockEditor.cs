#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace GUISystem.Editor
{
    /// <summary>
    /// RectTransformのサイズ編集を制御するエディタ拡張です。
    /// </summary>
    [CustomEditor(typeof(RectTransform)), CanEditMultipleObjects]
    public class RectTransformLockEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor _defaultEditor;

        void OnEnable()
        {
            Type t = Type.GetType("UnityEditor.RectTransformEditor, UnityEditor");
            if (t != null)
            {
                _defaultEditor = CreateEditor(targets, t);
            }
        }

        void OnDisable()
        {
            if (_defaultEditor != null)
            {
                DestroyImmediate(_defaultEditor);
            }
        }

        public override void OnInspectorGUI()
        {
            bool disableSize = false;
            foreach (UnityEngine.Object obj in targets)
            {
                RectTransform rt = obj as RectTransform;
                if (rt != null && rt.GetComponent<RectTransformViewportSize>() != null)
                {
                    disableSize = true;
                    break;
                }
            }

            if (_defaultEditor != null)
            {
                SerializedObject so = new SerializedObject(targets);
                so.Update();
                SerializedProperty prop = so.GetIterator();
                bool enterChildren = true;
                while (prop.NextVisible(enterChildren))
                {
                    if (disableSize && prop.name == "m_SizeDelta")
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.PropertyField(prop, true);
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(prop, true);
                    }
                    enterChildren = false;
                }
                so.ApplyModifiedProperties();
            }
            else
            {
                DrawDefaultInspector();
            }
        }
    }
}
#endif
