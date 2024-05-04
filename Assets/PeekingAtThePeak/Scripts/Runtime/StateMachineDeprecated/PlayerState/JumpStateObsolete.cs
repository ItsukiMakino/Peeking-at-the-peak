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
    public class JumpStateObsolete : BaseStateComponent
    {
        PlayerBehaiviour _playerComponets;
        [Inject]
        public void Constructer(PlayerBehaiviour _playerComponets)
        {
            this._playerComponets = _playerComponets;
        }
        public override async UniTask OnEnter(CancellationToken ct = default)
        {

            _playerComponets.HumanAnimator.SetFloat(_playerComponets.SpeedHash, 1);
            _playerComponets.MeronTopAnimator.SetFloat(_playerComponets.SpeedHash, 1);
            _playerComponets.SetTriggerToHumann(_playerComponets.JumpHash, true);

            if (_playerComponets.IsGrounded)
            {
                _playerComponets.Jump(_playerComponets.JumpVector, 1.1f);

            }
            if (_playerComponets.IsInversed)
            {
                _playerComponets.Jump(_playerComponets.JumpVector, 1.3f);
            }
            if (_playerComponets.IsTreeTouching)
            {

            }

        }
        public override async UniTask OnUpdate(CancellationToken ct = default)
        {

        }

        public override async UniTask OnExit(CancellationToken ct = default)
        {

            _playerComponets.SetTriggerToHumann(_playerComponets.JumpHash, false);

        }
    }

}
