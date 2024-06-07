using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MyGame.StateMachine
{
    public class IdleState : BaseState, IState
    {
        IdleState(PlayerBehaiviour pb) : base(pb)
        {
            base.PB = pb;
        }

        public UniTask Start(CancellationToken ct)
        {
            PB.HumanAnimator.SetFloat(PB.SpeedHash, 1);
            PB.MeronTopAnimator.SetFloat(PB.SpeedHash, 1);
            PB.SetTriggerToHumann(PB.IdleHash, true);
            if (PB.IsTreeTouching)
            {
                PB.Jump(PB.JumpVector, 2.0f);

            }
            if (PB.IsGool)
            {
                PB.Jump(PB.JumpVector, PB.BigJumpHeight);

            }
            return UniTask.CompletedTask;
        }
        public UniTask Update(CancellationToken ct)
        {
            PB.CurrentRotationSpeed = PB.CurrentFastRotationSpeed;
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            PB.CurrentRotationSpeed = PB.CurrentSlowRotationSpeed;

            PB.SetTriggerToHumann(PB.IdleHash, false);
            return UniTask.CompletedTask;
        }
    }

}