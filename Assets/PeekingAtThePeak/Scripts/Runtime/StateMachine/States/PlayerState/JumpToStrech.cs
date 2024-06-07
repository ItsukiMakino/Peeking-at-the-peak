using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MyGame.StateMachine
{
    public class JumpToStrech : BaseState, IState
    {
        JumpToStrech(PlayerBehaiviour pb) : base(pb)
        {
            base.PB = pb;
        }

        public UniTask Start(CancellationToken ct)
        {
            PB.HumanAnimator.SetFloat(PB.SpeedHash, 1);
            PB.MeronTopAnimator.SetFloat(PB.SpeedHash, 1);
            PB.SetTriggerToHumann(PB.JumpToStrechHash, true);
            if (PB.IsTreeTouching)
            {
                PB.Jump(PB.JumpVector, 2.0f);
            }
            if (PB.IsGool)
            {
                PB.Jump(PB.JumpVector, 4.0f);

            }
            return UniTask.CompletedTask;
        }
        public UniTask Update(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            PB.SetTriggerToHumann(PB.JumpToStrechHash, false);
            return UniTask.CompletedTask;
        }
    }

}