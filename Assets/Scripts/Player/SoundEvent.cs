using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
public class SoundEvent : MonoBehaviour
{
    public int id;
    void Start()
    {
        var collider2D = GetComponent<Collider2D>();
        collider2D.OnCollisionEnter2DAsObservable().Subscribe(other =>
        {
            SoundSystem.Instance.PlaySE(id);
        }).AddTo(this);
    }

    void Update()
    {

    }
}
