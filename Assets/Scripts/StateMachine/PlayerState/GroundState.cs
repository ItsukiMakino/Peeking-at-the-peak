using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;

public class GroundState : BaseStateComponent
{
    SMB stateMachine;
    public override async UniTask OnEnter(CancellationToken ct = default)
    {

    }

    public override async UniTask OnUpdate(CancellationToken ct = default)
    {

    }

    public override async UniTask OnExit(CancellationToken ct = default)
    {

    }
}
