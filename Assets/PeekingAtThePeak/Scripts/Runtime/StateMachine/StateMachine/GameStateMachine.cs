using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace MyGame.StateMachine
{
    public class GameStateMachine : StateMachineBase
    {

        IReadOnlyList<IState> states;

        public IState initialState;
        public IState playState;
        public IState poseState;
        public IState menuState;

        public GameStateMachine(IReadOnlyList<IState> states)
        {
            this.states = states;

            foreach (var state in states)
            {
                switch (state)
                {
                    case InitalState: initialState = state; break;
                    case PlayState: playState = state; break;
                    case PoseState: poseState = state; break;
                    case MenuState: menuState = state; break;
                    default: break;

                }
            }
            currentState = initialState;
            source = new CancellationTokenSource();
        }
        CancellationTokenSource source;
        public void Start()
        {
            currentState.Start(source.Token);
        }
        public async void Tick()
        {
            if (Updatable)
                await currentState.Update(source.Token);
        }


    }
}