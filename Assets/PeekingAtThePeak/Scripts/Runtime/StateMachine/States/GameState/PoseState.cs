using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityScreenNavigator.Runtime.Core.Page;

namespace MyGame.StateMachine
{
    public class PoseState : BaseState, IState
    {
        PlayerInput playerInput;
        LoadUISystem loadUISystem;
        PageContainer pageContainer;
        PoseState(PlayerBehaiviour pb,
            PlayerInput playerInput,
            LoadUISystem loadUISystem,
            PageContainer pageContainer
            ) : base(pb)
        {
            base.PB = pb;
            this.playerInput = playerInput;
            this.loadUISystem = loadUISystem;
            this.pageContainer = pageContainer;
        }

        public async UniTask Start(CancellationToken ct)
        {
            Debug.Log("poseState Enter");
            await UniTask.WaitWhile(() => PB == null, cancellationToken: ct);
            loadUISystem.PushPageWithInjection<ClearPage>(pageContainer, "ClearPage", true, stack: false);
            
        }
        public UniTask Update(CancellationToken ct)
        {
            // Debug.Log(nameof(Update));
            return UniTask.CompletedTask;
        }
        public async UniTask Exit(CancellationToken ct)
        {
            SaveSystem.loadData.CurrentTime = new System.TimeSpan();
           await UniTask.Yield();
        }
    }

}