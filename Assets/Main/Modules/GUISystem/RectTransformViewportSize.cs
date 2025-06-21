using UnityEngine;

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

        private void UpdateSize()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            Vector2 size = _rectTransform.sizeDelta;
            size.x = _width.ToWidthPixels(_rectTransform);
            size.y = _height.ToHeightPixels(_rectTransform);
            _rectTransform.sizeDelta = size;
        }
    }
}
