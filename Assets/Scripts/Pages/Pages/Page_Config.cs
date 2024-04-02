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


    LoadUiSystem loadUiSystem;
    PageContainer pageContainer;
    Pages_Model pages_Model;
    KeyBindSystem keyBindSystem;
    GameObject ConfigMaskPrefab;
    [Inject]
    public void Construct(KeyBindSystem keyBindSystem, LoadUiSystem loadUiSystem, PageContainer pageContainer, Pages_Model pages_Model
    , GameObject ConfigMaskPrefab)
    {

        this.loadUiSystem = loadUiSystem;
        this.pageContainer = pageContainer;
        this.pages_Model = pages_Model;
        this.keyBindSystem = keyBindSystem;
        this.ConfigMaskPrefab = ConfigMaskPrefab;
        keyBindSystem.RefreshDisplay(keyBindSystem.LeftAction, _leftConfigPathText);
        keyBindSystem.RefreshDisplay(keyBindSystem.RightAction, _rightConfigPathText);
        keyBindSystem.RefreshDisplay(keyBindSystem.UpperAction, _upperConfigText);
        keyBindSystem.RefreshDisplay(keyBindSystem.LowerAction, _lowerConfigPathText);
        keyBindSystem.RefreshDisplay(keyBindSystem.JumpAction, _jumpConfigPathText);

    }
    public override IEnumerator WillPushEnter()
    {
        var token = this.GetCancellationTokenOnDestroy();
        _senstivitySlider.value = SaveSystem.loadData.Senstivity;
        _leftConfig.OnClickAsObservable().Subscribe(_ =>
        {
            keyBindSystem.StartRebinding(keyBindSystem.LeftAction, _leftConfigPathText, ConfigMaskPrefab);
        }).AddTo(this);

        _rightConfig.OnClickAsObservable().Subscribe(_ =>
       {
           keyBindSystem.StartRebinding(keyBindSystem.RightAction, _rightConfigPathText, ConfigMaskPrefab);
       }).AddTo(this);

        _upperConfig.OnClickAsObservable().Subscribe(_ =>
       {
           keyBindSystem.StartRebinding(keyBindSystem.UpperAction, _upperConfigText, ConfigMaskPrefab);
       }).AddTo(this);

        _lowerConfig.OnClickAsObservable().Subscribe(_ =>
       {
           keyBindSystem.StartRebinding(keyBindSystem.LowerAction, _lowerConfigPathText, ConfigMaskPrefab);
       }).AddTo(this);

        _jumpConfig.OnClickAsObservable().Subscribe(_ =>
       {
           keyBindSystem.StartRebinding(keyBindSystem.JumpAction, _jumpConfigPathText, ConfigMaskPrefab);
       }).AddTo(this);


        _senstivitySlider.OnValueChangedAsObservable().Subscribe(sliderValue =>
        {
            SaveSystem.loadData.Senstivity = sliderValue;

        }).AddTo(this);

        _defaultButton.OnClickAsObservable().Subscribe(_ =>
        {
            keyBindSystem.ResetOverrides(keyBindSystem.LeftAction, _leftConfigPathText);
            keyBindSystem.ResetOverrides(keyBindSystem.RightAction, _rightConfigPathText);
            keyBindSystem.ResetOverrides(keyBindSystem.UpperAction, _upperConfigText);
            keyBindSystem.ResetOverrides(keyBindSystem.LowerAction, _lowerConfigPathText);
            keyBindSystem.ResetOverrides(keyBindSystem.JumpAction, _jumpConfigPathText);
            _senstivitySlider.value = 0.5f;
            keyBindSystem.Save();
        }).AddTo(this);

        yield break;
    }
}
