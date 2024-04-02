using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;
using System.Linq;
using UniRx;

public class PlayState : BaseStateComponent
{
    PlayerComponets _playerComponets;
    KeyBindSystem keyBindSystem;
    SMB statemachineBehaviour;
    [Inject]
    public void Constructer(KeyBindSystem keyBindSystem, PlayerComponets _playerComponets, Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour)
    {
        this._playerComponets = _playerComponets;
        this.statemachineBehaviour = stateMachineBehaviour;
        this.keyBindSystem = keyBindSystem;
    }
    public override async UniTask OnEnter(CancellationToken ct = default)
    {
        await UniTask.WaitWhile(() => _playerComponets is null, cancellationToken: ct);
        keyBindSystem.MyAction.Player.Enable();


        float b = (float)SaveSystem.loadData.TargetFrame / 120;
        float modifiedValue = Mathf.Lerp(0.8f, 1.2f, SaveSystem.loadData.Senstivity);


        _playerComponets.CurrentSlowRotationSpeed = _playerComponets.SlowRotationSpeed * b * modifiedValue;
        _playerComponets.CurrentFastRotationSpeed = _playerComponets.FastRotationSpeed * b * modifiedValue;

        SaveSystem.loadData.IsTimeStop = false;
        if (SaveSystem.loadData.EnableShowTime)
        {
            _playerComponets.TimeSpanCanvas.enabled = true;
        }
        else
        {
            _playerComponets.TimeSpanCanvas.enabled = false;
        }
        UnityEngine.Debug.Log($"Current targetframe is {SaveSystem.loadData.TargetFrame},CurrentFastRotationSpeed is {_playerComponets.CurrentFastRotationSpeed}");
    }

    public override async UniTask OnUpdate(CancellationToken ct = default)
    {
        var value = keyBindSystem.MyAction.Player.Pose.ReadValue<float>();
        if (value == 1)
        {
            statemachineBehaviour.StateMachine.TriggerNextTransition(ZString.Concat("PlayToMenu"));
        }


        if (!SaveSystem.loadData.IsTimeStop)
        {
            _playerComponets.ElapsedTime += Time.deltaTime;
            SaveSystem.loadData.CurrentTime = TimeSpan.FromSeconds(_playerComponets.ElapsedTime);
            TimeSpan timeSpan = SaveSystem.loadData.CurrentTime;
            _playerComponets.TimeText.SetText(ZString.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10));

        }

    }

    public override async UniTask OnExit(CancellationToken ct = default)
    {
        keyBindSystem.MyAction.Player.Disable();
    }
    public bool IsPressEscape()
    {
        // var value = _playerComponets.InputActions.Player.Pose.ReadValue<float>();
        // if (value == 1)
        // {
        //     Debug.Log(value);
        //     return true;
        // }
        return false;
    }
}
