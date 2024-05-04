using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
using VContainer;
using UnityEngine.InputSystem;
namespace MyGame.Deprecated
{
    public class PlayerInitialStateObsolete : BaseStateComponent
    {
        PlayerBehaiviour _playerComponets;
        PlayerInput playerInput;
        [Inject]
        public void Constructer(PlayerBehaiviour _playerComponets, PlayerInput playerInput)
        {
            this._playerComponets = _playerComponets;
            this.playerInput = playerInput;
        }
        SMB stateMachine;
        public bool IsPressOnlySpace() => !playerInput.LowerAction.IsPressed() && !playerInput.UpperAction.IsPressed() && playerInput.JumpAction.IsPressed();
        public bool IsNotPressJump() => !playerInput.JumpAction.IsPressed();
        public bool IsPressCtrAndSpace() => playerInput.LowerAction.IsPressed() && playerInput.JumpAction.IsPressed();
        public bool IsPressShiftAndSpace() => playerInput.UpperAction.IsPressed() && playerInput.JumpAction.IsPressed();

        public bool IsAnyPressButton() => !playerInput.LowerAction.IsPressed() && !playerInput.UpperAction.IsPressed() && !playerInput.JumpAction.IsPressed();


        public override async UniTask OnEnter(CancellationToken ct = default)
        {
            await UniTask.WaitWhile(() => playerInput is null, cancellationToken: ct);
        }

        public override async UniTask OnUpdate(CancellationToken ct = default)
        {

        }

        public override async UniTask OnExit(CancellationToken ct = default)
        {

        }
        public bool Compliete()
        {
            if (playerInput.MyAction != null)
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
}