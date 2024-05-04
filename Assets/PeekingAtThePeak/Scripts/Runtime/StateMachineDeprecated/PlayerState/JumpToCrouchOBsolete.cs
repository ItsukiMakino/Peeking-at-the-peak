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
    public class JumpToCrouchOBsolete : BaseStateComponent
    {
        PlayerBehaiviour _playerComponets;
        [Inject]
        public void Constructer(PlayerBehaiviour _playerComponets)
        {
            this._playerComponets = _playerComponets;
        }

        public override async UniTask OnEnter(CancellationToken ct = default)
        {

            // _playerComponets.HumanAnimator.SetFloat("Speed", 1);
            // _playerComponets.MeronTopAnimator.SetFloat("Speed", 1);
            // _playerComponets.SetTriggerToHumann("JumpToCrouch", true);

            // if (_playerComponets.IsTreeTouching)
            // {
            //     _playerComponets.Jump(_playerComponets.JumpVector, 2.0f);
            // }
            // if (_playerComponets.IsGool)
            // {
            //     _playerComponets.Jump(_playerComponets.JumpVector, 4.0f);

            // }

        }

        public override async UniTask OnUpdate(CancellationToken ct = default)
        {

        }

        public override async UniTask OnExit(CancellationToken ct = default)
        {

            // _playerComponets.SetTriggerToHumann("JumpToCrouch", false);

        }
    }
}