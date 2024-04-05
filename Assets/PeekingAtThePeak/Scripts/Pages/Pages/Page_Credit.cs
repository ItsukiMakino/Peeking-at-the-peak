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

public class Page_Credit : Page
{


    LoadUiSystem loadUiSystem;
    PageContainer pageContainer;
    Pages_Model pages_Model;
    [Inject]
    public void Construct(LoadUiSystem loadUiSystem, PageContainer pageContainer, Pages_Model pages_Model)
    {

        this.loadUiSystem = loadUiSystem;
        this.pageContainer = pageContainer;
        this.pages_Model = pages_Model;
    }
    public override void DidPushEnter()
    {
        var token = this.GetCancellationTokenOnDestroy();

        //     BackButton.OnClickAsObservable().Subscribe(async _ =>
        //   {

        //   }).AddTo(this);


    }
}
