using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityScreenNavigator;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Sheet;
using VContainer;
using VContainer.Unity;
namespace MyGame
{
    public class LoadUISystem
    {
        IObjectResolver objectResolver;
        public LoadUISystem(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;

        }
        public void PushPageWithInjection<T>(PageContainer pageContainer, string RererenceId, bool playAnimation = true, bool stack = true)
            where T : Page
        {
            pageContainer.Push(RererenceId, playAnimation, stack).OnTerminate += () =>
             {
                 var g = GameObject.FindFirstObjectByType<T>();
                 objectResolver.Inject(g);
             };
        }
        public void PushSheetWithInjection<T>(SheetContainer sheetContainer, string RererenceId, bool playAnimation = true)
            where T : Sheet
        {
            sheetContainer.Show(RererenceId, playAnimation).OnTerminate += () =>
             {
                 var g = GameObject.FindFirstObjectByType<T>();
                 objectResolver.Inject(g);
             };
        }

    }
}