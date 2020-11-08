using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BananaLoader.Support
{
    internal static class Main
    {
        internal static bool IsDestroying = false;
        internal static GameObject obj = null;
        internal static BananaLoaderComponent comp = null;
        internal static int CurrentScene = -9;
        private static ISupportModule Initialize()
        {
            BananaLoaderComponent.Create();
            return new Module();
        }
    }

    public class BananaLoaderComponent : MonoBehaviour
    {
        internal static readonly List<IEnumerator> QueuedCoroutines = new List<IEnumerator>();
        internal static void Create()
        {
            Main.obj = new GameObject("BananaLoader");
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
        void Update()
        {
            transform.SetAsLastSibling();
            if (Main.CurrentScene != Application.loadedLevel)
            {
                SceneHandler.OnSceneLoad(Application.loadedLevel);
                Main.CurrentScene = Application.loadedLevel;
            }
            BananaHandler.OnUpdate();
        }
        void FixedUpdate() => BananaHandler.OnFixedUpdate();
        void LateUpdate() => BananaHandler.OnLateUpdate();
        void OnGUI() => BananaHandler.OnGUI();
        void OnDestroy() { if (!Main.IsDestroying) Create(); }
        void OnApplicationQuit() { Destroy(); BananaHandler.OnApplicationQuit(); }
    }
}