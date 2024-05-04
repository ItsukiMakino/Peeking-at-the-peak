using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;
using UnityEngine.InputSystem;
using TMPro;
using System.IO;
namespace MyGame
{
    public class Page_Config : Page
    {
        [SerializeField] Button _defaultButton;
        [SerializeField] Button _leftConfig;
        [SerializeField] Button _rightConfig;
        [SerializeField] Button _upperConfig;
        [SerializeField] Button _lowerConfig;
        [SerializeField] Button _jumpConfig;
        [SerializeField] Slider _senstivitySlider;

        [SerializeField] TMP_Text _leftConfigPathText;
        [SerializeField] TMP_Text _rightConfigPathText;
        [SerializeField] TMP_Text _upperConfigText;
        [SerializeField] TMP_Text _lowerConfigPathText;
        [SerializeField] TMP_Text _jumpConfigPathText;


        LoadUISystem loadUiSystem;
        PageContainer pageContainer;
        Pages_Model pages_Model;
        PlayerInput playerInput;
        GameObject ConfigMaskPrefab;
        [Inject]
        public void Construct(PlayerInput playerInput, LoadUISystem loadUiSystem, PageContainer pageContainer, Pages_Model pages_Model
        , GameObject ConfigMaskPrefab)
        {

            this.loadUiSystem = loadUiSystem;
            this.pageContainer = pageContainer;
            this.pages_Model = pages_Model;
            this.playerInput = playerInput;
            this.ConfigMaskPrefab = ConfigMaskPrefab;
            playerInput.RefreshDisplay(playerInput.LeftAction, _leftConfigPathText);
            playerInput.RefreshDisplay(playerInput.RightAction, _rightConfigPathText);
            playerInput.RefreshDisplay(playerInput.UpperAction, _upperConfigText);
            playerInput.RefreshDisplay(playerInput.LowerAction, _lowerConfigPathText);
            playerInput.RefreshDisplay(playerInput.JumpAction, _jumpConfigPathText);

        }
        public override IEnumerator WillPushEnter()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _senstivitySlider.value = SaveSystem.loadData.Senstivity;
            _leftConfig.OnClickAsObservable().Subscribe(_ =>
            {
                playerInput.StartRebinding(playerInput.LeftAction, _leftConfigPathText, ConfigMaskPrefab);
            }).AddTo(this);

            _rightConfig.OnClickAsObservable().Subscribe(_ =>
           {
               playerInput.StartRebinding(playerInput.RightAction, _rightConfigPathText, ConfigMaskPrefab);
           }).AddTo(this);

            _upperConfig.OnClickAsObservable().Subscribe(_ =>
           {
               playerInput.StartRebinding(playerInput.UpperAction, _upperConfigText, ConfigMaskPrefab);
           }).AddTo(this);

            _lowerConfig.OnClickAsObservable().Subscribe(_ =>
           {
               playerInput.StartRebinding(playerInput.LowerAction, _lowerConfigPathText, ConfigMaskPrefab);
           }).AddTo(this);

            _jumpConfig.OnClickAsObservable().Subscribe(_ =>
           {
               playerInput.StartRebinding(playerInput.JumpAction, _jumpConfigPathText, ConfigMaskPrefab);
           }).AddTo(this);


            _senstivitySlider.OnValueChangedAsObservable().Subscribe(sliderValue =>
            {
                SaveSystem.loadData.Senstivity = sliderValue;

            }).AddTo(this);

            _defaultButton.OnClickAsObservable().Subscribe(_ =>
            {
                playerInput.ResetOverrides(playerInput.LeftAction, _leftConfigPathText);
                playerInput.ResetOverrides(playerInput.RightAction, _rightConfigPathText);
                playerInput.ResetOverrides(playerInput.UpperAction, _upperConfigText);
                playerInput.ResetOverrides(playerInput.LowerAction, _lowerConfigPathText);
                playerInput.ResetOverrides(playerInput.JumpAction, _jumpConfigPathText);
                _senstivitySlider.value = 0.5f;
                playerInput.Save();
            }).AddTo(this);

            yield break;
        }
    }
}