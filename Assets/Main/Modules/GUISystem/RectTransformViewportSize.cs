using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GUISystem
{
    /// <summary>
    /// RectTransformのサイズをビューポート単位で制御するコンポーネントです。
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformViewportSize : MonoBehaviour
    {
        /// <summary>
        /// 横幅設定です。
        /// </summary>
        public ViewportValue Width => _width;

        [SerializeField]
        private ViewportValue _width;

        /// <summary>
        /// 縦幅設定です。
        /// </summary>
        public ViewportValue Height => _height;

        [SerializeField]
        private ViewportValue _height;

        private RectTransform _rectTransform;

#if UNITY_EDITOR
        /// <summary>
        /// RectTransformのサイズを制御するトラッカーです。
        /// </summary>
        private DrivenRectTransformTracker _tracker;
#endif

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            UpdateSize();
        }

        void OnEnable()
        {
            UpdateSize();
        }

        void Update()
        {
            UpdateSize();
        }

        void OnDisable()
        {
#if UNITY_EDITOR
            // トラッカーをクリアしてプロパティの制御を解除
            _tracker.Clear();
#endif
        }

        void OnDestroy()
        {
#if UNITY_EDITOR
            // トラッカーをクリアしてプロパティの制御を解除
            _tracker.Clear();
#endif
        }

        /// <summary>
        /// サイズを更新します。
        /// </summary>
        private void UpdateSize()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

#if UNITY_EDITOR
            // エディター時にsizeDeltaを制御対象として追加
            _tracker.Add(this, _rectTransform, DrivenTransformProperties.SizeDelta);
#endif

            Vector2 size = _rectTransform.sizeDelta;
            size.x = _width.ToWidthPixels(_rectTransform);
            size.y = _height.ToHeightPixels(_rectTransform);
            _rectTransform.sizeDelta = size;
        }
    }
}
