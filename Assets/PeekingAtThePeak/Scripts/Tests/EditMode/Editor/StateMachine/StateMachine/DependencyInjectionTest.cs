using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using VContainer;
using MyGame.StateMachine;
using VContainer.Unity;
namespace MyGame.Tests
{
    public class DepedencyInjectionTest
    {
        [Test]
        public void Exists()
        {
            var builder = new ContainerBuilder();
            builder.Register<IState, InitalState>(Lifetime.Singleton);
            builder.Register<IState, PlayState>(Lifetime.Singleton);
            builder.Register<IState, PoseState>(Lifetime.Singleton);

            Assert.AreEqual(true, builder.Exists(typeof(InitalState)));
            Assert.AreEqual(true, builder.Exists(typeof(PlayState)));
            Assert.AreEqual(true, builder.Exists(typeof(PoseState)));



        }
        [Test]
        public void InjectionTest()
        {
            var builder = new ContainerBuilder();
            builder.Register<IState, InitalState>(Lifetime.Singleton);
            builder.Register<IState, PlayState>(Lifetime.Singleton);
            builder.Register<IState, PoseState>(Lifetime.Singleton);
            builder.Register<IState, JumpState>(Lifetime.Singleton);
            builder.Register<IState, StrechState>(Lifetime.Singleton);
            builder.Register<PlayerInput>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<PlayerStateMachine>(Lifetime.Singleton);
            var go = new GameObject().AddComponent<PlayerBehaiviour>();
            builder.RegisterComponent(go.GetComponent<PlayerBehaiviour>());

            var container = builder.Build();

            var sm = container.Resolve<GameStateMachine>();
            var sm2 = container.Resolve<PlayerStateMachine>();
            Assert.That(sm.initialState, Is.Not.Null);
            Assert.That(sm.initialState, Is.TypeOf(typeof(InitalState)));
            Assert.That(sm2.jumpState, Is.Not.Null);
            Assert.That(sm2.jumpState, Is.TypeOf(typeof(JumpState)));
        }
    }
}
