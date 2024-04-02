using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;
using VContainer.Unity;

public class MainLifeTimeScope : LifetimeScope
{

    [SerializeField] PlayerComponets _playerComponents;
    [SerializeField] PageContainer pageContainer;
    [SerializeField] Bg.UniTaskStateMachine.StateMachineBehaviour stateMachineBehaviour;
    [SerializeField] GameObject ConfigMaskPrefab;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerComponents);
        builder.RegisterComponent(stateMachineBehaviour);
        builder.RegisterInstance(pageContainer);
        if (ConfigMaskPrefab != null)
            builder.RegisterInstance(ConfigMaskPrefab);

        builder.RegisterEntryPoint<Main_EntryPoint>(Lifetime.Singleton);
        builder.Register<LoadUiSystem>(Lifetime.Singleton);
        builder.Register<Pages_Model>(Lifetime.Singleton);
        builder.Register<KeyBindSystem>(Lifetime.Singleton);


    }
}
