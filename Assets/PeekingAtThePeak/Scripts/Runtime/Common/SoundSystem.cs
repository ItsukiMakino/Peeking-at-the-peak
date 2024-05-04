using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
using CriWare.Assets;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks.Triggers;
using Cysharp.Text;
using System.ComponentModel;

namespace MyGame
{
    public class SoundSystem : MonoBehaviour
    {
        [SerializeField] CriAtomSourceForAsset _criAtomSourceSE;
        [SerializeField] CriAtomSourceForAsset _criAtomSourceStreaming;

        [SerializeField] CriAtomAcbAsset acbSE;
        [SerializeField] CriAtomAcbAsset acbBGM;

        public static SoundSystem Instance;

        public async UniTask SelectSEasync(CancellationToken token)
        {

            _criAtomSourceSE.player.SetCue(acbSE.Handle, 2);
            CriAtomExPlayback playback = _criAtomSourceSE.player.Start();
            await UniTask.WaitUntil(() => _criAtomSourceSE.player.GetStatus() == CriAtomExPlayer.Status.PlayEnd,
            cancellationToken: token);
        }

        // void OnDestroy()
        // {
        //     CriAtomAssetsLoader.ReleaseCueSheet(acbSE);
        //     CriAtomAssetsLoader.ReleaseCueSheet(acbVibe);
        //     CriAtomAssetsLoader.ReleaseCueSheet(itemSE);
        // }

        public void PlaySE(int id)
        {
            _criAtomSourceSE.player.SetCue(acbSE.Handle, id);
            _criAtomSourceSE.player.Start();
        }
        public async UniTask PlaySEasync(int id, CancellationToken token)
        {
            _criAtomSourceSE.player.SetCue(acbSE.Handle, id);
            criAtomExPlayback = _criAtomSourceSE.player.Start();
            await UniTask.WaitWhile(() => criAtomExPlayback.status != CriAtomExPlayback.Status.Playing, cancellationToken: token);
        }
        CriAtomExPlayback criAtomExPlayback;
        int currentCueId;
        public async UniTask PlayBGM(int id, CancellationToken ct, bool playImmidiate = false)
        {
            if (playImmidiate)
            {
                currentCueId = id;
                await UniTask.WaitWhile(() => !acbBGM.Loaded, cancellationToken: ct);
                _criAtomSourceStreaming.player.SetCue(acbBGM.Handle, id);
                criAtomExPlayback = _criAtomSourceStreaming.player.Start();
                return;
            }
            else if (currentCueId == id)
            {
                return;
            }
            currentCueId = id;
            await UniTask.WaitWhile(() => !acbBGM.Loaded, cancellationToken: ct);
            _criAtomSourceStreaming.player.SetCue(acbBGM.Handle, id);
            criAtomExPlayback = _criAtomSourceStreaming.player.Start();
        }
        public void SetSEVolume()
        {
            _criAtomSourceSE.volume = SaveSystem.loadData.SeVolume * SaveSystem.loadData.MasterVolume;
        }
        public void SetBGMVolume()
        {
            _criAtomSourceStreaming.volume = SaveSystem.loadData.BgmVolume * SaveSystem.loadData.MasterVolume;
        }
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);

            }
            else
            {
                Destroy(this.gameObject);
            }

        }

        async UniTask Start()
        {
            await UniTask.WaitWhile(() => !CriAtomPlugin.IsLibraryInitialized());
            // await UniTask.WaitWhile(() => acbSE.Handle is null, cancellationToken: this.GetCancellationTokenOnDestroy());

        }

    }
}