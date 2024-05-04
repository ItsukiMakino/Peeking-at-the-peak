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
namespace MyGame.Deprecated
{
    public class PlayStateObsolete : BaseStateComponent
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
            playerInput.MyAction.Player.Enable();


            float b = (float)SaveSystem.loadData.TargetFrame / 120;
            float modifiedValue = Mathf.Lerp(0.8f, 1.2f, SaveSystem.loadData.Senstivity);


            playerComponets.CurrentSlowRotationSpeed = playerComponets.SlowRotationSpeed * b * modifiedValue;
            playerComponets.CurrentFastRotationSpeed = playerComponets.FastRotationSpeed * b * modifiedValue;

            SaveSystem.loadData.IsTimeStop = false;
            if (SaveSystem.loadData.EnableShowTime)
            {
                playerComponets.TimeSpanCanvas.enabled = true;
            }
            else
            {
                playerComponets.TimeSpanCanvas.enabled = false;
            }
            UnityEngine.Debug.Log($"Current targetframe : {SaveSystem.loadData.TargetFrame},CurrentFastRotationSpeed : {playerComponets.CurrentFastRotationSpeed}");
        }

        public override async UniTask OnUpdate(CancellationToken ct = default)
        {
            var value = playerInput.MyAction.Player.Pose.ReadValue<float>();
            if (value == 1)
            {
                statemachineBehaviour.StateMachine.TriggerNextTransition(ZString.Concat("PlayToMenu"));
            }


            if (!SaveSystem.loadData.IsTimeStop)
            {
                playerComponets.ElapsedTime += Time.deltaTime;
                SaveSystem.loadData.CurrentTime = TimeSpan.FromSeconds(playerComponets.ElapsedTime);
                TimeSpan timeSpan = SaveSystem.loadData.CurrentTime;
                playerComponets.TimeText.SetText(ZString.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10));

            }

        }

        public override async UniTask OnExit(CancellationToken ct = default)
        {
            playerInput.MyAction.Player.Disable();
        }
        public bool IsPressEscape()
        {
            // var value = playerComponets.InputActions.Player.Pose.ReadValue<float>();
            // if (value == 1)
            // {
            //     Debug.Log(value);
            //     return true;
            // }
            return false;
        }
    }
}