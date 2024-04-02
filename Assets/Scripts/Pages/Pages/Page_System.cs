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
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;

public class Page_System : Page
{
    [SerializeField] Button _applyButton;
    [SerializeField] Button _quitButton;
    [SerializeField] Button _restartButton;



    [SerializeField] Toggle _useFullScreen;
    [SerializeField] Toggle _showTimeSpan;



    [SerializeField] RectTransform _langRightRect;
    [SerializeField] RectTransform _langLeftRect;
    [SerializeField] RectTransform _resoRightRect;
    [SerializeField] RectTransform _resoLeftRect;
    [SerializeField] RectTransform _frameRightRect;
    [SerializeField] RectTransform _frameLeftRect;

    public Button _langRightButton;

    [SerializeField] Button _langLeftButton;
    [SerializeField] Button _resolutionRightButton;
    [SerializeField] Button _resolutionLeftButton;
    [SerializeField] Button _TargetFrameLeftButton;
    [SerializeField] Button _TargetFrameRightButton;
    [SerializeField] TextMeshProUGUI _languageText;
    [SerializeField] TextMeshProUGUI _resolutionText;
    [SerializeField] TextMeshProUGUI _frameRateText;
    [SerializeField] TextMeshProUGUI _clearCountText;
    [SerializeField] TextMeshProUGUI _bestTimeText;

