using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;
using VContainer.Unity;
using MyGame.StateMachine;
namespace MyGame
{
    public class MainLifeTimeScope : LifetimeScope
    {
        [SerializeField] PageContainer pageContainer;
        [SerializeField] GameObject ConfigMaskPrefab;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LoadUISystem>(Lifetime.Singleton);
            builder.Register<Pages_Model>(Lifetime.Singleton);
            builder.Register<PlayerInput>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<PlayerBehaiviour>();
            builder.RegisterInstance(pageContainer);
            if (ConfigMaskPrefab != null)
                builder.RegisterInstance(ConfigMaskPrefab);
            // builder.RegisterComponent(stateMachineBehaviour);

            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<IState, InitalState>(Lifetime.Singleton);
            builder.Register<IState, PlayState>(Lifetime.Singleton);
            builder.Register<IState, PoseState>(Lifetime.Singleton);
            builder.Register<IState, MenuState>(Lifetime.Singleton);



        }
    }
}