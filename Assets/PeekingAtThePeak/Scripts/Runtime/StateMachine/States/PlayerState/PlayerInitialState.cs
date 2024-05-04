using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MyGame.StateMachine
{
    public class PlayerInitialState : BaseState, IState
    {
        PlayerInitialState(PlayerBehaiviour pb) : base(pb)
        {
            base.PB = pb;
        }
        public UniTask Start(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Update(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
    }

}