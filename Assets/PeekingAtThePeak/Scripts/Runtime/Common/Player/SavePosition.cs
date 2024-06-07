using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bg.UniTaskStateMachine;
using SMB = Bg.UniTaskStateMachine.StateMachineBehaviour;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Text;

namespace MyGame
{
    public class SavePosition: MonoBehaviour
    {
        public void OnApplicationQuit()
        {
            SaveSystem.loadData.CurrentPlayerPositon = transform.position;
            SaveSystem.SaveAsync(SaveSystem.filePath, SaveSystem.loadData).Forget();
        }
    }

}