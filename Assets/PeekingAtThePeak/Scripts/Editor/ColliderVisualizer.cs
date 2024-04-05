using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class ColliderVisualizer : Editor
{
    private void OnSceneGUI()
    {
        // シーン内のすべての2Dコライダーを取得
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            DrawCollider2D(collider);
        }
    }

    private void DrawCollider2D(Collider2D collider)
    {
        if (collider == null)
            return;

        // コライダーの種類に応じて適切な描画方法を選択
        if (collider is BoxCollider2D)
        {
            DrawBoxCollider2D((BoxCollider2D)collider);
        }
        else if (collider is CircleCollider2D)
        {
            DrawCircleCollider2D((CircleCollider2D)collider);
        }
        // 他の2Dコライダータイプについても同様に処理を追加できます
    }

    private void DrawBoxCollider2D(BoxCollider2D collider)
    {
        Handles.color = Color.green;
        Vector3 center = collider.transform.TransformPoint(collider.offset);
        Vector2 size = collider.size;
        Vector2 scale = collider.transform.lossyScale;
        Vector3 scaledSize = new Vector3(size.x * scale.x, size.y * scale.y, 1f);
        Handles.DrawWireCube(center, scaledSize);
    }

    private void DrawCircleCollider2D(CircleCollider2D collider)
    {
        Handles.color = Color.blue;
        Vector3 center = collider.transform.TransformPoint(collider.offset);
        float radius = collider.radius * Mathf.Max(collider.transform.lossyScale.x, collider.transform.lossyScale.y);
        Handles.DrawWireDisc(center, Vector3.back, radius);
    }
}
