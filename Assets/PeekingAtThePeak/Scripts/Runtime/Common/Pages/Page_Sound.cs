using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;
namespace MyGame
{
    public class Page_Sound : Page
    {

        [SerializeField] Slider SeVolumeSlider;
        [SerializeField] Slider BGMVolumeSlider;
        [SerializeField] Slider MasterVolumeSlider;

        LoadUISystem loadUiSystem;
        PageContainer pageContainer;
        Pages_Model pages_Model;
        [Inject]
        public void Construct(LoadUISystem loadUiSystem, PageContainer pageContainer, Pages_Model pages_Model)
        {

            this.loadUiSystem = loadUiSystem;
            this.pageContainer = pageContainer;
            this.pages_Model = pages_Model;
        }
        public override IEnumerator WillPushEnter()
        {
            var token = this.GetCancellationTokenOnDestroy();
            SeVolumeSlider.value = SaveSystem.loadData.SeVolume;
            BGMVolumeSlider.value = SaveSystem.loadData.BgmVolume;
            MasterVolumeSlider.value = SaveSystem.loadData.MasterVolume;

            SeVolumeSlider.OnValueChangedAsObservable().Subscribe(value =>
            {
                SaveSystem.loadData.SeVolume = value;
                SoundSystem.Instance.SetSEVolume();
            }).AddTo(this);

            BGMVolumeSlider.OnValueChangedAsObservable().Subscribe(value =>
           {
               SaveSystem.loadData.BgmVolume = value;
               SoundSystem.Instance.SetBGMVolume();
           }).AddTo(this);

            MasterVolumeSlider.OnValueChangedAsObservable().Subscribe(value =>
            {
                SaveSystem.loadData.MasterVolume = value;
                SoundSystem.Instance.SetBGMVolume();
                SoundSystem.Instance.SetSEVolume();
            }).AddTo(this);
            yield break;

        }
    }
}