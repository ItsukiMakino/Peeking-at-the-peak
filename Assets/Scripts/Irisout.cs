using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using UnityEngine;

public class Irisout : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Material fullScreenMaterial;
    public float DurationTime = 0.8f;
    private float elapsedTime = 0f;
    void Start()
    {
        gameObject.SetActive(true);
    }
    bool compliteAnimation;
    void Update()
    {
        if (!compliteAnimation)
        {
            elapsedTime += Time.deltaTime;
            float value = Mathf.Lerp(0f, 1f, elapsedTime / DurationTime);
            fullScreenMaterial.SetFloat(ZString.Concat("_Radius"), value);
            if (value == 1)
            {
                compliteAnimation = true;
                gameObject.SetActive(false);
            }
        }

    }

}
