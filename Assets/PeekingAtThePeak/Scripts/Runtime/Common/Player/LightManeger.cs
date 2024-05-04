using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame
{
    public class LightManeger : MonoBehaviour
    {

        Color initialColor;
        float transitionTimer = 0.0f;
        public Light _directionalLight;
        public Color targetColor;
        public float transitionDuration = 1.0f;
        bool IsColliding;
        void Start()
        {
            _directionalLight = FindObjectOfType<Light>();
            if (_directionalLight != null)
            {
                initialColor = _directionalLight.color;
            }
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            IsColliding = true;
            transitionTimer = 0f;
        }
        void OnTriggerExit2D(Collider2D other)
        {
            IsColliding = false;
            transitionTimer = 0f;
        }
        void LateUpdate()
        {
            if (IsColliding && transitionTimer != transitionDuration)
            {
                transitionTimer += Time.deltaTime;
                float t = Mathf.Clamp01(transitionTimer / transitionDuration); // タイマーの割合を計算
                _directionalLight.color = Color.Lerp(initialColor, targetColor, t);


            }

        }
    }
}