using MyGame;
using UnityEditor;
using UnityEngine;

public class ParameterWindow : EditorWindow
{
    // ウィンドウを表示するためのメニューアイテム
    [MenuItem("My Window/PlayerParameter")]
    public static void ShowWindow()
    {
        // ウィンドウを開く
        GetWindow<ParameterWindow>("PlayerParameter");
    }
    private GameObject targetObject;
    private PlayerBehaiviour pb;

    // カスタムウィンドウの内容を描画する
    private void OnGUI()
    {
        EditorGUILayout.Space();

        targetObject = EditorGUILayout.ObjectField("LifeTimeScope", targetObject, typeof(GameObject), true) as GameObject;

        EditorGUILayout.Space();
        pb = targetObject?.GetComponent<PlayerBehaiviour>();



        // EditorGUILayout.TextField("Text Field", "Default Text");
    }
}

