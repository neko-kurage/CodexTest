using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GUISystem
{
    /// <summary>
    /// 子階層をキャッシュし、階層が変化した時のみ再構築を行うコンポーネントです。
    /// </summary>
    [ExecuteAlways]
    public class UiHierarchyCache : MonoBehaviour
    {
        private Dictionary<int, Transform> _lookup = new();
        private bool _isDirty;

        void Awake()
        {
            Rebuild();
        }

        void OnTransformChildrenChanged()
        {
            MarkDirty();
        }

        void OnTransformParentChanged()
        {
            MarkDirty();
        }

#if UNITY_EDITOR
        void OnEnable()
        {
            EditorApplication.hierarchyChanged += MarkDirty;
        }

        void OnDisable()
        {
            EditorApplication.hierarchyChanged -= MarkDirty;
        }
#endif

        /// <summary>
        /// 階層の変更を通知し、次のフレームでキャッシュを再構築します。
        /// </summary>
        private void MarkDirty()
        {
            _isDirty = true;
        }

        void LateUpdate()
        {
            if (!_isDirty)
            {
                return;
            }

            Rebuild();
        }

        /// <summary>
        /// 子階層を走査してキャッシュを構築します。
        /// </summary>
        private void Rebuild()
        {
            _lookup.Clear();
            foreach (Transform t in GetComponentsInChildren<Transform>(true))
            {
                _lookup[t.GetInstanceID()] = t;
            }

            _isDirty = false;
        }

        /// <summary>
        /// instance IDからTransformを取得します。
        /// </summary>
        /// <param name="id">検索するinstance ID</param>
        /// <returns>該当するTransform。存在しない場合はnull</returns>
        public Transform Find(int id)
        {
            return _lookup.TryGetValue(id, out Transform t) ? t : null;
        }
    }
}
