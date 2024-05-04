using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityScreenNavigator.Runtime.Core.Page;
using Cysharp.Text;
namespace MyGame.StateMachine
{
    public class MenuState : BaseState, IState
    {
        PlayerInput playerInput;
        PageContainer pageContainer;
        LoadUISystem loadUiSystem;
        MenuState(PlayerBehaiviour pb, PlayerInput playerInput, PageContainer pageContainer, LoadUISystem loadUiSystem) : base(pb)
        {
            base.PB = pb;
            this.playerInput = playerInput;
            this.pageContainer = pageContainer;
            this.loadUiSystem = loadUiSystem;
        }
        public async UniTask Start(CancellationToken ct)
        {
            await UniTask.WaitWhile(() => playerInput.MyAction is null, cancellationToken: ct);
            playerInput.MyAction.UI.Enable();
            SaveSystem.loadData.IsTimeStop = true;
            loadUiSystem.PushPageWithInjection<SettingPage_Main>(pageContainer, ZString.Concat("SettingPage_Main"), false, stack: false);
        }
        public UniTask Update(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public async UniTask Exit(CancellationToken ct)
        {
            playerInput.MyAction.UI.Disable();
            await pageContainer.Pop(false);
        }
    }

}