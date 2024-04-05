using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;
public class TitlePage : Page
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _settingButton;


    Title_Model title_Model;
    [Inject]
    public void Construct(Title_Model title_Model)
    {
        this.title_Model = title_Model;
    }

    public override IEnumerator WillPushEnter()
    {
        var token = this.GetCancellationTokenOnDestroy();

        _playButton.OnClickAsObservable().Subscribe(async _ =>
        {
            await SoundSystem.Instance.PlaySEasync(7, token);
            await UniTask.Delay(500, cancellationToken: token);
            await title_Model.OnClickPlay(token);

        }).AddTo(this);



        _settingButton.OnClickAsObservable().Subscribe(async _ =>
        {
            await SoundSystem.Instance.PlaySEasync(7, token);

            await title_Model.OnClickSettings(token);


        }).AddTo(this);
        yield break;
    }
}
