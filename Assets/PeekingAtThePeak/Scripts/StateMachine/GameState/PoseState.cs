using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;
using UnityScreenNavigator.Runtime.Core.Page;


public class PoseState : BaseStateComponent
{
    PlayerAct input;
    PlayerComponets _playerComponets;
    Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour;
    LoadUiSystem loadUiSystem;
    PageContainer pageContainer;
    [Inject]
    public void Constructer(PlayerComponets _playerComponets, Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour,
     LoadUiSystem loadUiSystem, PageContainer pageContainer)
    {
        this._playerComponets = _playerComponets;
        this.stateMachineBehaviour = stateMachineBehaviour;
        this.loadUiSystem = loadUiSystem;
        this.pageContainer = pageContainer;
    }
    public override async UniTask OnEnter(CancellationToken ct = default)
    {

        await UniTask.WaitWhile(() => _playerComponets is null, cancellationToken: ct);
        await loadUiSystem.PushPage<ClearPage>(pageContainer, ZString.Concat("ClearPage"), true, stack: false);
    }

    public override async UniTask OnUpdate(CancellationToken ct = default)
    {
        await UniTask.Yield();
        // // Animation
        // SaveSystem.loadData.Poseing = true;
    }

    public override async UniTask OnExit(CancellationToken ct = default)
    {
        SaveSystem.loadData.CurrentTime = new System.TimeSpan();
    }
    public bool ToPlay()
    {
        if (input != null && (!SaveSystem.loadData.Poseing))
        {
            SaveSystem.loadData.Poseing = false;
            return true;
        }
        return false;
    }
    // public bool ToMenu()
    // {
    //     // bool IsPressed = input.Pose.Pose.IsPressed();
    //     return IsPressed;
    // }
}
