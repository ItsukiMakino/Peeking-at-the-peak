using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
public class Gool : MonoBehaviour
{
    [Inject]
    Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour;
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ZString.Concat("Player")))
        {
            stateMachineBehaviour.StateMachine.TriggerNextTransition(ZString.Concat("PlayToPose"));
        }
    }
}
