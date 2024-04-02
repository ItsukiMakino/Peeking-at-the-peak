using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using UnityEngine;
using UnityEngine.Timeline;
public class TimellineControl : MonoBehaviour, ITimeControl
{
    [SerializeField] Material fullScreenMaterial;
    public float DurationTime = 1.5f;
    public void OnControlTimeStart()
    {
        gameObject.SetActive(true);
    }

    public void OnControlTimeStop()
    {
        gameObject.SetActive(false);
    }
    private float elapsedTime = 0f;
    public void SetTime(double time)
    {
        elapsedTime += Time.deltaTime;
        float value = Mathf.Lerp(0f, 1f, elapsedTime / DurationTime);
        fullScreenMaterial.SetFloat(ZString.Concat("_Radius"), value);
    }
}
