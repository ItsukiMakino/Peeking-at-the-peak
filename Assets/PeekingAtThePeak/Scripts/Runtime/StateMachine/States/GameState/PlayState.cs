using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;
namespace MyGame.StateMachine
{
    public class PlayState : BaseState, IState
    {
        PlayerInput playerInput;
        PlayState(PlayerBehaiviour pb, PlayerInput playerInput) : base(pb)
        {
            base.PB = pb;
            this.playerInput = playerInput;
        }
        public async UniTask Start(CancellationToken ct)
        {
            await UniTask.WaitWhile(() => PB is null, cancellationToken: ct);
            playerInput.MyAction.Player.Enable();


            float b = (float)SaveSystem.loadData.TargetFrame / 120;
            float modifiedValue = Mathf.Lerp(0.8f, 1.2f, SaveSystem.loadData.Senstivity);


            PB.CurrentSlowRotationSpeed = PB.SlowRotationSpeed * b * modifiedValue;
            PB.CurrentFastRotationSpeed = PB.FastRotationSpeed * b * modifiedValue;

            SaveSystem.loadData.IsTimeStop = false;
            if (SaveSystem.loadData.EnableShowTime)
            {
                PB.TimeSpanCanvas.enabled = true;
            }
            else
            {
                PB.TimeSpanCanvas.enabled = false;
            }
            UnityEngine.Debug.Log($"Current targetframe : {SaveSystem.loadData.TargetFrame},CurrentFastRotationSpeed : {PB.CurrentFastRotationSpeed}");
        }
        public UniTask Update(CancellationToken ct)
        {
            if (!SaveSystem.loadData.IsTimeStop)
            {
                PB.ElapsedTime += Time.deltaTime;
                SaveSystem.loadData.CurrentTime = TimeSpan.FromSeconds(PB.ElapsedTime);
                TimeSpan timeSpan = SaveSystem.loadData.CurrentTime;
                PB.TimeText.SetText(ZString.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10));

            }
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            playerInput.MyAction.Player.Disable();
            return UniTask.CompletedTask;
        }
    }

}