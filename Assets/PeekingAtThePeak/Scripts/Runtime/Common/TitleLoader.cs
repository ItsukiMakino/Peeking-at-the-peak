using UnityEngine;
using UnityEngine.SceneManagement;
namespace MyGame
{
    public class TitleLoader : MonoBehaviour
    {
        const string sceneName = "Title";
        void Start()
        {
            SceneManager.LoadScene(sceneName);
        }


    }
}