using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Text;
using UnityEngine;
using System.Threading;


namespace MyGame.StateMachine
{
    public class InitalState : BaseState, IState
    {
        InitalState(PlayerBehaiviour pb) : base(pb)
        {
            base.PB = pb;
        }
        public async UniTask Start(CancellationToken ct)
        {
            await UniTask.WaitWhile(() => PB is null, cancellationToken: ct);
            await UniTask.WaitWhile(() => SaveSystem.loadData is null, cancellationToken: ct);

            Application.targetFrameRate = SaveSystem.loadData.TargetFrame;
            SoundSystem.Instance.SetSEVolume();
            SoundSystem.Instance.SetBGMVolume();

            if (SaveSystem.loadData.InitializeGame)
            {
                PB.PlayerTransfrom.position = SaveSystem.loadData.InitalPlayerPositon;
                SaveSystem.loadData.InitializeGame = false;
                SaveSystem.loadData.IsTimeStop = false;
                PB.ElapsedTime = 0;
                Debug.Log("initialised player position : " + SaveSystem.loadData.InitalPlayerPositon);

            }
            else
            {
                PB.PlayerTransfrom.position = SaveSystem.loadData.CurrentPlayerPositon;
                PB.TimeText.SetText(SaveSystem.loadData.CurrentTime);
                SaveSystem.loadData.IsTimeStop = false;
                PB.ElapsedTime = (float)SaveSystem.loadData.CurrentTime.TotalSeconds;
                Debug.Log("contenued player position : " + SaveSystem.loadData.CurrentPlayerPositon);
                Debug.Log("contenued play time : " + (float)SaveSystem.loadData.CurrentTime.TotalSeconds);


            }

        }

        public UniTask Update(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
    }
}