    [SerializeField] GameObject _applyButtonObject;
    [SerializeField] GameObject _restartButtonObjeck;
    bool IsFullscrean;
    LoadUiSystem loadUiSystem;
    PageContainer pageContainer;
    Pages_Model pages_Model;
    [Inject]
    public void Construct(LoadUiSystem loadUiSystem, PageContainer pageContainer, Pages_Model pages_Model)
    {

        this.loadUiSystem = loadUiSystem;
        this.pageContainer = pageContainer;
        this.pages_Model = pages_Model;

        _clearCountText.SetText(SaveSystem.loadData.ClearCount);
        _bestTimeText.SetText(ZString.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", SaveSystem.loadData.BestTime.Hours, SaveSystem.loadData.BestTime.Minutes, SaveSystem.loadData.BestTime.Seconds, SaveSystem.loadData.BestTime.Milliseconds / 10));

        pages_Model.langIndex = SaveSystem.loadData.CurentLangIndex;
        pages_Model.resoIndex = SaveSystem.loadData.ResolutionIndex;
        pages_Model.TargetFrameIndex = SaveSystem.loadData.CurrentFrameIndex;

        pages_Model.SetTargetFrameText(SaveSystem.loadData.CurrentFrameIndex, _frameRateText);
        pages_Model.SetLanguageText(SaveSystem.loadData.CurentLangIndex, _languageText);
        pages_Model.SetResolutionText(SaveSystem.loadData.ResolutionIndex, _resolutionText);



    }
    public override IEnumerator WillPushEnter()
    {
        Setting_Base page = GetComponentInParent<SettingPage_Main>();
        page ??= GetComponentInParent<SettingPage_Title>();

        Navigation navigation = new Navigation();
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnRight = page.ConfigButton;
        navigation.selectOnDown = _langRightButton;
        page.SystemButton.navigation = navigation;

        Navigation LangRightNavigation = new Navigation();
        LangRightNavigation.mode = Navigation.Mode.Explicit;
        LangRightNavigation.selectOnLeft = _langLeftButton;
        LangRightNavigation.selectOnDown = _resolutionRightButton;
        LangRightNavigation.selectOnUp = page.ConfigButton;
        _langRightButton.navigation = LangRightNavigation;

        Navigation LangLeftNavigation = new Navigation();
        LangLeftNavigation.mode = Navigation.Mode.Explicit;
        LangLeftNavigation.selectOnRight = _langRightButton;
        LangLeftNavigation.selectOnDown = _resolutionLeftButton;
        LangLeftNavigation.selectOnUp = page.ConfigButton;
        _langLeftButton.navigation = LangLeftNavigation;

        _showTimeSpan.isOn = SaveSystem.loadData.EnableShowTime;

        if (SaveSystem.loadData.ResolutionIndex == 4)
        {
            _useFullScreen.isOn = true;
            SaveSystem.loadData.IsFullscrean = true;
        }
        else
        {
            _useFullScreen.isOn = false;
            SaveSystem.loadData.IsFullscrean = false;
        }




        var token = this.GetCancellationTokenOnDestroy();

        _langLeftButton.OnClickAsObservable().Subscribe(async _ =>
            {
                pages_Model.OnClickLanguageLeft(_languageText);
                _applyButtonObject.SetActive(true);
                SoundSystem.Instance.PlaySE(7);
                await pages_Model.ButtonAnimation(_langLeftRect, token);
            }).AddTo(this);

        _langRightButton.OnClickAsObservable().Subscribe(async _ =>
           {
               pages_Model.OnClickLanguageRight(_languageText);
               _applyButtonObject.SetActive(true);
               SoundSystem.Instance.PlaySE(7);

               await pages_Model.ButtonAnimation(_langRightRect, token);
           }).AddTo(this);

        _resolutionLeftButton.OnClickAsObservable().Subscribe(async _ =>
           {
               pages_Model.OnClickResolutionLeft(_resolutionText);
               _applyButtonObject.SetActive(true);
               SoundSystem.Instance.PlaySE(7);

               await pages_Model.ButtonAnimation(_resoLeftRect, token);
           }).AddTo(this);

        _resolutionRightButton.OnClickAsObservable().Subscribe(async _ =>
         {
             pages_Model.OnClickResolutionRight(_resolutionText);
             _applyButtonObject.SetActive(true);
             SoundSystem.Instance.PlaySE(7);

             await pages_Model.ButtonAnimation(_resoRightRect, token);
         }).AddTo(this);


        _TargetFrameLeftButton.OnClickAsObservable().Subscribe(async _ =>
            {
                pages_Model.OnClickFrameCountLeft(_frameRateText);
                _applyButtonObject.SetActive(true);
                SoundSystem.Instance.PlaySE(7);

                await pages_Model.ButtonAnimation(_frameLeftRect, token);
            }).AddTo(this);

        _TargetFrameRightButton.OnClickAsObservable().Subscribe(async _ =>
        {
            pages_Model.OnClickFrameCountRight(_frameRateText);
            _applyButtonObject.SetActive(true);
            SoundSystem.Instance.PlaySE(7);

            await pages_Model.ButtonAnimation(_frameRightRect, token);


        }).AddTo(this);

        _useFullScreen.OnValueChangedAsObservable().Subscribe(isActive =>
        {
            SaveSystem.loadData.IsFullscrean = isActive;
            // SoundSystem.Instance.PlaySE(7);
            _applyButtonObject.SetActive(true);
        }).AddTo(this);

        _showTimeSpan.OnValueChangedAsObservable().Subscribe(isActive =>
        {
            SaveSystem.loadData.EnableShowTime = isActive;
            // SoundSystem.Instance.PlaySE(7);

        }).AddTo(this);


        _applyButton.OnClickAsObservable().Subscribe(async _ =>
           {
               _applyButtonObject.SetActive(false);
               SoundSystem.Instance.PlaySE(7);

               await pages_Model.OnClickSystemApply(token, _resolutionText);
           }).AddTo(this);


        _quitButton.OnClickAsObservable().Subscribe(async _ =>
        {
            await SoundSystem.Instance.PlaySEasync(7, token);
            await UniTask.Delay(500, cancellationToken: token);
            Application.Quit();
        }).AddTo(this);


        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == ZString.Concat("Title"))
                {
                    _restartButtonObjeck.SetActive(false);
                }
            }
        }
        _restartButton.OnClickAsObservable().Subscribe(async _ =>
       {
           SaveSystem.loadData.InitializeGame = true;
           SaveSystem.loadData.IsTimeStop = false;
           await SoundSystem.Instance.PlaySEasync(7, token);
           await UniTask.Delay(500, cancellationToken: token);
           await SceneManager.LoadSceneAsync(ZString.Concat("Base"), LoadSceneMode.Single);
       }).AddTo(this);

        _applyButtonObject.SetActive(false);
        yield break;
    }
    public override IEnumerator Initialize()
    {

        yield break;
    }
}
