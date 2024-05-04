using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame
{
    public class Irisout : MonoBehaviour
    {

        [SerializeField] Material fullScreenMaterial;
        public float DurationTime = 1.5f;
        float elapsedTime = 0f;
        bool compliteAnimation;
        void Start()
        {
            gameObject.SetActive(true);
        }
        void Update()
        {
            if (!compliteAnimation)
            {
                elapsedTime += Time.deltaTime;
                float value = Mathf.Lerp(0f, 1f, elapsedTime / DurationTime);
                fullScreenMaterial.SetFloat("_Radius", value);
                if (value == 1)
                {
                    compliteAnimation = true;
                    gameObject.SetActive(false);
                }
            }

        }

    }
}