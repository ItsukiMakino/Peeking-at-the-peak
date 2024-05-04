using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using Cysharp.Threading.Tasks;
using System.Threading;
using VContainer;
using UnityScreenNavigator.Runtime.Core.Page;

namespace MyGame.Deprecated
{
    public class PoseStateObsolete : BaseStateComponent
    {
        PlayerAct input;
        PlayerBehaiviour playerComponets;
        Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour;
        LoadUISystem loadUiSystem;
        PageContainer pageContainer;
        [Inject]
        public void Constructer(PlayerBehaiviour playerComponets, Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour,
         LoadUISystem loadUiSystem, PageContainer pageContainer)
        {
            this.playerComponets = playerComponets;
            this.stateMachineBehaviour = stateMachineBehaviour;
            this.loadUiSystem = loadUiSystem;
            this.pageContainer = pageContainer;
        }
        public override async UniTask OnEnter(CancellationToken ct = default)
        {

            await UniTask.WaitWhile(() => playerComponets is null, cancellationToken: ct);
            loadUiSystem.PushPageWithInjection<ClearPage>(pageContainer, "ClearPage", true, stack: false);
        }

        public override async UniTask OnUpdate(CancellationToken ct = default)
        {
            await UniTask.Yield();

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

    }
}