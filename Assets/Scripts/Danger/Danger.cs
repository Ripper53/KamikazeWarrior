using System.Collections.Generic;
using UnityEngine;

public abstract class Danger : MonoBehaviour {
    public int Damage = 1;

    private void FixedUpdate() {
        foreach (IDamageable damageable in GetHits())
            damageable.Damage(Damage);
    }

    protected abstract IEnumerable<IDamageable> GetHits();
}
