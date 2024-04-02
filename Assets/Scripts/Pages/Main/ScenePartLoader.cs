using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum CheakMethod
{
    Distance,
    Trigger
}
public class ScenePartLoader : MonoBehaviour
{
    public Transform player;
    public CheakMethod cheakMethod;
    public float loadRange;
    public int BGMid;
    bool isLoaded;
    bool shouldLoad;
    CancellationToken token;
    void Start()
    {
        token = this.GetCancellationTokenOnDestroy();
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;

                }
            }
        }
    }
    void LateUpdate()
    {
        if (cheakMethod == CheakMethod.Distance)
        {
            DistanceCheak();
        }
        else if (cheakMethod == CheakMethod.Trigger)
        {
            TriggerCheak();
        }
    }
    void DistanceCheak()
    {
        if (Vector2.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene(token).Forget();
        }
        else
        {
            UnLoadScene(token).Forget();
        }
    }
    void TriggerCheak()
    {
        if (shouldLoad && !token.IsCancellationRequested)
        {
            LoadScene(token).Forget();
        }
        else
        {
            UnLoadScene(token).Forget();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ZString.Concat("Player")))
        {
            shouldLoad = true;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(ZString.Concat("Player")))
        {
            shouldLoad = false;

        }
    }
    async UniTask LoadScene(CancellationToken token)
    {
        if (!isLoaded)
        {
            isLoaded = true;

            await SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
        }
    }
    async UniTask UnLoadScene(CancellationToken token)
    {
        if (isLoaded)
        {
            isLoaded = false;
            await SceneManager.UnloadSceneAsync(gameObject.name);



        }
    }

}
