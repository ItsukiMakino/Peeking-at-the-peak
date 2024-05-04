using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MyGame.StateMachine
{
    public class CrouchState : BaseState, IState
    {
        CrouchState(PlayerBehaiviour pb) : base(pb)
        {
            base.PB = pb;
        }

        public UniTask Start(CancellationToken ct)
        {
            PB.HumanAnimator.SetFloat(PB.SpeedHash, 1);
            PB.HumanAnimator.SetFloat(PB.TimeHash, 1);
            PB.MeronTopAnimator.SetFloat(PB.SpeedHash, 1);
            PB.MeronTopAnimator.SetFloat(PB.TimeHash, 1);
            PB.SetTriggerToHumann(PB.CrounchHash, true);
            if (!PB.IsTreeTouching)
            {
                PB.Jump(PB.JumpVector, 1.2f);
            }
            return UniTask.CompletedTask;
        }
        public UniTask Update(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            PB.SetTriggerToHumann(PB.CrounchHash, false);
            return UniTask.CompletedTask;
        }
    }

}