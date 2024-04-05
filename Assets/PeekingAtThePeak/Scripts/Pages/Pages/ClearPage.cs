using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;

public class ClearPage : Page
{

    [SerializeField] Button _restartButton;
    [SerializeField] CanvasGroup _buttonCanvasGroup;

    [SerializeField] TextMeshProUGUI _clearTime;
    [SerializeField] TextMeshProUGUI _clearTimeText;

    [SerializeField] TextMeshProUGUI _bestTime;
    [SerializeField] TextMeshProUGUI _bestTimeText;


    [SerializeField] TextMeshProUGUI _clearCount;
    [SerializeField] TextMeshProUGUI _clearCountText;



    [Inject]
    public void Construct() { }
    public override IEnumerator Initialize()
    {
        SaveSystem.loadData.IsTimeStop = true;
        SaveSystem.loadData.ClearCount++;

        if (SaveSystem.loadData.CurrentTime < SaveSystem.loadData.BestTime || SaveSystem.loadData.BestTime == TimeSpan.Zero)
        {
            SaveSystem.loadData.BestTime = SaveSystem.loadData.CurrentTime;
        }



        _clearCount.SetText(SaveSystem.loadData.ClearCount);
        _clearTime.SetText(ZString.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", SaveSystem.loadData.CurrentTime.Hours, SaveSystem.loadData.CurrentTime.Minutes, SaveSystem.loadData.CurrentTime.Seconds, SaveSystem.loadData.CurrentTime.Milliseconds / 10));
        _bestTime.SetText(ZString.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", SaveSystem.loadData.BestTime.Hours, SaveSystem.loadData.BestTime.Minutes, SaveSystem.loadData.BestTime.Seconds, SaveSystem.loadData.BestTime.Milliseconds / 10));


        _restartButton.OnClickAsObservable().Subscribe(async _ =>
        {
            SaveSystem.loadData.InitializeGame = true;
            SaveSystem.loadData.IsTimeStop = false;
            await SceneManager.LoadSceneAsync(ZString.Concat("Base"), LoadSceneMode.Single);
        }).AddTo(this);
        SaveSystem.SaveAsync(SaveSystem.filePath, SaveSystem.loadData).Forget();
        yield break;
    }
    public override IEnumerator WillPushEnter()
    {
        _clearTime.alpha = 0f;
        _bestTime.alpha = 0f;
        _clearCount.alpha = 0f;
        _clearTimeText.alpha = 0f;
        _bestTimeText.alpha = 0f;
        _clearCountText.alpha = 0f;
        _buttonCanvasGroup.alpha = 0;


        DOTween.Sequence()
        .AppendInterval(2f)
       .Append(_clearTime.DOFade(1f, 0.5f))
       .Join(_clearTimeText.DOFade(1f, 0.5f))
       .Append(_bestTime.DOFade(1f, 0.5f))
       .Join(_bestTimeText.DOFade(1f, 0.5f))
       .Append(_clearCount.DOFade(1f, 0.5f))
       .Join(_clearCountText.DOFade(1f, 0.5f))
        .Append(_buttonCanvasGroup.DOFade(1f, 0.5f))
        .AppendInterval(0.5f);
        yield break;
    }
    public override IEnumerator WillPopEnter()
    {




        yield break;
    }
}
