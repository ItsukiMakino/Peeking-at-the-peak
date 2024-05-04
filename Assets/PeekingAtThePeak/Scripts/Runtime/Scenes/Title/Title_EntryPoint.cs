using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using UnityScreenNavigator.Runtime.Core.Page;
namespace MyGame
{
    public class Title_EntryPoint : IAsyncStartable
    {
        PageContainer pageContainer;
        LoadUISystem loadUiSystem;
        public Title_EntryPoint(PageContainer pageContainer, LoadUISystem loadUiSystem)
        {
            this.pageContainer = pageContainer;
            this.loadUiSystem = loadUiSystem;


        }
        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            await UniTask.WaitWhile(() => SaveSystem.loadData is null, cancellationToken: cancellationToken);
            Application.targetFrameRate = SaveSystem.loadData.TargetFrame;
            SoundSystem.Instance.SetSEVolume();
            SoundSystem.Instance.SetBGMVolume();

            loadUiSystem.PushPageWithInjection<TitlePage>(pageContainer, ZString.Concat("TitlePage"), true, true);

        }
    }
}