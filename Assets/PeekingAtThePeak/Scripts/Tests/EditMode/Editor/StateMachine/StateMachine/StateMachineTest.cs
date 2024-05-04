using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using VContainer;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;
using MyGame.StateMachine;
using VContainer.Unity;

namespace MyGame.Tests
{
    public class StateMachineTest
    {

        GameStateMachine gameStateMachine;
        PlayerStateMachine playerStateMachine;
        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            // register gamestates
            builder.Register<IState, InitalState>(Lifetime.Singleton);
            builder.Register<IState, PlayState>(Lifetime.Singleton);
            builder.Register<IState, PoseState>(Lifetime.Singleton);

            //register playerstates
            builder.Register<IState, IdleState>(Lifetime.Singleton);
            builder.Register<IState, JumpState>(Lifetime.Singleton);
            builder.Register<IState, StrechState>(Lifetime.Singleton);

            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<PlayerStateMachine>(Lifetime.Singleton);
            builder.Register<PlayerInput>(Lifetime.Singleton);

            var go = new GameObject().AddComponent<PlayerBehaiviour>();
            builder.RegisterComponent(go.GetComponent<PlayerBehaiviour>());

            var container = builder.Build();
            this.gameStateMachine = container.Resolve<GameStateMachine>();
            this.playerStateMachine = container.Resolve<PlayerStateMachine>();




        }
        [UnityTest]
        public IEnumerator ChangeStateTest() => UniTask.ToCoroutine(async () =>
        {
            Assert.AreEqual(gameStateMachine.initialState, gameStateMachine.CurrentState);
            Assert.AreEqual(playerStateMachine.idle, playerStateMachine.CurrentState);
            Assert.That(gameStateMachine.CurrentState, Is.Not.Null);
            Assert.That(playerStateMachine.CurrentState, Is.Not.Null);

            await gameStateMachine.ChangeState(gameStateMachine.playState);
            await playerStateMachine.ChangeState(playerStateMachine.jumpState);
            Assert.AreEqual(gameStateMachine.CurrentState, gameStateMachine.playState);
            Assert.That(playerStateMachine.CurrentState, Is.TypeOf(typeof(JumpState)));
        });

        [Test]
        public void RestoreTest()
        {
            playerStateMachine.Restore();
            Assert.That(playerStateMachine.Updatable, Is.True);
        }
        [Test]
        public void StopTest()
        {
            playerStateMachine.Stop();
            Assert.That(playerStateMachine.Updatable, Is.False);
        }
    }
}