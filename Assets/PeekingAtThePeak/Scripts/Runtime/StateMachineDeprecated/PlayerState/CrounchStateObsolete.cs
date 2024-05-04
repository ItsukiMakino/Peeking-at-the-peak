using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;
namespace MyGame.Deprecated
{
    public class CrounchStateObsolete : BaseStateComponent
    {
        PlayerBehaiviour _playerComponets;
        [Inject]
        public void Constructer(PlayerBehaiviour _playerComponets)
        {
            this._playerComponets = _playerComponets;
        }
        SMB stateMachine;
        public override async UniTask OnEnter(CancellationToken ct = default)
        {

            // _playerComponets.HumanAnimator.SetFloat(ZString.Concat("Speed"), 1);
            // _playerComponets.HumanAnimator.SetFloat(ZString.Concat("Time"), 1);
            // _playerComponets.MeronTopAnimator.SetFloat(ZString.Concat("Speed"), 1);
            // _playerComponets.MeronTopAnimator.SetFloat(ZString.Concat("Time"), 1);
            // _playerComponets.SetTriggerToHumann("Crounch", true);
            // if (!_playerComponets.IsTreeTouching)
            // {
            //     _playerComponets.Jump(_playerComponets.JumpVector, 1.2f);
            // }



        }

        public override async UniTask OnUpdate(CancellationToken ct = default)
        {
            // _playerComponets.CrounchAnimation(1, 0f);
        }

        public override async UniTask OnExit(CancellationToken ct = default)
        {

            // _playerComponets.SetTriggerToHumann("Crounch", false);

            // _playerComponets.
        }
    }
}