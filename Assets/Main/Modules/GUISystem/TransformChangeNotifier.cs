using System;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GUISystem
{
    /// <summary>
    /// Transformの変化を検知しイベントを発行するコンポーネントです。
    /// </summary>
    [ExecuteAlways]
    public class TransformChangeNotifier : MonoBehaviour
    {
        /// <summary>
        /// 変化時に通知されるイベントです。
        /// </summary>
        public TransformUnityEvent OnTransformChanged => _onTransformChanged;

        [SerializeField]
        private TransformUnityEvent _onTransformChanged = new();

        private TransformState _state;
        private bool _hierarchyDirty;

        void Awake()
        {
            _state = new TransformState(transform);
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

        void OnTransformParentChanged()
        {
            MarkDirty();
        }

        void OnTransformChildrenChanged()
        {
            MarkDirty();
        }

        void LateUpdate()
        {
            if (_state.HasChanged(transform))
            {
                _state.Capture(transform);
                _onTransformChanged.Invoke(transform);
            }

            if (_hierarchyDirty)
            {
                _hierarchyDirty = false;
                _state.Capture(transform);
                _onTransformChanged.Invoke(transform);
            }
        }

        /// <summary>
        /// 変化通知を強制的に送信します。
        /// </summary>
        public void ForceNotify()
        {
            _state.Capture(transform);
            _onTransformChanged.Invoke(transform);
        }

        /// <summary>
        /// リスナーを登録します。
        /// </summary>
        /// <param name="handler">ハンドラー</param>
        public void RegisterHandler(ITransformChangeHandler handler)
        {
            _onTransformChanged.AddListener(handler.OnTransformChanged);
        }

        /// <summary>
        /// リスナーを解除します。
        /// </summary>
        /// <param name="handler">ハンドラー</param>
        public void UnregisterHandler(ITransformChangeHandler handler)
        {
            _onTransformChanged.RemoveListener(handler.OnTransformChanged);
        }

        /// <summary>
        /// 階層変化を記録します。
        /// </summary>
        private void MarkDirty()
        {
            _hierarchyDirty = true;
        }
    }

    /// <summary>
    /// Transformを引数とするUnityEventです。
    /// </summary>
    [Serializable]
    public class TransformUnityEvent : UnityEvent<Transform>
    {
    }
}
