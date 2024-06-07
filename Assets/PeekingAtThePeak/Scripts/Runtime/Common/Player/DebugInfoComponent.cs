#if UNITY_EDITOR || DEVELOPMENT_BUILD

using Cysharp.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    public class DebugInfoComponent : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text1;
        [SerializeField] Button position1_button;
        [SerializeField] Button position2_button;
        [SerializeField] Button position3_button;
        [SerializeField] Button position4_button;
        [SerializeField] Button position5_button;
        [SerializeField] Button position6_button;
        [SerializeField]Vector3 position1;
        [SerializeField]Vector3 position2;
        [SerializeField]Vector3 position3;
        [SerializeField]Vector3 position4;
        [SerializeField]Vector3 position5;
        [SerializeField]Vector3 position6;
        [Space]
        [SerializeField]Transform playerTransform;
        
        public TextMeshProUGUI Text1 { get => text1; set => text1 = value; }

        public void Start()
        {
            text1.SetText("操作方法(デフォルト)\n右に回転 : D\n左に回転 ：A\nジャンプ : space\n上半身コントロール  : Shift\n下半身コントロール : Ctrl");
            position1_button.OnClickAsObservable().Subscribe(_ =>
            {
                playerTransform.position = position1;
            });
            position2_button.OnClickAsObservable().Subscribe(_ =>
            {
                playerTransform.position = position2;
            });
            position3_button.OnClickAsObservable().Subscribe(_ =>
            {
                playerTransform.position = position3;
            });
            position4_button.OnClickAsObservable().Subscribe(_ =>
            {
                playerTransform.position = position4;
            });
            position5_button.OnClickAsObservable().Subscribe(_ =>
            {
                playerTransform.position = position5;
            });
            position6_button.OnClickAsObservable().Subscribe(_ =>
            {
                playerTransform.position = position6;
            });

        }
        public void SetText()
        {

        }
    }
}
#endif