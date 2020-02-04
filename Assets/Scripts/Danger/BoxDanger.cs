using System.Collections.Generic;
using UnityEngine;

public class BoxDanger : Danger {
    public Transform Origin;
    public Vector2 Size;
    public float Angle;
    public LayerMask LayerMask;

    private readonly List<IDamageable> damageables = new List<IDamageable>();

    protected override IEnumerable<IDamageable> GetHits() {
        Collider2D[] cols = Physics2D.OverlapBoxAll(Origin.position, Size, Angle, LayerMask);
        damageables.Clear();
        damageables.Capacity = cols.Length;
        foreach (Collider2D col in cols) {
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
                damageables.Add(damageable);
        }
        return damageables;
    }
}
