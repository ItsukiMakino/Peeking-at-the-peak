using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;
public class JumpState : BaseStateComponent
{
    PlayerComponets _playerComponets;
    [Inject]
    public void Constructer(PlayerComponets _playerComponets)
    {
        this._playerComponets = _playerComponets;
    }
    public override async UniTask OnEnter(CancellationToken ct = default)
    {

        _playerComponets.HumanAnimator.SetFloat(ZString.Concat("Speed"), 1);
        _playerComponets.MeronTopAnimator.SetFloat(ZString.Concat("Speed"), 1);
        _playerComponets.SetTriggerToHumann(ZString.Concat("Jump"), true);

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

        _playerComponets.SetTriggerToHumann(ZString.Concat("Jump"), false);

    }
}
