#if UNITY_EDITOR
using UnityEditor;

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
            EditorGUILayout.HelpBox("RectTransformのサイズはこのコンポーネントによってDrivenRectTransformTrackerを使用して制御されます。RectTransformのSize Deltaプロパティは自動的に無効化されます。", MessageType.Info);
        }
    }
}
#endif
