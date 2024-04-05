using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;

public class PlayerInitialState : BaseStateComponent
{
    PlayerComponets _playerComponets;
    KeyBindSystem keyBindSystem;
    [Inject]
    public void Constructer(PlayerComponets _playerComponets, KeyBindSystem keyBindSystem)
    {
        this._playerComponets = _playerComponets;
        this.keyBindSystem = keyBindSystem;
    }
    SMB stateMachine;
    public bool IsPressOnlySpace() => !keyBindSystem.LowerAction.IsPressed() && !keyBindSystem.UpperAction.IsPressed() && keyBindSystem.JumpAction.IsPressed();
    public bool IsNotPressJump() => !keyBindSystem.JumpAction.IsPressed();
    public bool IsPressCtrAndSpace() => keyBindSystem.LowerAction.IsPressed() && keyBindSystem.JumpAction.IsPressed();
    public bool IsPressShiftAndSpace() => keyBindSystem.UpperAction.IsPressed() && keyBindSystem.JumpAction.IsPressed();

    public bool IsAnyPressButton() => !keyBindSystem.LowerAction.IsPressed() && !keyBindSystem.UpperAction.IsPressed() && !keyBindSystem.JumpAction.IsPressed();


    public override async UniTask OnEnter(CancellationToken ct = default)
    {
        await UniTask.WaitWhile(() => keyBindSystem is null, cancellationToken: ct);
    }

    public override async UniTask OnUpdate(CancellationToken ct = default)
    {

    }

    public override async UniTask OnExit(CancellationToken ct = default)
    {

    }
    public bool Compliete()
    {
        if (keyBindSystem.MyAction != null)
        {
            return true;
        }
        else
            return false;
    }
    public void OnApplicationQuit()
    {
        SaveSystem.loadData.CurrentPlayerPositon = transform.position;
        SaveSystem.SaveAsync(SaveSystem.filePath, SaveSystem.loadData).Forget();

    }
}
