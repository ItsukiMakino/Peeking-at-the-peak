using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyBox : MonoBehaviour
{
    public Material targetMaterial; // Shader Graphで作成したマテリアル
    Color TopStartColor;
    Color BottomStartColor;
    Color TopBeginColor;
    Color BottomBeginColor;
    public Color TopTargetColor = Color.blue;
    public Color BottomTargetColor = Color.blue;

    public float lerpDuration = 1.0f; // 補間の時間

    public int BGMid;
    public bool PlayImmidiate;
    private float lerpTime = 0.0f;

    private bool isColliding = false;
    private MaterialPropertyBlock mpb;
    CancellationToken token;
    void Start()
    {
        token = this.GetCancellationTokenOnDestroy();
        mpb = new MaterialPropertyBlock();
        targetMaterial = RenderSettings.skybox;
        TopStartColor = targetMaterial.GetColor(ZString.Concat("_Top"));
        BottomStartColor = targetMaterial.GetColor(ZString.Concat("_Bottom"));




    }
    void OnTriggerEnter2D(Collider2D other)
    {
        isColliding = true;
        lerpTime = 0.0f;
        TopBeginColor = targetMaterial.GetColor(ZString.Concat("_Top"));
        BottomBeginColor = targetMaterial.GetColor(ZString.Concat("_Bottom"));
        SoundSystem.Instance.PlayBGM(BGMid, token, PlayImmidiate).Forget();


    }

    void OnTriggerExit2D(Collider2D other)
    {
        isColliding = false;
        lerpTime = 0.0f;
    }

    private void Update()
    {
        if (isColliding)
        {
            lerpTime += Time.deltaTime;
            float t = Mathf.Clamp01(lerpTime / lerpDuration);


            Color TopLerpedColor = Color.Lerp(TopBeginColor, TopTargetColor, t);
            Color BottomLerpedColor = Color.Lerp(BottomBeginColor, BottomTargetColor, t);
            // マテリアルのカラープロパティを変更
            targetMaterial.SetColor(ZString.Concat("_Top"), TopLerpedColor);
            targetMaterial.SetColor(ZString.Concat("_Bottom"), BottomLerpedColor);



        }

    }
    void OnDestroy()
    {
        targetMaterial.SetColor(ZString.Concat("_Top"), TopStartColor);
        targetMaterial.SetColor(ZString.Concat("_Bottom"), BottomStartColor);
    }
}






