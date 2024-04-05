using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;

public class CrouchAndStrechState : BaseStateComponent
{
    PlayerComponets _playerComponets;
    [Inject]
    public void Constructer(PlayerComponets _playerComponets)
    {
        this._playerComponets = _playerComponets;
    }
    SMB stateMachine;
    public override async UniTask OnEnter(CancellationToken ct = default)
    {
        _playerComponets.HumanAnimator.SetFloat(ZString.Concat("Speed"), 1);
        _playerComponets.MeronTopAnimator.SetFloat(ZString.Concat("Speed"), 1);
        _playerComponets.SetTriggerToHumann(ZString.Concat("Idle"), true);

        if (_playerComponets.IsTreeTouching)
        {
            _playerComponets.Jump(_playerComponets.JumpVector, 2.0f);

        }
        if (_playerComponets.IsGool)
        {
            _playerComponets.Jump(_playerComponets.JumpVector, 4.0f);

        }



    }

    public override async UniTask OnUpdate(CancellationToken ct = default)
    {
        _playerComponets.CurrentRotationSpeed = _playerComponets.CurrentFastRotationSpeed;
    }

    public override async UniTask OnExit(CancellationToken ct = default)
    {
        _playerComponets.CurrentRotationSpeed = _playerComponets.CurrentSlowRotationSpeed;

        _playerComponets.SetTriggerToHumann(ZString.Concat("Idle"), false);

    }
}
