using System.Collections.Generic;
using UnityEngine;

namespace SceneSystem
{
    /// <summary>
    /// シーンの設定を管理する ScriptableObject です。
    /// </summary>
    [CreateAssetMenu(menuName = "SceneSystem/SceneSettings")]
    public class SceneSettings : ScriptableObject
    {
        [SerializeField]
        private string _loadingScene = string.Empty;

        [SerializeField]
        private List<string> _scenes = new List<string>();

        /// <summary>
        /// ロードシーン名を取得します。
        /// </summary>
        public string LoadingScene
        {
            get { return _loadingScene; }
        }

        /// <summary>
        /// 管理対象のシーン名一覧を取得します。
        /// </summary>
        public IReadOnlyList<string> Scenes
        {
            get { return _scenes; }
        }
    }
}
