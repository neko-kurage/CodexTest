using UnityEngine;

namespace GUISystem
{
    /// <summary>
    /// ビューポート単位を表す列挙です。
    /// </summary>
    public enum ViewportUnit
    {
        Pixels,
        Percent,
        Vw,
        Vh,
        Vmax,
        Vmin
    }

    /// <summary>
    /// ビューポート単位の値を保持する構造体です。
    /// </summary>
    [System.Serializable]
    public struct ViewportValue
    {
        /// <summary>
        /// 値です。
        /// </summary>
        public float Value;

        /// <summary>
        /// 単位です。
        /// </summary>
        public ViewportUnit Unit;

        /// <summary>
        /// 幅計算用のピクセル値を取得します。
        /// </summary>
        /// <param name="self">参照するRectTransform</param>
        /// <returns>ピクセル値</returns>
        public float ToWidthPixels(RectTransform self)
        {
            float parent = self.parent is RectTransform rt ? rt.rect.width : Screen.width;
            return ToPixels(parent);
        }

        /// <summary>
        /// 高さ計算用のピクセル値を取得します。
        /// </summary>
        /// <param name="self">参照するRectTransform</param>
        /// <returns>ピクセル値</returns>
        public float ToHeightPixels(RectTransform self)
        {
            float parent = self.parent is RectTransform rt ? rt.rect.height : Screen.height;
            return ToPixels(parent);
        }

        private float ToPixels(float parent)
        {
            switch (Unit)
            {
                case ViewportUnit.Percent:
                    return parent * Value * 0.01f;
                case ViewportUnit.Vw:
                    return Screen.width * Value * 0.01f;
                case ViewportUnit.Vh:
                    return Screen.height * Value * 0.01f;
                case ViewportUnit.Vmax:
                    return Mathf.Max(Screen.width, Screen.height) * Value * 0.01f;
                case ViewportUnit.Vmin:
                    return Mathf.Min(Screen.width, Screen.height) * Value * 0.01f;
                case ViewportUnit.Pixels:
                    return Value;
                default:
                    return Value;
            }
        }
    }
}
