using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BananaLoader.Support
{
    internal static class Main
    {
        internal static bool IsDestroying = false;
        internal static GameObject obj = null;
        internal static BananaLoaderComponent comp = null;
        private static ISupportModule Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
            return new Module();
        }
        private static void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            if (obj == null) BananaLoaderComponent.Create();
            if (!scene.Equals(null)) SceneHandler.OnSceneLoad(scene.buildIndex);
        }
    }

    public class BananaLoaderComponent : MonoBehaviour
    {
        internal static readonly List<IEnumerator> QueuedCoroutines = new List<IEnumerator>();
        internal static void Create()
        {
            Main.obj = new GameObject();
            DontDestroyOnLoad(Main.obj);
            Main.comp = (BananaLoaderComponent)Main.obj.AddComponent(typeof(BananaLoaderComponent));
            Main.obj.transform.SetAsLastSibling();
            Main.comp.transform.SetAsLastSibling();
        }
        internal static void Destroy() { Main.IsDestroying = true; if (Main.obj != null) GameObject.Destroy(Main.obj); }
        void Awake()
        {
            foreach (var queuedCoroutine in QueuedCoroutines) StartCoroutine(queuedCoroutine);
            QueuedCoroutines.Clear();
        }
        void Start() => transform.SetAsLastSibling();
        void Update() { transform.SetAsLastSibling(); BananaHandler.OnUpdate(); }
        void FixedUpdate() => BananaHandler.OnFixedUpdate();
        void LateUpdate() => BananaHandler.OnLateUpdate();
        void OnGUI() => BananaHandler.OnGUI();
        void OnDestroy() { if (!Main.IsDestroying) Create(); }
        void OnApplicationQuit() { Destroy(); BananaHandler.OnApplicationQuit(); }
    }
}