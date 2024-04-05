using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using UnityEngine.InputSystem;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;
using UniRx.Triggers;
public class GameInitialState : BaseStateComponent
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
        await UniTask.WaitWhile(() => SaveSystem.loadData is null, cancellationToken: ct);

        Application.targetFrameRate = SaveSystem.loadData.TargetFrame;
        keyBindSystem.MyAction.Player.Enable();
        keyBindSystem.MyAction.UI.Disable();
        SoundSystem.Instance.SetSEVolume();
        SoundSystem.Instance.SetBGMVolume();

        if (SaveSystem.loadData.InitializeGame)
        {
            _playerComponets.PlayerTransfrom.position = SaveSystem.loadData.InitalPlayerPositon;
            SaveSystem.loadData.InitializeGame = false;
            SaveSystem.loadData.IsTimeStop = false;
            _playerComponets.ElapsedTime = 0;
            Debug.Log("initialised " + SaveSystem.loadData.InitalPlayerPositon);

        }
        else
        {
            _playerComponets.PlayerTransfrom.position = SaveSystem.loadData.CurrentPlayerPositon;
            _playerComponets.TimeText.SetText(SaveSystem.loadData.CurrentTime);
            SaveSystem.loadData.IsTimeStop = false;
            _playerComponets.ElapsedTime = (float)SaveSystem.loadData.CurrentTime.TotalSeconds;
            Debug.Log("contenued " + SaveSystem.loadData.CurrentPlayerPositon);
            Debug.Log("contenued " + (float)SaveSystem.loadData.CurrentTime.TotalSeconds);


        }



    }

    public override async UniTask OnUpdate(CancellationToken ct = default)
    {

    }

    public override async UniTask OnExit(CancellationToken ct = default)
    {

    }

    public bool ToPose()
    {
        if (keyBindSystem.MyAction != null && !SaveSystem.loadData.Poseing)
        {
            return true;
        }
        return false;
    }
    public bool ToPlay()
    {
        if (keyBindSystem.MyAction != null)
        {
            return true;
        }

        return false;
    }

}
