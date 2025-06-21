using UnityEngine;

namespace GUISystem
{
    /// <summary>
    /// Transformの状態を保持し変化検知を行うクラスです。
    /// </summary>
    [System.Serializable]
    public class TransformState
    {
        /// <summary>
        /// ローカル位置
        /// </summary>
        public Vector3 LocalPosition { get; private set; }

        /// <summary>
        /// ローカル回転
        /// </summary>
        public Quaternion LocalRotation { get; private set; }

        /// <summary>
        /// ローカルスケール
        /// </summary>
        public Vector3 LocalScale { get; private set; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="target">監視対象のTransform</param>
        public TransformState(Transform target)
        {
            Capture(target);
        }

        /// <summary>
        /// 変化があるかどうか確認します。
        /// </summary>
        /// <param name="target">比較対象のTransform</param>
        /// <returns>変化があればtrue</returns>
        public bool HasChanged(Transform target)
        {
            return LocalPosition != target.localPosition ||
                   LocalRotation != target.localRotation ||
                   LocalScale != target.localScale;
        }

        /// <summary>
        /// 現在の状態を保存します。
        /// </summary>
        /// <param name="target">コピー元となるTransform</param>
        public void Capture(Transform target)
        {
            LocalPosition = target.localPosition;
            LocalRotation = target.localRotation;
            LocalScale = target.localScale;
        }
    }
}
