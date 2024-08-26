using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public sealed class SceneControllerUniTask : MonoBehaviour
    {
        [SerializeField] private float loadDelay = 0.5f;
        [SerializeField] private CanvasGroup loadingScreen;
    
        public event Action OnSceneLoadStarted;
        public event Action OnSceneLoadCompleted;
        public event Action<Exception> OnSceneLoadFailed;

        private void Awake() => 
            InitializeLoadingScreen();

        public void LoadScene(string sceneName) => 
            LoadSceneAsync(sceneName).Forget();

        public void ReloadScene() => 
            ReloadSceneAsync().Forget();

        public void LoadNextScene() => 
            LoadNextSceneAsync().Forget();

        private void InitializeLoadingScreen()
        {
            if (loadingScreen == null) return;
            loadingScreen.alpha = 0;
            loadingScreen.gameObject.SetActive(false);
        }

        private async UniTaskVoid LoadSceneAsync(string sceneName)
        {
            OnSceneLoadStarted?.Invoke();
            await ShowLoadingScreen();
            await UniTask.Delay((int)(loadDelay * 1000));

            try
            {
                await LoadSceneInternal(sceneName);
                await HideLoadingScreen();
                OnSceneLoadCompleted?.Invoke();
            }
            catch (Exception e)
            {
                HandleSceneLoadFailed(e);
            }
        }

        private async UniTaskVoid ReloadSceneAsync()
        {
            OnSceneLoadStarted?.Invoke();
            await ShowLoadingScreen();
            await UniTask.Delay((int)(loadDelay * 1000));

            try
            {
                var currentScene = SceneManager.GetActiveScene();
            
                await LoadSceneInternal(currentScene.name);
                await HideLoadingScreen();
                OnSceneLoadCompleted?.Invoke();
            }
            catch (Exception e)
            {
                HandleSceneLoadFailed(e);
            }
        }

        private async UniTaskVoid LoadNextSceneAsync()
        {
            OnSceneLoadStarted?.Invoke();
            await ShowLoadingScreen();
            await UniTask.Delay((int)(loadDelay * 1000));

            try
            {
                var currentScene = SceneManager.GetActiveScene();
                int nextSceneIndex = currentScene.buildIndex + 1;
            
                if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                    throw new InvalidOperationException("There is no next scene to load.");
            
                await SceneManager.LoadSceneAsync(nextSceneIndex).ToUniTask();
                await HideLoadingScreen();
                OnSceneLoadCompleted?.Invoke();
            }
            catch (Exception e)
            {
                HandleSceneLoadFailed(e);
            }
        }

        private async UniTask ShowLoadingScreen()
        {
            if (loadingScreen == null) return;
            loadingScreen.gameObject.SetActive(true);
            await FadeLoadingScreen(1);
        }

        private async UniTask HideLoadingScreen()
        {
            if (loadingScreen == null) return;
            await FadeLoadingScreen(0);
            loadingScreen.gameObject.SetActive(false);
        }

        private async UniTask LoadSceneInternal(string sceneName)
        {
            if (!IsSceneExists(sceneName))
                throw new ArgumentException($"Scene with name '{sceneName}' does not exist.");

            await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        }

        private void HandleSceneLoadFailed(Exception e)
        {
            OnSceneLoadFailed?.Invoke(e);
            Debug.LogError($"An error occurred while loading the scene: {e.Message}");
        }

        private async UniTask FadeLoadingScreen(float targetAlpha)
        {
            const float fadeSpeed = 2f;
            float currentAlpha = loadingScreen.alpha;

            while (!Mathf.Approximately(currentAlpha, targetAlpha))
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
                loadingScreen.alpha = currentAlpha;
                await UniTask.Yield();
            }
        }

        private bool IsSceneExists(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                if (sceneFileName == sceneName)
                    return true;
            }
            return false;
        }
    }
}
