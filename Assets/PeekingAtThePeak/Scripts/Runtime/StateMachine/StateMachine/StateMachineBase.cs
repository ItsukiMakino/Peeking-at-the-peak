using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using VContainer.Unity;

namespace MyGame.StateMachine
{
    public class StateMachineBase : IStateMachine
    {

        protected bool updatable = true;
        public bool Updatable => updatable;
        protected IState currentState;
        public IState CurrentState => currentState;

        public void Stop()
        {
            updatable = false;
        }
        public void Restore()
        {
            updatable = true;
        }
        public async UniTask ChangeState<T>(T newState, CancellationToken ct = default) where T : IState
        {
            if (currentState != null && !currentState.Equals(newState))
            {
                if (currentState != null)
                {
                    await currentState.Exit(ct);
                }
                currentState = newState;
                await currentState.Start(ct);
            }
        }
    }
}