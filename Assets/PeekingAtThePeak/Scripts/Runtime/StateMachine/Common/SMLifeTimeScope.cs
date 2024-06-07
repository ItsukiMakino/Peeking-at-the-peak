using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyGame.StateMachine
{
    public class SMLifeTimeScope : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StateMachineController>(Lifetime.Singleton);

            //Playerstatemachine に必要な依存関係を登録
            builder.Register<PlayerStateMachine>(Lifetime.Singleton);

            builder.Register<IState, IdleState>(Lifetime.Singleton);
            builder.Register<IState, CrouchState>(Lifetime.Singleton);
            builder.Register<IState, JumpState>(Lifetime.Singleton);
            builder.Register<IState, JumpToCrouch>(Lifetime.Singleton);
            builder.Register<IState, JumpToStrech>(Lifetime.Singleton);
            builder.Register<IState, PlayerInitialState>(Lifetime.Singleton);
            builder.Register<IState, StrechState>(Lifetime.Singleton);



        }
    }
}
