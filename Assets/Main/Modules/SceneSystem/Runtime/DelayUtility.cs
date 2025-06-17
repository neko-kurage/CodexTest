using UnityEngine;

namespace SceneSystem
{
    /// <summary>
    /// ロード時間調整のためのユーティリティクラスです。
    /// </summary>
    public static class DelayUtility
    {
        /// <summary>
        /// 指定時間が経過したかどうかを判定します。
        /// </summary>
        /// <param name="startTime">開始時間</param>
        /// <param name="minimum">最低待機時間</param>
        /// <param name="currentTime">現在の時間</param>
        /// <returns>経過していれば true</returns>
        public static bool IsTimeElapsed(float startTime, float minimum, float currentTime)
        {
            return currentTime - startTime >= minimum;
        }

        /// <summary>
        /// 残り時間を取得します。
        /// </summary>
        /// <param name="startTime">開始時間</param>
        /// <param name="minimum">最低待機時間</param>
        /// <param name="currentTime">現在の時間</param>
        /// <returns>残り時間</returns>
        public static float GetRemainingTime(float startTime, float minimum, float currentTime)
        {
            float elapsed = currentTime - startTime;
            return Mathf.Max(minimum - elapsed, 0f);
        }
    }
}
