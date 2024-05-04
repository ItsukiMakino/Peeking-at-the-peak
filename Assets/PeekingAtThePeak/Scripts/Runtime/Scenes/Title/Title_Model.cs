using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Text;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Sheet;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
namespace MyGame
{
    public class Title_Model
    {
        LoadUISystem loadUiSystem;
        PageContainer pageContainer;
        Title_ViewComponet title_ViewComponet;
        public Title_Model(LoadUISystem loadUiSystem, PageContainer pageContainer, Title_ViewComponet title_ViewComponet)
        {
            this.loadUiSystem = loadUiSystem;
            this.pageContainer = pageContainer;
            this.title_ViewComponet = title_ViewComponet;
        }

        public async UniTask OnClickPlay(CancellationToken ct)
        {
            await SceneManager.LoadSceneAsync(ZString.Concat("Base"));
        }
        public async UniTask OnClickSettings(CancellationToken ct)
        {
            loadUiSystem.PushPageWithInjection<SettingPage_Title>(pageContainer, ZString.Concat("SettingPage_Title"), true, true);
            await title_ViewComponet.FadeOutCanvas(0f, ct);
        }
        public async UniTask OnClickBack(CancellationToken ct)
        {
            await pageContainer.Pop(false);
            await title_ViewComponet.FadeINCanvas(1f, ct);
        }

    }
}