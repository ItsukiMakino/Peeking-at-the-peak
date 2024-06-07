using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace MyGame.StateMachine
{
    public class StateMachineController : IStartable, ITickable
    {

        GameStateMachine gameSBM;
        PlayerStateMachine playerSBM;
        PlayerInput playerInput;
        public StateMachineController(GameStateMachine gameSBM, PlayerStateMachine playerSBM, PlayerInput playerInput)
        {
            this.gameSBM = gameSBM;
            this.playerSBM = playerSBM;
            this.playerInput = playerInput;
        }
        public void Start()
        {
            gameSBM.Start();
            playerSBM.Start();
            playerInput.MyAction.Player.Enable();
            playerInput.MyAction.UI.Disable();
            gameSBM.ChangeState(gameSBM.playState).Forget();
        }
        public void Tick()
        {
            gameSBM.Tick();
            playerSBM.Tick();
            UpdateState();
        }
        public void UpdateState()
        {
            // MenuStateの時にESCが押されたらPlayStateへ遷移する
            if (IsPressEscapeInMenuState() && gameSBM.CurrentState == gameSBM.menuState)
            {
                //PlayerStateMachineを再開する
                playerSBM.Restore();
                gameSBM.ChangeState(gameSBM.playState).Forget();
                return;
            }
            // PlayStateの時にESCが押されたらMenuStateへ遷移する
            if (IsPressEscapeInPlayState() && gameSBM.CurrentState == gameSBM.playState)
            {
                //PlayerStateMachineを停止
                playerSBM.Stop();
                gameSBM.ChangeState(gameSBM.menuState).Forget();
                return;
            }



            if (IsPressOnlySpace())
            {
                playerSBM.ChangeState(playerSBM.jumpState).Forget();
                return ;
            }
            if (IsPressCtrAndSpace())
            {
                if (playerSBM.CurrentState == playerSBM.jumpState)
                {
                    playerSBM.ChangeState(playerSBM.jumpToCrouch).Forget();
                    return;
                }
                playerSBM.ChangeState(playerSBM.crouchState).Forget();
            }
            if (IsPressShiftAndSpace())
            {
                if (playerSBM.CurrentState == playerSBM.jumpState)
                {
                    playerSBM.ChangeState(playerSBM.jumpToStrech).Forget();
                    return ;
                }
                playerSBM.ChangeState(playerSBM.strechState).Forget();
            }
            if (IsNotPressJump())
            {
                playerSBM.ChangeState(playerSBM.idle).Forget();
                return;
            }


        }

        bool IsPressEscapeInMenuState() => playerInput.MyAction.UI.Back.IsPressed();
        bool IsPressEscapeInPlayState() => playerInput.MyAction.Player.Pose.IsPressed();

        bool IsPressOnlySpace() => !playerInput.LowerAction.IsPressed() && !playerInput.UpperAction.IsPressed() && playerInput.JumpAction.IsPressed();
        bool IsNotPressJump() => !playerInput.JumpAction.IsPressed();
        bool IsPressCtrAndSpace() => playerInput.LowerAction.IsPressed() && playerInput.JumpAction.IsPressed();
        bool IsPressShiftAndSpace() => playerInput.UpperAction.IsPressed() && playerInput.JumpAction.IsPressed();

        bool IsAnyPressButton() => !playerInput.LowerAction.IsPressed() && !playerInput.UpperAction.IsPressed() && !playerInput.JumpAction.IsPressed();
    }
}