using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;
using UnityScreenNavigator.Runtime.Core.Page;
public class MenuState : BaseStateComponent
{
    KeyBindSystem keyBindSystem;
    PlayerComponets _playerComponets;
    LoadUiSystem loadUiSystem;
    PageContainer pageContainer;
    [Inject]
    public void Constructer(KeyBindSystem keyBindSystem, PlayerComponets _playerComponets, LoadUiSystem loadUiSystem, PageContainer pageContainer
    , Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour)
    {
        this._playerComponets = _playerComponets;
        this.loadUiSystem = loadUiSystem;
        this.pageContainer = pageContainer;
        this.stateMachine = stateMachineBehaviour;
        this.keyBindSystem = keyBindSystem;
    }
    SMB stateMachine;
    public override async UniTask OnEnter(CancellationToken ct = default)
    {
        await UniTask.WaitWhile(() => keyBindSystem.MyAction is null, cancellationToken: ct);
        keyBindSystem.MyAction.UI.Enable();
        SaveSystem.loadData.IsTimeStop = true;
        await loadUiSystem.PushPage<SettingPage_Main>(pageContainer, ZString.Concat("SettingPage_Main"), false, stack: false);
    }

    public override async UniTask OnUpdate(CancellationToken ct = default)
    {
        var value = keyBindSystem.MyAction.UI.Back.ReadValue<float>();
        if (value == 1)
        {
            stateMachine.StateMachine.TriggerNextTransition(ZString.Concat("MenuToPlay"));
        }
        // else if (value == 1 && SaveSystem.loadData.Poseing)
        // {
        //     stateMachine.StateMachine.TriggerNextTransition(ZString.Concat("MenuToPose"));
        // }
    }

    public override async UniTask OnExit(CancellationToken ct = default)
    {
        keyBindSystem.MyAction.UI.Disable();
        await pageContainer.Pop(false);
    }
    public bool ToPose()
    {

        if (!SaveSystem.loadData.Poseing && keyBindSystem.MyAction.UI.Back.IsPressed())
            return true;
        return false;
    }
    public bool ToPlay()
    {

        return false;
    }
}
