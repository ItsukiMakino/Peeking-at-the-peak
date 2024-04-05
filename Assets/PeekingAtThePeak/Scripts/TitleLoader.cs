
using Cysharp.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(ZString.Concat("Title"));
    }


}
