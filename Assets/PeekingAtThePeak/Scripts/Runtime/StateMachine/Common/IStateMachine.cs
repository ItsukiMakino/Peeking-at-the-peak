using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyGame.StateMachine
{
     public interface IStateMachine
     {
          UniTask ChangeState<T>(T state, CancellationToken ct = default) where T : IState;
     }
}