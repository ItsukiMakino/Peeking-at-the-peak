using System.Collections;
using System.Collections.Generic;
using Cysharp.Text;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetMultiplyer(int value)
    {
        animator.SetFloat(ZString.Concat("Speed"), value);
        Debug.Log("setfloat");
    }
}
