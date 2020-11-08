using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BananaLoader.Support
{
    public class Module : ISupportModule
    {
        public int GetActiveSceneIndex() => SceneManager.GetActiveScene().buildIndex;
        public object StartCoroutine(IEnumerator coroutine) => BananaCoroutines.Start(coroutine);
        public void StopCoroutine(object coroutineToken) => BananaCoroutines.Stop((IEnumerator)coroutineToken);
        public void UnityDebugLog(string msg) => Debug.Log(msg);
        public void Destroy() => BananaLoaderComponent.Destroy();
    }
}