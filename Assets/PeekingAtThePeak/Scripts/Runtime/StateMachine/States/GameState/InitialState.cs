using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;


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
            await UniTask.WaitWhile(() => PB == null || SaveSystem.loadData == null, cancellationToken: ct);

            // ‚ß‚èž‚Ý–hŽ~
            PB.Rigidbody2D.gravityScale = 0;

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

            //Å‰‚ÌƒV[ƒ“‚ª“Ç‚Ýž‚Ü‚ê‚é‚Ü‚Å‘Ò‹@‚·‚é
            await UniTask.WaitWhile(() => !IsLoadedLevel(), cancellationToken: ct);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            PB.Rigidbody2D.gravityScale = 2.5f;
         
        }

        public UniTask Update(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public UniTask Exit(CancellationToken ct)
        {
            return UniTask.CompletedTask;
        }
        public bool IsLoadedLevel()
        {
            for(int i = 0; i< SceneManager.sceneCount; i++) 
            {
                Scene scene = SceneManager.GetSceneAt(i);
                foreach (string sceneName in SceneNames)
                {
                    if (scene.name == sceneName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public List<string> SceneNames = new()
        {
            "Stage1",
            "Stage2",
            "Stage3",
            "Stage4",
            "Stage5",
            "Stage6",
            "Stage7",
        };
    }
    

}