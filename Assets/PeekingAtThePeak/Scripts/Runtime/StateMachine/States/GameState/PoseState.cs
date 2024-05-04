using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MyGame.StateMachine
{
    public class PoseState : BaseState, IState
    {
        PlayerInput playerInput;
        PoseState(PlayerBehaiviour pb, PlayerInput playerInput) : base(pb)
        {
            base.PB = pb;
            this.playerInput = playerInput;
        }

        public UniTask Start(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Update(CancellationToken ct)
        {
            // Debug.Log(nameof(Update));
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
    }

}