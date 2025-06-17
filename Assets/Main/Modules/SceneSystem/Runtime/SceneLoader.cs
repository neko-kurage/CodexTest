using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SceneSystem
{
    /// <summary>
    /// シーンのロードを制御するコンポーネントです。
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader _instance;

        [SerializeField]
        private SceneSettings _settings = null;

        [SerializeField]
        private float _minimumLoadTime = 1f;

        [SerializeField]
        private UnityEvent<float> _onProgress = new UnityEvent<float>();

        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        public static SceneLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SceneLoader");
                    _instance = obj.AddComponent<SceneLoader>();
                    DontDestroyOnLoad(obj);
                }

                return _instance;
            }
        }

        /// <summary>
        /// 進捗通知イベントを取得します。
        /// </summary>
        public UnityEvent<float> OnProgress
        {
            get { return _onProgress; }
        }

        /// <summary>
        /// シーンをロードします。
        /// </summary>
        /// <param name="sceneName">ロードするシーン名</param>
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            string loadingScene = _settings != null ? _settings.LoadingScene : string.Empty;
            if (!string.IsNullOrEmpty(loadingScene))
            {
                SceneManager.LoadScene(loadingScene);
                yield return null;
            }

            float startTime = Time.realtimeSinceStartup;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _onProgress.Invoke(progress);

                if (progress >= 0.9f && DelayUtility.IsTimeElapsed(startTime, _minimumLoadTime, Time.realtimeSinceStartup))
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
