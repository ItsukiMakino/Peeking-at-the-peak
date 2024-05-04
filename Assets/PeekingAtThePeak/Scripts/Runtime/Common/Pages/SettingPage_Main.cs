using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MyGame.StateMachine;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;
namespace MyGame
{
    public class SettingPage_Main : Setting_Base
    {
        [Inject]
        private GameStateMachine gameStateMachine;
        public override void DidPushEnter()
        {

            SystemButton.OnSelectAsObservable().Subscribe(async _ =>
           {
               SelectButton(SystemButton, SelectedColor);
               UnselectButton(SoundButton, UnselectedColor);
               UnselectButton(ConfigButton, UnselectedColor);
               UnselectButton(CreditButton, UnselectedColor);
               loadUiSystem.PushPageWithInjection<Page_System>(settingPageContainer, ZString.Concat("PageSystem"), false, false);
           }).AddTo(this);

            SoundButton.OnSelectAsObservable().Subscribe(async _ =>
            {
                SelectButton(SoundButton, SelectedColor);
                UnselectButton(SystemButton, UnselectedColor);
                UnselectButton(ConfigButton, UnselectedColor);
                UnselectButton(CreditButton, UnselectedColor);
                loadUiSystem.PushPageWithInjection<Page_Sound>(settingPageContainer, ZString.Concat("PageSound"), false, false);
            }).AddTo(this);

            ConfigButton.OnSelectAsObservable().Subscribe(async _ =>
           {
               SelectButton(ConfigButton, SelectedColor);
               UnselectButton(SystemButton, UnselectedColor);
               UnselectButton(SoundButton, UnselectedColor);
               UnselectButton(CreditButton, UnselectedColor);
               loadUiSystem.PushPageWithInjection<Page_Config>(settingPageContainer, ZString.Concat("PageConfig"), false, false);

           }).AddTo(this);

            CreditButton.OnSelectAsObservable().Subscribe(async _ =>
           {
               SelectButton(CreditButton, SelectedColor);
               UnselectButton(SystemButton, UnselectedColor);
               UnselectButton(SoundButton, UnselectedColor);
               UnselectButton(ConfigButton, UnselectedColor);
               loadUiSystem.PushPageWithInjection<Page_Credit>(settingPageContainer, ZString.Concat("PageCredit"), false, false);

           }).AddTo(this);

            BackButton.OnSelectAsObservable().Subscribe(_ =>
            {
                gameStateMachine.ChangeState(gameStateMachine.playState).Forget();

            }).AddTo(this);

            SystemButton.OnClickAsObservable().Subscribe(_ => SoundSystem.Instance.PlaySE(7)).AddTo(this);
            SoundButton.OnClickAsObservable().Subscribe(_ => SoundSystem.Instance.PlaySE(7)).AddTo(this);
            ConfigButton.OnClickAsObservable().Subscribe(_ => SoundSystem.Instance.PlaySE(7)).AddTo(this);
            CreditButton.OnClickAsObservable().Subscribe(_ => SoundSystem.Instance.PlaySE(7)).AddTo(this);


        }

    }
}