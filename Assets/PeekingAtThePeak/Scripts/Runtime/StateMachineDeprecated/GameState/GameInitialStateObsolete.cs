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
namespace MyGame.Deprecated
{
    public class GameInitialStateObsolete : BaseStateComponent
    {
        PlayerBehaiviour playerComponets;
        PlayerInput playerInput;
        SMB statemachineBehaviour;

        [Inject]
        public void Constructer(PlayerInput playerInput, PlayerBehaiviour playerComponets, Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour)
        {
            this.playerComponets = playerComponets;
            this.statemachineBehaviour = stateMachineBehaviour;
            this.playerInput = playerInput;
        }
        public override async UniTask OnEnter(CancellationToken ct = default)
        {

            await UniTask.WaitWhile(() => playerComponets is null, cancellationToken: ct);
            await UniTask.WaitWhile(() => SaveSystem.loadData is null, cancellationToken: ct);

            Application.targetFrameRate = SaveSystem.loadData.TargetFrame;
            playerInput.MyAction.Player.Enable();
            playerInput.MyAction.UI.Disable();
            SoundSystem.Instance.SetSEVolume();
            SoundSystem.Instance.SetBGMVolume();

            if (SaveSystem.loadData.InitializeGame)
            {
                playerComponets.PlayerTransfrom.position = SaveSystem.loadData.InitalPlayerPositon;
                SaveSystem.loadData.InitializeGame = false;
                SaveSystem.loadData.IsTimeStop = false;
                playerComponets.ElapsedTime = 0;
                Debug.Log("initialised player position : " + SaveSystem.loadData.InitalPlayerPositon);

            }
            else
            {
                playerComponets.PlayerTransfrom.position = SaveSystem.loadData.CurrentPlayerPositon;
                playerComponets.TimeText.SetText(SaveSystem.loadData.CurrentTime);
                SaveSystem.loadData.IsTimeStop = false;
                playerComponets.ElapsedTime = (float)SaveSystem.loadData.CurrentTime.TotalSeconds;
                Debug.Log("contenued player position : " + SaveSystem.loadData.CurrentPlayerPositon);
                Debug.Log("contenued play time : " + (float)SaveSystem.loadData.CurrentTime.TotalSeconds);


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
            if (playerInput.MyAction != null && !SaveSystem.loadData.Poseing)
            {
                return true;
            }
            return false;
        }
        public bool ToPlay()
        {
            if (playerInput.MyAction != null)
            {
                return true;
            }

            return false;
        }

    }
}