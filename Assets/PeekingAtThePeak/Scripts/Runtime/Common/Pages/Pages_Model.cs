using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Bg.UniTaskStateMachine;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
namespace MyGame
{
    public class Pages_Model
    {
        public async UniTask OnClickSystemApply(CancellationToken token, TextMeshProUGUI resoText)
        {
            ApplyTargetFrame();
            ApplyResolution(resoText);
            await ApplyLanguage(token);
            SoundSystem.Instance.SetSEVolume();
            SoundSystem.Instance.SetBGMVolume();


        }

        public void ApplyTargetFrame()
        {
            switch (TargetFrameIndex)
            {
                case 0:
                    SaveSystem.loadData.TargetFrame = 60;
                    Application.targetFrameRate = 60;
                    SaveSystem.loadData.CurrentFrameIndex = 0;
                    break;
                case 1:
                    SaveSystem.loadData.TargetFrame = 80;
                    Application.targetFrameRate = 80;
                    SaveSystem.loadData.CurrentFrameIndex = 1;

                    break;
                case 2:
                    SaveSystem.loadData.TargetFrame = 100;
                    Application.targetFrameRate = 100;
                    SaveSystem.loadData.CurrentFrameIndex = 2;

                    break;
                case 3:
                    SaveSystem.loadData.TargetFrame = 120;
                    Application.targetFrameRate = 120;
                    SaveSystem.loadData.CurrentFrameIndex = 3;

                    break;
                default:
                    break;
            }
        }

        public async UniTask ApplyLanguage(CancellationToken token)
        {
            switch (langIndex)
            {
                case 0:
                    LocalizationSettings.SelectedLocale = Locale.CreateLocale(ZString.Concat("en"));
                    await LocalizationSettings.InitializationOperation.Task;
                    break;
                case 1:
                    LocalizationSettings.SelectedLocale = Locale.CreateLocale(ZString.Concat("ja"));
                    await LocalizationSettings.InitializationOperation.Task;
                    break;
                case 2:
                    LocalizationSettings.SelectedLocale = Locale.CreateLocale(ZString.Concat("zh"));
                    await LocalizationSettings.InitializationOperation.Task;
                    break;
                case 3:
                    LocalizationSettings.SelectedLocale = Locale.CreateLocale(ZString.Concat("ko"));
                    await LocalizationSettings.InitializationOperation.Task;
                    break;
                default:
                    break;
            }
            SaveSystem.loadData.CurentLangIndex = langIndex;
        }
        public void ApplyResolution(TextMeshProUGUI text)
        {
            if (SaveSystem.loadData.IsFullscrean)
            {
                Screen.SetResolution(1920, 1080, true);
                SaveSystem.loadData.IsFullscrean = true;
                SaveSystem.loadData.ResolutionIndex = 4;
                resoIndex = SaveSystem.loadData.ResolutionIndex;
                text.SetText("1920x1080");
            }
            else
            {
                switch (resoIndex)
                {
                    case 0:
                        Screen.SetResolution(896, 504, false);
                        text.SetText("896x504");
                        SaveSystem.loadData.IsFullscrean = false;
                        break;
                    case 1:
                        Screen.SetResolution(1024, 576, false);
                        text.SetText("1024x576");
                        SaveSystem.loadData.IsFullscrean = false;
                        break;
                    case 2:
                        Screen.SetResolution(1280, 720, false);
                        text.SetText("1280x720");
                        SaveSystem.loadData.IsFullscrean = false;
                        break;
                    case 3:
                        Screen.SetResolution(1664, 936, false);
                        text.SetText("1664x936");
                        SaveSystem.loadData.IsFullscrean = false;
                        break;
                    case 4:
                        Screen.SetResolution(1920, 1080, true);
                        text.SetText("1920x1080");
                        SaveSystem.loadData.IsFullscrean = true;
                        break;
                    default:
                        break;
                }
                SaveSystem.loadData.ResolutionIndex = resoIndex;
            }
        }
        public void OncklickConfigApply() { }
        public void SetResolutionText(int Index, TextMeshProUGUI text)
        {
            switch (Index)
            {
                case 0:
                    text.SetText("896x504");
                    break;
                case 1:
                    text.SetText("1024x576");
                    break;
                case 2:
                    text.SetText("1280x720");
                    break;
                case 3:
                    text.SetText("1664x936");
                    break;
                case 4:
                    text.SetText("1920x1080");
                    break;
                default:
                    break;
            }
        }
        public int resoIndex = SaveSystem.loadData.ResolutionIndex;
        public void OnClickResolutionLeft(TextMeshProUGUI text)
        {
            if (resoIndex == 0)
                return;
            resoIndex -= 1;


            SetResolutionText(resoIndex, text);
        }
        public void OnClickResolutionRight(TextMeshProUGUI text)
        {
            if (resoIndex == 4)
                return;
            resoIndex += 1;

            SetResolutionText(resoIndex, text);
        }
        public int TargetFrameIndex = SaveSystem.loadData.CurrentFrameIndex;
        public void SetTargetFrameText(int Index, TextMeshProUGUI text)
        {
            switch (Index)
            {
                case 0:
                    text.SetText("60");
                    break;
                case 1:
                    text.SetText("80");
                    break;
                case 2:
                    text.SetText("100");
                    break;
                case 3:
                    text.SetText("120");
                    break;
                default:
                    break;
            }
        }
        public void OnClickFrameCountLeft(TextMeshProUGUI text)
        {
            if (TargetFrameIndex == 0)
                return;
            TargetFrameIndex -= 1;
            SetTargetFrameText(TargetFrameIndex, text);
        }
        public void OnClickFrameCountRight(TextMeshProUGUI text)
        {
            if (TargetFrameIndex == 3)
                return;
            TargetFrameIndex += 1;

            SetTargetFrameText(TargetFrameIndex, text);
        }
        public void SetLanguageText(int Index, TextMeshProUGUI text)
        {
            switch (Index)
            {
                case 0:
                    text.SetText("English");
                    break;
                case 1:
                    text.SetText("日本語");
                    break;
                case 2:
                    text.SetText("中文");
                    break;
                case 3:
                    text.SetText("한국어");
                    break;
                default:
                    break;
            }
        }
        public int langIndex = SaveSystem.loadData.CurentLangIndex;
        public void OnClickLanguageRight(TextMeshProUGUI text)
        {
            langIndex += 1;
            if (langIndex > 3)
            {
                langIndex = 0;
            }
            // Debug.Log(langIndex);
            SetLanguageText(langIndex, text);
        }
        public void OnClickLanguageLeft(TextMeshProUGUI text)
        {
            langIndex -= 1;
            if (langIndex < 0)
            {
                langIndex = 3;
            }
            // Debug.Log(langIndex);

            SetLanguageText(langIndex, text);
        }

        public async UniTask ButtonAnimation(RectTransform rect, CancellationToken token = default)
        {
            Sequence sequence = DOTween.Sequence();
            await sequence
            .Append(rect.DOScale(0.8f, 0.08f))
            .Append(rect.DOScale(1.0f, 0.08f));
        }

        public void SelectButton(Button button, bool state = true)
        {
            Color defaultColor = button.image.color;
            if (state)
            {
                button.image.color = Color.yellow;
            }
            else
            {
                button.image.color = defaultColor;
            }
        }
    }
}