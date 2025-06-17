using UnityEngine;

namespace GUISystem
{
    /// <summary>
    /// Transformの変化を受け取るインターフェースです。
    /// </summary>
    public interface ITransformChangeHandler
    {
        /// <summary>
        /// Transformが変化した際に呼び出されます。
        /// </summary>
        /// <param name="changed">変化したTransform</param>
        void OnTransformChanged(Transform changed);
    }
}
