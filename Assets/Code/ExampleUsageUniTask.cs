using System;
using UnityEngine;

namespace Code
{
    public class ExampleUsageUniTask : MonoBehaviour
    {
        public SceneControllerUniTask sceneController;

        private void Start()
        {
            sceneController.OnSceneLoadStarted += HandleSceneLoadStarted;
            sceneController.OnSceneLoadCompleted += HandleSceneLoadCompleted;
            sceneController.OnSceneLoadFailed += HandleSceneLoadFailed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                sceneController.LoadScene("SceneNameHere");
        }

        private void HandleSceneLoadStarted() =>
            Debug.Log("Scene loading started.");

        private void HandleSceneLoadCompleted() =>
            Debug.Log("Scene loading completed.");

        private void HandleSceneLoadFailed(Exception e) =>
            Debug.LogError($"Scene loading error: {e.Message}");

        private void OnDestroy()
        {
            sceneController.OnSceneLoadStarted -= HandleSceneLoadStarted;
            sceneController.OnSceneLoadCompleted -= HandleSceneLoadCompleted;
            sceneController.OnSceneLoadFailed -= HandleSceneLoadFailed;
        }
    }
}