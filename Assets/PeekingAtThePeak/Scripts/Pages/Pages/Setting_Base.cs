using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;

public class Setting_Base : Page
{
    [SerializeField] Button _systemButton;
    [SerializeField] Button _soundButton;

    [SerializeField] Button _ConfigButton;
    [SerializeField] Button _creditButton;
    [SerializeField] Button _backButton;
    public Button SystemButton { get => _systemButton; set => _systemButton = value; }
    public Button SoundButton { get => _soundButton; set => _soundButton = value; }

    public Button ConfigButton { get => _ConfigButton; set => _ConfigButton = value; }
    public Button CreditButton { get => _creditButton; set => _creditButton = value; }
    public Button BackButton { get => _backButton; set => _backButton = value; }

    public Color UnselectedColor;
    public Color SelectedColor;



    protected LoadUiSystem loadUiSystem;
    protected PageContainer settingPageContainer;
    protected PageContainer pageContainer;

    [Inject]
    public void Construct(LoadUiSystem loadUiSystem)
    {

        this.loadUiSystem = loadUiSystem;
        this.settingPageContainer = PageContainer.Find(ZString.Concat("SettingContainer"));
        this.pageContainer = PageContainer.Find(ZString.Concat("Parent"));
        _systemButton.Select();

    }

    public override void DidPushEnter()
    {

        _systemButton.OnSelectAsObservable().Subscribe(async _ =>
       {
           UnityEngine.Debug.Log("system");
       }).AddTo(this);

        _soundButton.OnSelectAsObservable().Subscribe(async _ =>
       {

       }).AddTo(this);

        _ConfigButton.OnSelectAsObservable().Subscribe(async _ =>
       {


       }).AddTo(this);

        _creditButton.OnSelectAsObservable().Subscribe(async _ =>
       {


       }).AddTo(this);



    }
    public void SelectButton(Button button, Color selectColor)
    { button.image.color = selectColor; }
    public void UnselectButton(Button button, Color unselectColor)
    {
        button.image.color = unselectColor;
    }
}
