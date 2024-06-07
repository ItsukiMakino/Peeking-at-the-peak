using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using MyGame.StateMachine;
using UnityEngine;
using VContainer;
namespace MyGame
{
    public class Gool : MonoBehaviour
    {
        [Inject]
        GameStateMachine gameStateMachine;

        string tagName = "Player";
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(tagName))
            {
                gameStateMachine.ChangeState(gameStateMachine.poseState).Forget();
                
            }
        }
    }
}