using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Sheet;
using VContainer;
using VContainer.Unity;
namespace MyGame
{
    public class TitleLifeTimeScope : LifetimeScope
    {
        [SerializeField] PageContainer _pageContainer;
        [SerializeField] Title_ViewComponet title_ViewComponet;
        [SerializeField] GameObject ConfigMaskPrefab;

        protected override void Configure(IContainerBuilder builder)
        {


            builder.RegisterEntryPoint<Title_EntryPoint>(Lifetime.Singleton);
            builder.RegisterInstance(_pageContainer);
            builder.RegisterComponent(title_ViewComponet);
            builder.Register<Title_Model>(Lifetime.Singleton);
            builder.Register<LoadUISystem>(Lifetime.Singleton);
            builder.Register<Pages_Model>(Lifetime.Singleton);
            builder.Register<PlayerInput>(Lifetime.Singleton);
            if (ConfigMaskPrefab != null)
                builder.RegisterInstance(ConfigMaskPrefab);
        }
    }
}