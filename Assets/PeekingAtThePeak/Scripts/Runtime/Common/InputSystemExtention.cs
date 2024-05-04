using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyGame
{
    public static class InputSystemExtension
    {
        public static ReadOnlyReactiveProperty<bool> GetButtonProperty(this InputAction inputAction)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                    h => inputAction.performed += h,
                    h => inputAction.performed -= h)
                .Select(x => x.ReadValueAsButton())
                .ToReadOnlyReactiveProperty(false);
        }


        public static ReadOnlyReactiveProperty<float> GetAxisProperty(this InputAction inputAction)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                    h => inputAction.performed += h,
                    h => inputAction.performed -= h)
                .Select(x => x.ReadValue<float>())
                .ToReadOnlyReactiveProperty(0);
        }


        public static ReadOnlyReactiveProperty<float> GetDeltaAxisProperty(this InputAction inputAction)
        {
            return Observable.EveryUpdate().Select(_ => inputAction.ReadValue<float>()).ToReadOnlyReactiveProperty(0);
        }

    }
}