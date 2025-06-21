#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GUISystem.Editor
{
    /// <summary>
    /// RectTransformViewportSize用のインスペクタです。
    /// </summary>
    [CustomEditor(typeof(RectTransformViewportSize))]
    public class RectTransformViewportSizeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox("RectTransformのサイズはこのコンポーネントによって制御されます。", MessageType.Info);
        }
    }
}
#endif
