using System.Collections;
using System.Collections.Generic;
using System.Security;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Threading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UnityScreenNavigator.Runtime.Core.Page;
using System;
using UniRx;
using UnityEngine.UI;
using Cysharp.Text;
using BG = Bg.UniTaskStateMachine;


public class Main_EntryPoint : IAsyncStartable
{
    PlayerComponets playerComponets;
    BG.StateMachineBehaviour stateMachineBehaviour;
    public Main_EntryPoint(PlayerComponets playerComponets, BG.StateMachineBehaviour stateMachineBehaviour)
    {
        this.playerComponets = playerComponets;
        this.stateMachineBehaviour = stateMachineBehaviour;
    }
    public async UniTask StartAsync(CancellationToken cancellationToken)
    {

    }
}