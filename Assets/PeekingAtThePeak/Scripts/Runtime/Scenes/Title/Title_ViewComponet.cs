using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
namespace MyGame
{
    public class Title_ViewComponet : MonoBehaviour
    {
        [SerializeField] Canvas canvas;

        [SerializeField] Image image;

        public async UniTask FadeOutCanvas(float targetValue, CancellationToken cancellationToken)
        {
            await image.DOFade(targetValue, 0.2f);
            canvas.enabled = targetValue == 0 ? false : true;
        }
        public async UniTask FadeINCanvas(float targetValue, CancellationToken cancellationToken)
        {
            canvas.enabled = targetValue == 0 ? false : true;
            await image.DOFade(targetValue, 0.2f);
        }
    }
}