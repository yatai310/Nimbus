using UnityEngine;

public class Core : MonoBehaviour
{
    public float attractionRadius = 5f;
    public float attractionForce = 10f;
    public LayerMask targetLayer;

    void FixedUpdate()
    {
        // 2D用の範囲取得
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attractionRadius, targetLayer);

        foreach (Collider2D target in targets)
        {
            Rigidbody2D rb = target.attachedRigidbody;
            if (rb != null)
            {
                Vector2 direction = (transform.position - target.transform.position).normalized;
                rb.AddForce(direction * attractionForce);
            }
        }
    }
}
