using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MyGame.StateMachine
{
    public class JumpState : BaseState, IState
    {
        JumpState(PlayerBehaiviour pb) : base(pb)
        {
            base.PB = pb;
        }

        public UniTask Start(CancellationToken ct)
        {
            PB.HumanAnimator.SetFloat(PB.SpeedHash, 1);
            PB.MeronTopAnimator.SetFloat(PB.SpeedHash, 1);
            PB.SetTriggerToHumann(PB.JumpHash, true);

            if (PB.IsGrounded)
            {
                PB.Jump(PB.JumpVector, 1.1f);

            }
            if (PB.IsInversed)
            {
                PB.Jump(PB.JumpVector, 1.3f);
            }

            return UniTask.CompletedTask;
        }
        public UniTask Update(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            PB.SetTriggerToHumann(PB.JumpHash, false);
            return UniTask.CompletedTask;
        }
    }

}