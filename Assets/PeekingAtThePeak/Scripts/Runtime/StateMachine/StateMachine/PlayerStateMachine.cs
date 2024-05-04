using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyGame.Deprecated;
using UnityEngine;
using VContainer.Unity;

namespace MyGame.StateMachine
{
    public class PlayerStateMachine : StateMachineBase
    {

        IReadOnlyList<IState> states;

        public IState idle;
        public IState crouchState;
        public IState jumpState;
        public IState jumpToCrouch;
        public IState jumpToStrech;
        public IState playerInitialState;
        public IState strechState;



        public PlayerStateMachine(IReadOnlyList<IState> states)
        {
            this.states = states;
            updatable = true;

            foreach (var state in states)
            {
                switch (state)
                {
                    case IdleState: idle = state; break;
                    case CrouchState: crouchState = state; break;
                    case JumpState: jumpState = state; break;
                    case JumpToCrouch: jumpToCrouch = state; break;
                    case JumpToStrech: jumpToStrech = state; break;
                    case PlayerInitialState: playerInitialState = state; break;
                    case StrechState: strechState = state; break;

                    default: break;

                }
            }
            currentState = idle;
            source = new CancellationTokenSource();
        }
        CancellationTokenSource source;

        public void Start()
        {
            currentState.Start(source.Token);
        }
        public async void Tick()
        {
            if (updatable)
                await currentState.Update(source.Token);
        }
    }
}